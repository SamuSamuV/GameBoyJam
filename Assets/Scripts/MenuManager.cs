using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Estructura para guardar el sprite default y el sprite seleccionado de cada botón
    [System.Serializable]
    public struct ButtonSprites
    {
        public Sprite defaultSprite;
        public Sprite selectedSprite;
    }

    public AudioClip sonidoCambiarBoton;
    public AudioClip sonidoDarBoton;
    private AudioSource audioSource;

    public Button[] buttons; // Aquí añades los botones en Unity
    public ButtonSprites[] buttonSprites; // Aquí añades los sprites para cada botón
    private int selectedButtonIndex = 0;
    public static GameManager instance;
    public GameObject[] objectsToActivate;

    public GameObject menu;
    public GameObject credits;
    public GameObject howplay;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Verificamos que haya sprites asignados para todos los botones
        if (buttonSprites.Length != buttons.Length)
        {
            Debug.LogError("El número de botones y sprites asignados no coinciden.");
            return;
        }

        // Seleccionamos el primer botón
        SelectButton(selectedButtonIndex);
    }

    void Update()
    {
        // Navegación de botones
        if (Input.GetKeyDown(KeyCode.W)) // Subir de botón
        {
            if (menu.activeSelf)
            {
                audioSource.clip = sonidoCambiarBoton;
                audioSource.Play();
                selectedButtonIndex--;
                if (selectedButtonIndex < 0)
                    selectedButtonIndex = buttons.Length - 1;
                SelectButton(selectedButtonIndex);

            }

        }

        if (Input.GetKeyDown(KeyCode.S)) // Bajar de botón
        {
            if (menu.activeSelf)
            {
                audioSource.clip = sonidoCambiarBoton;
                audioSource.Play();
                selectedButtonIndex++;
                if (selectedButtonIndex >= buttons.Length)
                    selectedButtonIndex = 0;
                SelectButton(selectedButtonIndex);

            }

        }

        // Acciones de los botones
        if (Input.GetKeyDown(KeyCode.J)) // Si le damos a J, cambia de escena
        {
            if (menu.activeSelf)
            {
                audioSource.clip = sonidoDarBoton; 
                audioSource.Play();
                menu.SetActive(false);
                LoadSceneFromSelectedButton();

            }
 
            
        }

        if (Input.GetKeyDown(KeyCode.K)) // Si le damos a K, volvemos al menú
        {
            if (!menu.activeSelf)
            {
                audioSource.clip = sonidoDarBoton;
                audioSource.Play();
                credits.SetActive(false);
                howplay.SetActive(false);
                StartCoroutine(ActivarMenu(1.0f));

            }


        }
    }

    IEnumerator ActivarMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        menu.SetActive(true);
    }



    // Función para seleccionar el botón actual y cambiar su sprite
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

    // Función para cargar la escena desde el botón seleccionado
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
        else if (selectedButtonIndex == buttons.Length - 1) // Último botón: Cierra el juego
        {
            QuitGame();
        }
        else // Otros botones: Activan un objeto
        {
            ActivateObjectForButton(selectedButtonIndex);
        }
    }

    // Activar objetos según el botón seleccionado
    void ActivateObjectForButton(int buttonIndex)
    {
        foreach (var obj in objectsToActivate)
        {
            obj.SetActive(false);
        }

        if (buttonIndex - 1 < objectsToActivate.Length)
        {
            menu.SetActive(false);
            StartCoroutine(ActivarObjeto(1f));

        }
        else
        {
            Debug.LogWarning("No hay objeto asignado para este botón.");
        }

        IEnumerator ActivarObjeto(float delay)
        {
            yield return new WaitForSeconds(delay);
            objectsToActivate[buttonIndex - 1].SetActive(true);
        }
    }

    // Cargar la escena
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Cerrar el juego
    public void QuitGame()
    {
        Debug.Log("El juego se ha cerrado.");
        Application.Quit();
    }
}
