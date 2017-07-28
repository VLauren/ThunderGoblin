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
    public float distanciaTest = 2;

    private CharacterController cc;
    private NombresInput nombresInput;
    private Transform modelo;
    private Vector3[] puntosTrayectoriaSuelo = new Vector3[5];

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
                modelo.rotation = Quaternion.RotateTowards(modelo.rotation, rotacionObjetivo, Time.deltaTime * Global.VelocidadRotacion);
        }

        if(Input.GetButton(nombresInput.BOTON_A))
        {
            Debug.Log("OK");
            for (int i = 0; i < puntosTrayectoriaSuelo.Length; i++)
            {
                puntosTrayectoriaSuelo[i] = transform.position + (i * transform.position.normalized * distanciaTest / puntosTrayectoriaSuelo.Length);
            }
        }
    }
}
