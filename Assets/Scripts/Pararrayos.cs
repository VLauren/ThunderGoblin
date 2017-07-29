using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pararrayos : MonoBehaviour
{
    Goblin[] goblins;
    
    private void Start()
    {
        Gestor.instance.OnRayo += RecibirRayo;
        goblins = FindObjectsOfType<Goblin>();
    }

    private void RecibirRayo()
    {
        // efectos
        Instantiate(Gestor.instance.prefabRayoA, transform.position, Quaternion.Euler(90,0,0));
        Instantiate(Gestor.instance.prefabRayoB, transform.position, Quaternion.identity);

        // matar
        foreach (Goblin g in goblins)
            if (Vector3.Distance(transform.position, g.transform.position) < Global.AlcanceRayo)
                g.Morir(); ;
    }
}
