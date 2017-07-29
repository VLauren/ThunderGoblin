using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    Goblin[] goblins;
    Vector3 diferencia;
    Vector3 vel;

    private void Start()
    {
        goblins = FindObjectsOfType<Goblin>();

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = new Ray(transform.position, transform.forward);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
            diferencia = -ray.direction * rayDistance;
    }

    void Update ()
    {
        if(goblins[0] != null && goblins[1] != null)
        {
            Vector3 posObjetivo = diferencia + (goblins[0].transform.position + goblins[1].transform.position) / 2;
            transform.position = Vector3.SmoothDamp(transform.position, posObjetivo, ref vel, 0.3f);
        }
	}
}
