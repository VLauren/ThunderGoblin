using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    Goblin[] goblins;
    Vector3 diferencia;
    Vector3 vel;
    float intensidadTemblor;
    const float decrementeTemblor = 1f;

    private void Start()
    {
        goblins = FindObjectsOfType<Goblin>();

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = new Ray(transform.position, transform.forward);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
            diferencia = -ray.direction * rayDistance;

        GestorJuego.instance.OnRayo += Temblor;
    }

    void Update ()
    {
        if(goblins[0] != null && goblins[1] != null)
        {
            Vector3 posObjetivo = diferencia + (goblins[0].transform.position + goblins[1].transform.position) / 2;
            transform.position = Vector3.SmoothDamp(transform.position, posObjetivo, ref vel, 0.3f);
        }

        if(intensidadTemblor > 0)
        {
            transform.position += Random.onUnitSphere * intensidadTemblor;
            intensidadTemblor -= Time.deltaTime * decrementeTemblor;
        }
        if (intensidadTemblor < 0)
            intensidadTemblor = 0;
	}

    void Temblor()
    {
        if (FindObjectOfType<Pararrayos>() != null)
            StartCoroutine(TemblorConRetraso());
    }
    
    IEnumerator TemblorConRetraso()
    {
        yield return new WaitForSeconds(0.2f);
        intensidadTemblor = 0.5f;
    }

}
