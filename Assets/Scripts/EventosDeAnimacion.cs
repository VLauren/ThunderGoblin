using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosDeAnimacion : MonoBehaviour
{

    void Paso()
    {
        // Debug.Log("Paso!");
    }

    void Rodar()
    {
        // Debug.Log("Rodar!");
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.UNO)
            Sonido.PlaySonido("Rodar1", 0.7f);
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.DOS)
            Sonido.PlaySonido("Rodar2", 0.7f);
    }
}
