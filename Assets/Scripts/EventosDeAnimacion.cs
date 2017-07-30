using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventosDeAnimacion : MonoBehaviour
{

    void Paso()
    {
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.UNO)
            Sonido.PlaySonido("Paso1");
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.DOS)
            Sonido.PlaySonido("Paso2");

        Instantiate(GestorJuego.instance.prefabHumoA, transform.position, Quaternion.Euler(-90, 0, 0));
    }

    void Rodar()
    {
        // Debug.Log("Rodar!");
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.UNO)
            Sonido.PlaySonido("Rodar1");
        if (transform.parent.GetComponent<Goblin>().jugador == Goblin.Jugador.DOS)
            Sonido.PlaySonido("Rodar2");
    }
}
