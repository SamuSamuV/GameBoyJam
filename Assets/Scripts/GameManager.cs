using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject derrotapanel;
    [SerializeField] public GameObject victoriapanel;
    [SerializeField] public GameObject ghost;
    [SerializeField] public GameObject[] ghostSpawners; // Habrá 4 en dificultad máxima.
    [SerializeField] public Sprite[] ghostImages;       // Tienen imágenes diferentes según el tipo.
    [SerializeField] public List<GameObject> currentGhostSpawners; // Los Spawners actuales.
    [SerializeField] public GameObject player;
    [SerializeField] public Transform playerTransform;

    [SerializeField] public RuntimeAnimatorController[] animatorControllers;

    public bool seTerminoPartida = false;

    public int contRonda;
    public int contFantasmas = 0;

    private void Start()
    {
        Time.timeScale = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>().transform;

        SpawnerPacks("easy");
        contRonda++;

        seTerminoPartida = false;

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


    public void SpawnerPacks(string difficulty)
    {
        int rnd = Random.Range(1, 4);
        GameObject s1, s2, s3, s4;

        if (difficulty == "easy")
        {
            if (rnd == 1)
            {
                s1 = GhostSpawnersCreation("Paper", 1, "arriba");
                s2 = GhostSpawnersCreation("Paper", 2, "izquierda");
            }

            else if (rnd == 2)
            {
                s1 = GhostSpawnersCreation("Paper", 1, "arriba");
                s2 = GhostSpawnersCreation("Scissors", 2, "abajo");
            }

            else if (rnd == 3)
            {
                s1 = GhostSpawnersCreation("Paper", 1, "left");
                s2 = GhostSpawnersCreation("Rock", 2, "arriba");
            }

            else if (rnd == 4)
            {
                s1 = GhostSpawnersCreation("Scissors", 1, "izquierda");
                s2 = GhostSpawnersCreation("Paper", 2, "arriba");
            }

            for (int i = 0; i < currentGhostSpawners.Count; i++)
            {
                currentGhostSpawners[i].GetComponent<Spawners>().nombre += "Easy";
            }
        }

        else if (difficulty == "normal")
        {
            if (rnd == 1)
            {
                s1 = GhostSpawnersCreation("Rock", 1, "abajo");
                s2 = GhostSpawnersCreation("Rock", 2, "izquierda");
                s3 = GhostSpawnersCreation("Scissors", 3, "abajo");
            }

            else if (rnd == 2)
            {
                s1 = GhostSpawnersCreation("Paper", 1, "arriba");
                s2 = GhostSpawnersCreation("Rock", 2, "arriba");
                s3 = GhostSpawnersCreation("Paper", 3, "izquierda");
            }

            else if (rnd == 3)
            {
                s1 = GhostSpawnersCreation("Scissors", 1, "abajo");
                s2 = GhostSpawnersCreation("Paper", 2, "arriba");
                s3 = GhostSpawnersCreation("Rock", 3, "abajo");
            }

            else if (rnd == 4)
            {
                s1 = GhostSpawnersCreation("Scissors", 1, "izquierda");
                s2 = GhostSpawnersCreation("Paper", 2, "arriba");
                s3 = GhostSpawnersCreation("Scissors", 3, "abajo");
            }

            else if (rnd == 5)
            {
                s1 = GhostSpawnersCreation("Paper", 1, "arriba");
                s2 = GhostSpawnersCreation("Scissors", 2, "arriba");
                s3 = GhostSpawnersCreation("Rock", 3, "abajo");
            }

            for (int i = 0; i < currentGhostSpawners.Count; i++)
            {
                currentGhostSpawners[i].GetComponent<Spawners>().nombre += "Normal";
            }
        }

        else if (difficulty == "hard")
        {
            if (rnd == 1)
            {
                s1 = GhostSpawnersCreation("Scissors", 1, "arriba");
                s2 = GhostSpawnersCreation("Rock", 2, "abajo");
                s3 = GhostSpawnersCreation("Scissors", 3, "abajo");
                s4 = GhostSpawnersCreation("Paper", 4, "izquierda");
            }

            else if (rnd == 2)
            {
                s1 = GhostSpawnersCreation("Rock", 1, "abajo");
                s2 = GhostSpawnersCreation("Rock", 2, "arriba");
                s3 = GhostSpawnersCreation("Scissors", 3, "izquierda");
                s4 = GhostSpawnersCreation("Paper", 4, "arriba");
            }

            else if (rnd == 3)
            {
                s1 = GhostSpawnersCreation("Rock", 1, "izquierda");
                s2 = GhostSpawnersCreation("Paper", 2, "izquierda");
                s3 = GhostSpawnersCreation("Scissors", 3, "abajo");
                s4 = GhostSpawnersCreation("Paper", 4, "arriba");
            }

            else if (rnd == 4)
            {
                s1 = GhostSpawnersCreation("Scissors", 1, "abajo");
                s2 = GhostSpawnersCreation("Paper", 2, "arriba");
                s3 = GhostSpawnersCreation("Rock", 3, "abajo");
                s4 = GhostSpawnersCreation("Paper", 4, "izquierda");
            }

            else if (rnd == 5)
            {
                s1 = GhostSpawnersCreation("Rock", 1, "arriba");
                s2 = GhostSpawnersCreation("Scissors", 2, "izquierda");
                s3 = GhostSpawnersCreation("Rock", 3, "izquierda");
                s4 = GhostSpawnersCreation("Rock", 4, "arriba");
            }

            for (int i = 0; i < currentGhostSpawners.Count; i++)
            {
                currentGhostSpawners[i].GetComponent<Spawners>().nombre += "Hard";
            }
        }
    }

    public GameObject GhostSpawnersCreation(string tipo, int orden, string direccion)
    {
        Vector3 spawnPos;
        int disMin = 12;
        float posX, posY;
        GameObject clon;

        if (direccion == "arriba")
        {
            posX = playerTransform.position.x + disMin * orden;
            posY = playerTransform.position.y + 5;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[orden - 1], spawnPos, Quaternion.Euler(0, 0, -180));
            clon.GetComponent<Spawners>().nombre += tipo;
            clon.GetComponent<Spawners>().nombre += "Up";
            currentGhostSpawners.Add(clon);

            return clon;
        }

        else if (direccion == "abajo")
        {
            posX = playerTransform.position.x + disMin * orden;
            posY = playerTransform.position.y - 5;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[orden - 1], spawnPos, Quaternion.identity);
            clon.GetComponent<Spawners>().nombre += tipo;
            clon.GetComponent<Spawners>().nombre += "Down";
            currentGhostSpawners.Add(clon);

            return clon;
        }

        else
        {
            posX = playerTransform.position.x + disMin * orden;
            posY = playerTransform.position.y;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[orden - 1], spawnPos, Quaternion.Euler(0, 0, -90));
            clon.GetComponent<Spawners>().nombre += tipo;
            clon.GetComponent<Spawners>().nombre += "Left";
            currentGhostSpawners.Add(clon);

            return clon;
        }
    }
}