using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScreen : MonoBehaviour
{

    public static loadScreen Instancia { get; private set; }

    public Image imageDeCarga;
    public Image imageDeMov;
    public Image pantallaFinal;
    [Range(0.01f, 0.1f)]
    public float velocidadAparecer = 0.5f;
    [Range(0.01f, 0.1f)]
    public float velocidadOcultar = 0.5f;

    void Awake()
    {
        DefinirSingleton();
    }

    private void DefinirSingleton()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(this);
            imageDeCarga.gameObject.SetActive(false);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarEscena(string nombreEscena)
    {
        if (!imageDeCarga.gameObject.activeSelf) StartCoroutine(MostrarPantallaDeCarga(nombreEscena));
    }


    private IEnumerator MostrarPantallaDeCarga(string nombreEscena)
    {
        pantallaFinal.gameObject.SetActive(true);
        imageDeCarga.gameObject.SetActive(true);
        Color c = pantallaFinal.color;
        c.a = 0.0f;
        //Mientras no esté totalmente visible va aumentando su visibilidad
        while (c.a < 1)
        {
            pantallaFinal.color = c;
            c.a += velocidadAparecer;
            yield return null;
        }

        c = imageDeCarga.color;
        c.a = 0.0f;
        //Mientras no esté totalmente visible va aumentando su visibilidad
        while (c.a < 1)
        {
            imageDeCarga.color = c;
            c.a += velocidadAparecer;
            yield return null;
        }

        imageDeMov.gameObject.SetActive(true);
       

        c.a = 0.0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(nombreEscena);
        while (!operation.isDone)
        {
            imageDeMov.color = c;
            c.a += 0.1f;
            yield return null;
        }
        //Espera a que haya cargado la nueva escena
        while (!nombreEscena.Equals(SceneManager.GetActiveScene().name))
        {
            yield return null;
        }
        
        c = imageDeCarga.color;
        c.a = 1.0f;
        while (c.a > 0)
        {
            pantallaFinal.color = c;
            imageDeMov.color = c;
            imageDeCarga.color = c;
            c.a -= velocidadOcultar * 2;
            yield return null;
        }

        c = pantallaFinal.color;
        c.r = c.g = c.b = 0.0f;
        pantallaFinal.color = c;

        imageDeMov.gameObject.SetActive(false);
        imageDeCarga.gameObject.SetActive(false);
        pantallaFinal.gameObject.SetActive(false);
    }
}
