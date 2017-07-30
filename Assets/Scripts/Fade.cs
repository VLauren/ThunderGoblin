using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private static Image imagen;

	void Start ()
    {
        imagen = GetComponent<Image>();
        if (imagen)
        {
            imagen.CrossFadeAlpha(0, 0, false);
        }
    }
	
    public static void In()
    {
        if (imagen)
        {
            imagen.CrossFadeAlpha(1, 0, false);
            imagen.CrossFadeAlpha(0, 0.5f, false);
        }
    }

    public static void Out()
    {
        if(imagen)
        {
            imagen.CrossFadeAlpha(0, 0, false);
            imagen.CrossFadeAlpha(1, 0.5f, false);
        }
    }
}
