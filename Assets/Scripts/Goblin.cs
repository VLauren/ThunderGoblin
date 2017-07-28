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

    public Jugador jugador;

    private CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Debug.Log("Horizontal " + Input.GetAxisRaw("Horizontal"));
        Debug.Log("Vertical " + Input.GetAxisRaw("Vertical"));
        Debug.Log("Horizontal2 " + Input.GetAxisRaw("Horizontal2"));
        Debug.Log("Vertical2 " + Input.GetAxisRaw("Vertical2"));

    }

    private void FixedUpdate()
    {
        cc.Move(Vector3.right * Time.fixedDeltaTime);
    }
}
