using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button[] buttons; //aqui añado los botones en el unity
    private int selectedButtonIndex = 0;
    public static GameManager instance;
    public GameObject[] objectsToActivate;

<<<<<<< Updated upstream
    public Sprite defaultSprite;  
    public Sprite selectedSprite; 
=======
    [SerializeField] public GameObject derrotapanel;
>>>>>>> Stashed changes

    void Start()
    {
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) //bajar de botón
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
                selectedButtonIndex = buttons.Length - 1;
            SelectButton(selectedButtonIndex);
        }

        if (Input.GetKeyDown(KeyCode.S)) //subir de botón
        {
            selectedButtonIndex++;
            if (selectedButtonIndex >= buttons.Length)
                selectedButtonIndex = 0;
            SelectButton(selectedButtonIndex);
        }

        if (Input.GetKeyDown(KeyCode.J)) //si le doy a la J cambio de escena
        {
            LoadSceneFromSelectedButton();
        }
    }

    void SelectButton(int index) 
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().sprite = defaultSprite;
        }

        buttons[index].GetComponent<Image>().sprite = selectedSprite; 
    }

    void LoadSceneFromSelectedButton()
    {
        if (selectedButtonIndex == 0) // Primer botón: Cambia de escena
        {
            var sceneChanger = buttons[selectedButtonIndex].GetComponent<SceneChanger>();
            if (sceneChanger != null)
            {
                LoadScene(sceneChanger.sceneName);
            }
            else
            {
                Debug.LogError("El primer botón no tiene ninguna escena asignada.");
            }
        }
        else if (selectedButtonIndex == buttons.Length - 1) // Último botón: Cerrar el juego
        {
            QuitGame();
        }
        else // Otros botones: Activan un objeto
        {
            ActivateObjectForButton(selectedButtonIndex);
        }
    }

    void ActivateObjectForButton(int buttonIndex)
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        if (buttonIndex - 1 < objectsToActivate.Length)
        {
            objectsToActivate[buttonIndex - 1].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No hay objeto asignado para este botón.");
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("El juego se ha cerrado.");
        Application.Quit();
    }

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