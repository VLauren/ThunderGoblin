using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuzRayoLejano : MonoBehaviour
{
    const float tiempoFijo = 2f;
    const float tiempoAleatorio = 4.5f;

    float tiempoDesteUltimoRayo = 0;
    float esperaSiguienteRayo;

    Light luz;
    
	void Start ()
    {
        luz = GetComponent<Light>();
        esperaSiguienteRayo = tiempoFijo + Random.value * tiempoAleatorio;
	}
	
	void Update ()
    {
        if(Time.time > tiempoDesteUltimoRayo + esperaSiguienteRayo)
        {
            StartCoroutine(Parpadeo());
            tiempoDesteUltimoRayo = Time.time;
            esperaSiguienteRayo = tiempoFijo + Random.value * tiempoAleatorio;
        }
    }

    IEnumerator Parpadeo()
    {
        Sonido.PlaySonido("Trueno");
        for (int i = 0; i < 3; i++)
        {
            luz.enabled = true;
            for (int j = 0; j < 3; j++)
                yield return null;
            luz.enabled = false;
            for (int j = 0; j < 3; j++)
                yield return null;
        }
    }
}
