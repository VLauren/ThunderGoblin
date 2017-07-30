using UnityEngine;
using System.Collections;

public class TTL : MonoBehaviour
{
    public float tiempo = 1;
    private float tInicio;

	void Start ()
    {
        tInicio = Time.time;
	    //Invoke("Destruir",tiempo);
	}
	
	void Destruir ()
    {
        Destroy(gameObject);
	}

    private void Update()
    {
        if (Time.time > tInicio + tiempo)
            Destruir();
    }
}
