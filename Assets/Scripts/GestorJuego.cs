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

    public GameObject prefabMuertoJ1;
    public GameObject prefabMuertoJ2;

    private float tiempoUltimoRayo = 0;
    private bool rayosActivos;
    private bool rondaAcabada = false;
    private GameObject muerto;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        Fade.In();
    }

    private void Update()
    {
        if(rayosActivos && Time.time - tiempoUltimoRayo > Global.PeriodoRayo)
        {
            tiempoUltimoRayo = Time.time;
            if(OnRayo != null)
                OnRayo();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (OnReload != null)
                OnReload();

            OnRayo = null;
            OnReload = null;

            SceneManager.UnloadScene("Juego");
            rondaAcabada = false;
            if (muerto != null)
                Destroy(muerto);

            SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
            Fade.In();
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
        if(!instance.rondaAcabada)
        {
            instance.rondaAcabada = true;
            instance.Invoke("FadeOut", 2f);
            instance.Invoke("ReiniciarEscena", 2.5f);
        }
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
        rondaAcabada = false;
        if (muerto != null)
            Destroy(muerto);
        
        Fade.In();
    }

    void FadeOut()
    {
        Fade.Out();
    }

    public static void CrearMuerto(Goblin.Jugador jugador, Vector3 posicion, Quaternion rotacion)
    {
        if (jugador == Goblin.Jugador.UNO)
            instance.muerto = Instantiate(instance.prefabMuertoJ1, posicion, rotacion).gameObject;
        if (jugador == Goblin.Jugador.DOS)
            instance.muerto = Instantiate(instance.prefabMuertoJ2, posicion, rotacion).gameObject;
    }
}
