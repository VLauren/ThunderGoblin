using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pararrayos : MonoBehaviour
{
    Goblin[] goblins;
    
    private void Start()
    {
        GestorJuego.instance.OnRayo += RecibirRayo;
        goblins = FindObjectsOfType<Goblin>();
        GestorJuego.instance.OnReload += Destruir;
    }

    private void RecibirRayo()
    {
        StartCoroutine(Rayazo());
    }

    IEnumerator Rayazo()
    {
        yield return new WaitForSeconds(Random.value * 0.2f);

        // efectos
        Instantiate(GestorJuego.instance.prefabRayoA, transform.Find("PuntoImpacto").position, Quaternion.Euler(90,0,0));
        Instantiate(GestorJuego.instance.prefabRayoB, transform.position, Quaternion.identity);

        // matar
        foreach (Goblin g in goblins)
            if (g != null && Vector3.Distance(transform.position, g.transform.position) < Global.AlcanceRayo)
                g.Morir();

        // sonido
        Sonido.PlaySonido("Rayo");
        Sonido.PlaySonido("Circulo");
    }

    private void Destruir()
    {
        Destroy(gameObject);
    }
}
