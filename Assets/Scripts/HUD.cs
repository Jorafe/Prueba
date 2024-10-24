using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject[] estrellas;

    public static HUD instance;

    void Awake()
    {
         if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        foreach (GameObject moneda in estrellas)
        {
            moneda.SetActive(false);
        }
    }

    public void ActivarEstrellas(int indice)
    {

        if (indice >= 0 && indice < estrellas.Length)
        {
            estrellas[indice].SetActive(true);
        }

    }

}
