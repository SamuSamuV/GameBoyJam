using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class WinLostManager : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonSprites
    {
        public Sprite defaultSprite;
        public Sprite selectedSprite;
    }

    public Button[] buttons;
    public ButtonSprites[] buttonSprites;
    private int selectedButtonIndex = 0;
    public GameObject winPanel;
    public GameObject lostPanel;

    void Update()
    {
        // Navegación de botones
        if (Input.GetKeyDown(KeyCode.A) && (winPanel.activeSelf || lostPanel.activeSelf)) // Subir de botón
        {
            selectedButtonIndex--;
            if (selectedButtonIndex < 0)
                selectedButtonIndex = buttons.Length - 1;
            SelectButton(selectedButtonIndex);
        }

        if (Input.GetKeyDown(KeyCode.D) && (winPanel.activeSelf || lostPanel.activeSelf)) // Bajar de botón
        {
            selectedButtonIndex++;
            if (selectedButtonIndex >= buttons.Length)
                selectedButtonIndex = 0;
            SelectButton(selectedButtonIndex);
        }

        // Acciones de los botones
        if (Input.GetKeyDown(KeyCode.J) && (winPanel.activeSelf || lostPanel.activeSelf)) // Si le damos a J, cambia de escena
        {
            LoadSceneFromSelectedButton();
        }
    }

    void LoadSceneFromSelectedButton()
    {
        if (selectedButtonIndex == 0 || selectedButtonIndex == 2) // Primer botón: Reinicia la escena
        {
            LoadScene("Nivel1");

        }
        else if (selectedButtonIndex == 1 || selectedButtonIndex == 3) // Último botón: Va al menu
        {
            LoadScene("MenuInicial");
        }

    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    IEnumerator Reiniciar(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    IEnumerator Menu(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    void SelectButton(int index)
    {
        // Cambiamos todos los botones a su sprite default
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponent<Image>().sprite = buttonSprites[i].defaultSprite;
        }

        // Cambiamos el sprite del botón seleccionado
        buttons[index].GetComponent<Image>().sprite = buttonSprites[index].selectedSprite;
    }
}
