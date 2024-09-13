using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] arrayFantasmas;
    [SerializeField] Transform transformSpawner;
    GameObject fantasmaSpawneado;

    void Start()
    {
        spawnearFantasma();
    }

    void Update()
    {
        
    }

    void spawnearFantasma()
    {
        int random = Random.Range(0, arrayFantasmas.Length);
        fantasmaSpawneado = arrayFantasmas[random];

        Instantiate(fantasmaSpawneado, transformSpawner.position, Quaternion.identity);
    }
}