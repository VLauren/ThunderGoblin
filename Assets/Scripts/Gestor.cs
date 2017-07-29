using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestor : MonoBehaviour
{
    public delegate void OnRayoCall();

    public static Gestor instance { get; private set; }

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
}
