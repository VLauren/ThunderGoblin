using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float distanciaLanzamiento = 2;
    private bool apuntando = false;
    private LineRenderer lr;
    private Vector3[] puntosTrayectoriaSuelo = new Vector3[10];

    private GameObject circulo;

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
        lr = GetComponent<LineRenderer>();
        lr.positionCount = puntosTrayectoriaSuelo.Length;
        lr.startColor = new Color(0, 0, 0, 0);
        lr.endColor = new Color(0, 0, 0, 0);
    }

    private void Update()
    {
        // movimiento direccional
        Vector3 direccion = Vector3.zero;
        direccion.x = Input.GetAxisRaw(nombresInput.EJE_HORIZONTAL);
        direccion.z = Input.GetAxisRaw(nombresInput.EJE_VERTICAL);
        cc.Move(direccion * Time.deltaTime * Global.VelocidadMovimiento);

        // rotacion del modelo
        if(direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion, Vector3.up);
            if (modelo)
            {
                if(apuntando)
                    modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotacionObjetivo, Time.deltaTime * Global.VelocidadRotacionApuntando);
                else
                    modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotacionObjetivo, Time.deltaTime * Global.VelocidadRotacion);
            }
        }

        // ==========================
        //  Lanzamiento

        if (Input.GetButtonDown(nombresInput.BOTON_A))
        {
            apuntando = true;
            circulo = Instantiate(Gestor.instance.prefabCirculo, transform.position, Quaternion.Euler(90, 0, 0));
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
            Proyectil p = Instantiate(Gestor.instance.prefabProyectil, transform.position, Quaternion.identity).GetComponent<Proyectil>();
            p.Lanzar(modelo.forward, distanciaLanzamiento);

            apuntando = false;
            distanciaLanzamiento = 0;
            lr.startColor = new Color(0, 0, 0, 0);
            lr.endColor = new Color(0, 0, 0, 0);

            Destroy(circulo);
        }
    }
}
