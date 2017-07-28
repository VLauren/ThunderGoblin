using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestor : MonoBehaviour
{
    public static Gestor instancia { get; private set; }

    public GameObject prefabProyectil;

    void Awake()
    {
        instancia = this;
    }
}
