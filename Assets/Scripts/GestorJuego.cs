using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorJuego : MonoBehaviour
{
    public delegate void VoidCall();

    public static GestorJuego instance { get; private set; }

    public VoidCall OnRayo;
    public VoidCall OnReload;

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
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
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

    public void ReiniciarEscena()
    {
        if (OnReload != null)
            OnReload();

        OnRayo = null;
        OnReload = null;

        SceneManager.UnloadScene("Juego");
        SceneManager.LoadScene("Juego", LoadSceneMode.Additive);
        ActivarRayos();
    }
}
