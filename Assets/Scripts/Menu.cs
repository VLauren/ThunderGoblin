using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    bool iniciado;

    private void Start()
    {
        iniciado = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown && !iniciado)
        {
            iniciado = true;
            StartCoroutine(Iniciar());
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
    }
}
