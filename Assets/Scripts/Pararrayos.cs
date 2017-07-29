using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pararrayos : MonoBehaviour
{
    private void Start()
    {
        Gestor.instance.OnRayo += RecibirRayo;
    }

    private void RecibirRayo()
    {
        Debug.Log("BUM");
    }
}
