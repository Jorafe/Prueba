using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public GameObject[] estrellas;

    public static HUD instance;

    void Start()
    {
        foreach (GameObject moneda in estrellas)
        {
            moneda.SetActive(false);
        }
    }

    public void ActivarMoneda(int indice)
    {
        Debug.Log("Activando moneda en índice: " + indice);

        if (indice >= 0 && indice < estrellas.Length)
        {
            estrellas[indice].SetActive(true);
        }
    }

}
