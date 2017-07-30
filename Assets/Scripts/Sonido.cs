using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonido : MonoBehaviour
{
    public static Sonido instancia { get; private set; }

    private Dictionary<string,AudioSource> sources;

    static float volumenFX = 0.6f;

    private void Awake()
    {
        instancia = this;

        sources = new Dictionary<string, AudioSource>();
    }

    public void Play(string sonido, float vol = 1)
    {
        GetComponent<AudioSource>().PlayOneShot(Resources.Load("FX/" + sonido) as AudioClip, vol);
    }

    public static void PlaySonido(string sonido, float vol = 1)
    {
        if (!instancia.sources.ContainsKey(sonido))
            instancia.sources.Add(sonido, instancia.gameObject.AddComponent<AudioSource>());
        AudioSource fuente = instancia.sources[sonido];

        if (sonido == "Rodar1")
            fuente.PlayOneShot(Resources.Load("FX/goblin1_rueda" + Random.Range(1, 5)) as AudioClip, vol * volumenFX);
        if (sonido == "Rodar2")
            fuente.PlayOneShot(Resources.Load("FX/goblin2_rueda" + Random.Range(1, 5)) as AudioClip, vol * volumenFX);
        if (sonido == "Rayo")
            fuente.PlayOneShot(Resources.Load("FX/rayo" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Pararrayos")
            fuente.PlayOneShot(Resources.Load("FX/pararrayos_cae" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Lanza")
            fuente.PlayOneShot(Resources.Load("FX/lanza" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Morir1")
            fuente.PlayOneShot(Resources.Load("FX/goblin1_muere" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Morir2")
            fuente.PlayOneShot(Resources.Load("FX/goblin2_muere" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Circulo")
            fuente.PlayOneShot(Resources.Load("FX/circulo_electrico" + Random.Range(1, 3)) as AudioClip, vol * volumenFX);
        if (sonido == "Trueno")
            fuente.PlayOneShot(Resources.Load("FX/trueno") as AudioClip, vol * volumenFX);
        if (sonido == "Paso1")
            fuente.PlayOneShot(Resources.Load("FX/paso" + Random.Range(1, 9)) as AudioClip, vol * volumenFX);
        if (sonido == "Paso2")
            fuente.PlayOneShot(Resources.Load("FX/paso" + Random.Range(1, 9)) as AudioClip, vol * volumenFX);

    }

    public static void IniciarMusica()
    {
        instancia.transform.Find("Musica").GetComponent<AudioSource>().Play();
    }

    public static void PararMusica()
    {
        instancia.transform.Find("Musica").GetComponent<AudioSource>().Stop();
    }
}
