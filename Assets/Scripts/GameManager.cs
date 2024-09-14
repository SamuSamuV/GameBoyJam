using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject derrotapanel;

    public void Reintentar()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Nivel1");
    }

    public void VolverMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuInicial");
    }
}