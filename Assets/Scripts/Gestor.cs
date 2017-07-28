using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestor : MonoBehaviour
{
    private static Gestor instance;

    void Awake()
    {
        instance = this;
    }
}
