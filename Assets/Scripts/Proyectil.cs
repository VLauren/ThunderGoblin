using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    private Vector3 puntoInicial;
    private Vector3 puntoFinal;

    private Vector3 puntoActual;
    private float distanciaLanzamiento;
    
    public void Lanzar(Vector3 direccion, float distancia)
    {
        puntoInicial = transform.position;
        puntoFinal = puntoInicial + direccion * distancia;
        puntoActual = transform.position;
        distanciaLanzamiento = distancia;
    }

    private void Update()
    {
        if(puntoActual != puntoFinal)
        {
            puntoActual = Vector3.MoveTowards(puntoActual, puntoFinal, Time.deltaTime * Global.VelocidadProyectil);
            transform.position = puntoActual;

            float d = Vector3.Distance(puntoInicial, puntoActual);
            if (d > distanciaLanzamiento / 2)
                d = distanciaLanzamiento - d;
            d /= (distanciaLanzamiento / 2);
            float altura = 2 * Mathf.Sin(d * Mathf.PI / 2) * (distanciaLanzamiento / 4);

            transform.position = new Vector3(transform.position.x, altura + 1, transform.position.z);
        }
    }
}
