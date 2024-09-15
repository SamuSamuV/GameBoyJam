using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawners : MonoBehaviour
{
    public GameManager gM;
    public string nombre;

    void Start()
    {
        gameObject.name = nombre;
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        Instantiate(gM.ghost, gameObject.transform);
    }
}
