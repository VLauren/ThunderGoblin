using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJuego : MonoBehaviour
{
    public delegate void OnRayoCall();

    public static GestorJuego instance { get; private set; }

    public OnRayoCall OnRayo;

    public GameObject prefabProyectil;
    public GameObject prefabCirculo;
    public GameObject prefabPararrayos;

    public GameObject prefabRayoA;
    public GameObject prefabRayoB;

    private float tiempoUltimoRayo = 0;
    private bool rayosActivos;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // por ahora se activa na mas empezar
        ActivarRayos();
    }

    private void Update()
    {
        if(rayosActivos && Time.time - tiempoUltimoRayo > Global.PeriodoRayo)
        {
            tiempoUltimoRayo = Time.time;
            if(OnRayo != null)
                OnRayo();
        }
    }

    public static void ActivarRayos()
    {
        instance.rayosActivos = true;
    }

    public static void DesactivarRayos()
    {
        instance.rayosActivos = false;
    }

    public static void TerminarRonda()
    {
        instance.Invoke("ReiniciarEscena", 1);
    }

    // HACK
    public void ReiniciarEscena()
    {
        SceneManager.LoadScene(0);
        ActivarRayos();
    }
}
