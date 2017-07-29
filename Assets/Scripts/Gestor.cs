using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestor : MonoBehaviour
{
    public static Gestor instance { get; private set; }

    public GameObject prefabProyectil;
    public GameObject prefabCirculo;

    void Awake()
    {
        instance = this;
    }
}
