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
        cc.Move(Global.VelocidadMovimiento * Vector3.right * Time.deltaTime * Input.GetAxisRaw(nombresInput.EJE_HORIZONTAL));
        cc.Move(Global.VelocidadMovimiento * Vector3.forward * Time.deltaTime * Input.GetAxisRaw(nombresInput.EJE_VERTICAL));
    }
}
