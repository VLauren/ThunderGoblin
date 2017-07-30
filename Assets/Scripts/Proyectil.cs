using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public Goblin.Jugador jugador;

    private Vector3 puntoInicial;
    private Vector3 puntoFinal;

    private Vector3 puntoActual;
    private Vector3 puntoAnterior;
    private Vector3 UltimoPunto;
    private float distanciaLanzamiento;
    
    public void Lanzar(Vector3 direccion, float distancia, Goblin.Jugador jug)
    {
        puntoInicial = transform.position;
        puntoFinal = puntoInicial + direccion * distancia;
        puntoActual = transform.position;
        distanciaLanzamiento = distancia;

        jugador = jug;
        Sonido.PlaySonido("Lanza");
    }

    private void Update()
    {
        // volando
        if(puntoActual != puntoFinal)
        {
            puntoActual = Vector3.MoveTowards(puntoActual, puntoFinal, Time.deltaTime * Global.VelocidadProyectil);
            puntoAnterior = transform.position;
            transform.position = puntoActual;

            float d = Vector3.Distance(puntoInicial, puntoActual);
            if (d > distanciaLanzamiento / 2)
            {
                GetComponent<Rigidbody>().isKinematic = false; 
                d = distanciaLanzamiento - d;
            }
            d /= (distanciaLanzamiento / 2);
            float altura = 2 * Mathf.Sin(d * Mathf.PI / 2) * (distanciaLanzamiento / 4);

            transform.position = new Vector3(transform.position.x, altura + 1, transform.position.z);
            UltimoPunto = transform.position;
        }
        // he llegado
        else
        {
            GetComponent<Rigidbody>().velocity = (UltimoPunto - puntoAnterior).normalized * 20;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Sonido.PlaySonido("Pararrayos");
        Instantiate(GestorJuego.instance.prefabPararrayos, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
