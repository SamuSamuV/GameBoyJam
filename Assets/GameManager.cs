using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Button[] buttons; //aqui a�ado los botones en el unity
    private int selectedButtonIndex = 0;
    public static GameManager instance;
    public GameObject[] objectsToActivate;

    void Start()
    {
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) //bajar de bot�n
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
                selectedButtonIndex = buttons.Length - 1;
            SelectButton(selectedButtonIndex);
        }

        if (Input.GetKeyDown(KeyCode.S)) //subir de bot�n
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

    void SelectButton(int index) //esto tendr� que ver� como cambiar entre sprites cuando haya arte
    {
        foreach (var button in buttons)
        {
            button.GetComponent<Image>().color = Color.white;
        }

        buttons[index].GetComponent<Image>().color = Color.green;
    }

    void LoadSceneFromSelectedButton()
    {
        if (selectedButtonIndex == 0) // Primer bot�n: Cambia de escena
        {
            var sceneChanger = buttons[selectedButtonIndex].GetComponent<SceneChanger>();
            if (sceneChanger != null)
            {
                LoadScene(sceneChanger.sceneName);
            }
            else
            {
                Debug.LogError("El primer bot�n no tiene ninguna escena asignada.");
            }
        }
        else if (selectedButtonIndex == buttons.Length - 1) // �ltimo bot�n: Cerrar el juego
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
            Debug.LogWarning("No hay objeto asignado para este bot�n.");
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
}
