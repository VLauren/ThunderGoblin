using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goblin : MonoBehaviour
{
    public enum Jugador
    {
        UNO,
        DOS
    }

    public struct NombresInput
    {
        public string EJE_HORIZONTAL;
        public string EJE_VERTICAL;
        public string BOTON_A;
        public string BOTON_B;
    }

    public Jugador jugador;

    private CharacterController cc;
    private NombresInput nombresInput;
    private Transform modelo;

    private float distanciaLanzamiento = 5;
    private bool apuntando = false;
    private LineRenderer lr;
    private Vector3[] puntosTrayectoriaSuelo = new Vector3[10];

    private GameObject circulo;

    private bool rodando = false;
    private Vector3 direccionRodar;
    private float tInicioRodar;

    private Animator animator;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        nombresInput = new NombresInput();

        if(jugador == Jugador.UNO)
        {
            nombresInput.EJE_HORIZONTAL = "Horizontal";
            nombresInput.EJE_VERTICAL = "Vertical";
            nombresInput.BOTON_A = "Fire";
            nombresInput.BOTON_B = "Jump";
        }
        if(jugador == Jugador.DOS)
        {
            nombresInput.EJE_HORIZONTAL = "Horizontal2";
            nombresInput.EJE_VERTICAL = "Vertical2";
            nombresInput.BOTON_A = "Fire2";
            nombresInput.BOTON_B = "Jump2";
        }

        modelo = transform.Find("Modelo");
        animator = modelo.GetComponent<Animator>();
        lr = GetComponent<LineRenderer>();
        lr.positionCount = puntosTrayectoriaSuelo.Length;
        lr.startColor = new Color(0, 0, 0, 0);
        lr.endColor = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        if(rodando)
        {
            cc.Move(direccionRodar * Time.deltaTime * Global.VelocidadRodar);

            if (Time.time - tInicioRodar > Global.TiempoRodar)
            {
                rodando = false;
            }

            return;
        }
        else
            animator.SetBool("Rodando", false);

        // movimiento direccional
        Vector3 direccion = Vector3.zero;
        direccion.x = Input.GetAxisRaw(nombresInput.EJE_HORIZONTAL);
        direccion.z = Input.GetAxisRaw(nombresInput.EJE_VERTICAL);
        direccion.Normalize();
        if(!apuntando)
            cc.Move(direccion * Time.deltaTime * Global.VelocidadMovimiento);

        // rotacion del modelo
        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion, Vector3.up);
            if (modelo)
            {
                if (apuntando)
                    modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotacionObjetivo, Time.deltaTime * Global.VelocidadRotacionApuntando);
                else
                    modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotacionObjetivo, Time.deltaTime * Global.VelocidadRotacion);
            }
            animator.SetFloat("Velocidad", 1);
        }
        else
            animator.SetFloat("Velocidad", 0);

        // ==========================
        //  Lanzamiento

        if (Input.GetButtonDown(nombresInput.BOTON_A))
        {
            apuntando = true;
            circulo = Instantiate(GestorJuego.instance.prefabCirculo, transform.position, Quaternion.Euler(90, 0, 0));
            animator.SetTrigger("IniciarLanzamiento");
        }

        if(apuntando)
        {
            distanciaLanzamiento += Time.deltaTime * Global.VelocidadLanzamiento;
            for (int i = 0; i < puntosTrayectoriaSuelo.Length; i++)
            {
                puntosTrayectoriaSuelo[i] = transform.position + (i * modelo.forward.normalized * distanciaLanzamiento / (puntosTrayectoriaSuelo.Length - 1));

                float d = Vector3.Distance(transform.position, puntosTrayectoriaSuelo[i]);
                if (d > distanciaLanzamiento / 2)
                    d = distanciaLanzamiento - d;
                d /= (distanciaLanzamiento / 2);
                puntosTrayectoriaSuelo[i].y = 2 * Mathf.Sin(d * Mathf.PI / 2) * (distanciaLanzamiento/4);
            }

            lr.SetPositions(puntosTrayectoriaSuelo);
            lr.startColor = new Color(1, 1, 1, 0.2f);
            lr.endColor = new Color(1, 1, 1, 0.2f);

            circulo.transform.position = puntosTrayectoriaSuelo[puntosTrayectoriaSuelo.Length - 1] + Vector3.up * 1;
        }

        if (Input.GetButtonUp(nombresInput.BOTON_A))
        {
            // lanzo
            /*
            Proyectil p = Instantiate(GestorJuego.instance.prefabProyectil, transform.position, Quaternion.identity).GetComponent<Proyectil>();
            p.Lanzar(modelo.forward, distanciaLanzamiento, jugador);

            apuntando = false;
            distanciaLanzamiento = 5;
            lr.startColor = new Color(0, 0, 0, 0);
            lr.endColor = new Color(0, 0, 0, 0);

            Destroy(circulo);

            animator.SetTrigger("Lanzar");
            */
            StartCoroutine(Lanzar());
        }

        if (Input.GetButtonUp(nombresInput.BOTON_B))
        {
            tInicioRodar = Time.time;
            modelo.rotation = Quaternion.LookRotation(direccion, Vector3.up);
            direccionRodar = direccion;
            rodando = true;
            animator.SetBool("Rodando", true);
        }
    }

    IEnumerator Lanzar()
    {
        animator.SetTrigger("Lanzar");
        yield return new WaitForSeconds(0.2f);

        Proyectil p = Instantiate(GestorJuego.instance.prefabProyectil, transform.position, Quaternion.identity).GetComponent<Proyectil>();
        p.Lanzar(modelo.forward, distanciaLanzamiento, jugador);

        apuntando = false;
        distanciaLanzamiento = 5;
        lr.startColor = new Color(0, 0, 0, 0);
        lr.endColor = new Color(0, 0, 0, 0);

        Destroy(circulo);
    }

    public void Morir()
    {
        Debug.Log("MUERTE jugador " + jugador);
        GestorJuego.TerminarRonda();
        Destroy(gameObject);
    }
}
