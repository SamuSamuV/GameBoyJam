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
    [SerializeField] public Sprite[] ayuditas;
    [SerializeField] public GameObject manual;

    public bool seTerminoPartida = false;

    public int contRonda;
    public int contFantasmas = 0;


    private void Start()
    {
        Time.timeScale = 1;

        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>().transform;

        SpawnerPacks("Easy");
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

        if (difficulty == "Easy")
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
        }

        else if (difficulty == "Normal")
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
        }

        else if (difficulty == "Hard")
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
        }

        for (int i = 0; i < currentGhostSpawners.Count; i++)
        {
            currentGhostSpawners[i].GetComponent<Ghost>().gameObject.name += difficulty;
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
            posY = playerTransform.position.y + 7;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[0], spawnPos, Quaternion.identity);
            clon.gameObject.name = tipo;
            clon.gameObject.name += "Up";
            currentGhostSpawners.Add(clon);
            clon.GetComponent<Animator>().SetBool("StandBy", true);
            clon.transform.GetChild(0).GetComponent<Animator>().SetBool("StandBy", true);


            return clon;
        }

        else if (direccion == "abajo")
        {
            posX = playerTransform.position.x + disMin * orden;
            posY = playerTransform.position.y - 5;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[1], spawnPos, Quaternion.identity);
            clon.gameObject.name = tipo;
            clon.gameObject.name += "Down";
            currentGhostSpawners.Add(clon);
            clon.GetComponent<Animator>().SetBool("StandBy", true);
            clon.transform.GetChild(0).GetComponent<Animator>().SetBool("StandBy", true);

            return clon;
        }

        else
        {
            posX = playerTransform.position.x + disMin * orden;
            posY = playerTransform.position.y;

            spawnPos = new Vector2(posX, posY);

            clon = Instantiate(ghostSpawners[2], spawnPos, Quaternion.identity);
            clon.gameObject.name = tipo;
            clon.gameObject.name += "Left";
            currentGhostSpawners.Add(clon);
            clon.GetComponent<Animator>().SetBool("StandBy", true);
            clon.transform.GetChild(0).GetComponent<Animator>().SetBool("StandBy", true);

            return clon;
        }
    }

    public void SettingHands(string handType, GameObject ghost)
    {

        if (handType == "Roca")
        {
            ghost.GetComponent<Animator>().SetBool("Rock", true);
            ghost.transform.GetChild(0).GetComponent<Animator>().SetBool("Rock", true);
        }

        else if (handType == "Tijeras")
        {
            ghost.GetComponent<Animator>().SetBool("Scissors", true);
            ghost.transform.GetChild(0).GetComponent<Animator>().SetBool("Scissors", true);
        }

        else if (handType == "Papel")
        {
            ghost.GetComponent<Animator>().SetBool("Paper", true);
            ghost.transform.GetChild(0).GetComponent<Animator>().SetBool("Paper", true);
        }
    }

    public void PlayerDamage()
    {
        player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().hurtSound;
        player.GetComponent<Player>().audioSourcePlayer.Play();

        player.GetComponent<Player>().vidas--;

        if (player.gameObject.GetComponent<Player>().vidas == 2)
            player.GetComponent<Player>().animatorPlayer.runtimeAnimatorController = animatorControllers[1];

        else if (player.gameObject.GetComponent<Player>().vidas == 1)
            player.GetComponent<Player>().animatorPlayer.runtimeAnimatorController = animatorControllers[0];

        player.gameObject.GetComponent<Player>().ResetearEnfoque();
    }
}