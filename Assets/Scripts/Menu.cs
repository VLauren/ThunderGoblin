using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool iniciado;

    public GameObject pantalla1;
    public GameObject pantalla2;

    private void Start()
    {
        iniciado = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !iniciado)
        {
            if(pantalla1.activeSelf)
            {
                pantalla1.SetActive(false);
                pantalla2.SetActive(true);
            }
            else
            {
                iniciado = true;
                StartCoroutine(Iniciar());
            }
        }
    }

    IEnumerator Iniciar()
    {
        Fade.Out();

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene("Juego", LoadSceneMode.Additive);
        SceneManager.UnloadScene("Menu");
        GestorJuego.ActivarRayos();
        Fade.In();
        Sonido.IniciarMusica();
    }
}
