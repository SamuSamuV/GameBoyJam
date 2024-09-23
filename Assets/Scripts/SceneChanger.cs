using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public string sceneName;

    public GameObject presentacion;
    public GameObject menu;
    //public string objectKey = "objectDeactivated";

    [SerializeField] private Animator animator;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        //if (PlayerPrefs.GetInt(objectKey, 0) == 1)
        //{
        //    presentacion.SetActive(false);
        //    menu.SetActive(true);
        //}
    }

    public void OnAnimationEnd()
    {
        presentacion.SetActive(false);

        menu.SetActive(true);

        //PlayerPrefs.SetInt(objectKey, 1);
        //PlayerPrefs.Save();
    }
}

