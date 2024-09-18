using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] public GameManager gM; // Referencia al GM, que es el que les irá dando todo cuando spawneen.

    [SerializeField] public string tipo;        // Pueden ser Piedra, Papel o Tijera.
    [SerializeField] public char dificultad;  // Pueden ser Fáciles, Normales o Difíciles.
    [SerializeField] public float tiempo;       // Pueden durar 2, 3 o 5 segundos.
    [SerializeField] public Animator anim;      // Tienen diferentes animaciones dependiendo del tipo.
    [SerializeField] public GameObject papa;      // Tienen diferentes animaciones dependiendo del tipo.
    [SerializeField] public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Fantasma";
        papa = gameObject.transform.parent.gameObject;
        WhoAmI();

        //Animator animator = gameObject.GetComponent<Animator>();
        //if (animator != null) animator = anim;
    }
    public void WhoAmI()
    {
        gameObject.name = papa.name;
        MyTypeIs();
        MyDifficultIs();
    }

    public void MyTypeIs()
    {
        if (papa.name.Contains("Rock"))
        {
            tipo = "Roca";
            gameObject.GetComponent<SpriteRenderer>().sprite = gM.ghostImages[0];
        }

        else if (papa.name.Contains("Paper"))
        {
            tipo = "Papel";
            gameObject.GetComponent<SpriteRenderer>().sprite = gM.ghostImages[1];
        }

        else if (papa.name.Contains("Scissors"))
        {
            tipo = "Tijeras";
            gameObject.GetComponent<SpriteRenderer>().sprite = gM.ghostImages[2];
        }
    }

    public void MyDifficultIs()
    {
        if (papa.name.Contains("Easy"))
        {
            dificultad = 'f';
            tiempo = 5;
        }

        else if (papa.name.Contains("Normal"))
        {
            dificultad = 'n';
            tiempo = 3;
        }

        else if (papa.name.Contains("Hard"))
        {
            dificultad = 'd';
            tiempo = 2;
        }
    }

    public IEnumerator IniciarTemporizador(KeyCode teclaDestruir)
    {
        Debug.Log("Temporizador iniciado. Tienes " + tiempo + " segundos para destruirme o valiste");
        float tiempoRestante = tiempo;

        while (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) 
                || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
            {
                if (Input.GetKeyDown(teclaDestruir))
                {
                    player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().ghostDieSound;
                    player.GetComponent<Player>().audioSourcePlayer.Play();

                    gM.player.gameObject.GetComponent<Player>().DestruirFantasma(gameObject);

                    // Lógica de oleadas, dependiendo de la dificultad
                    yield break;
                }

                else
                {
                    player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().hurtSound;
                    player.GetComponent<Player>().audioSourcePlayer.Play();

                    gM.player.gameObject.GetComponent<Player>().vidas--;
                    Debug.Log("Tecla incorrecta. Te queda " + gM.player.gameObject.GetComponent<Player>().vidas + " vida.");
                }
            }

            yield return null;
        }

        if (tiempoRestante <= 0)
        {
            player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().hurtSound;
            player.GetComponent<Player>().audioSourcePlayer.Play();

            // Cuando el tiempo se acaba y no destruyen al fantasma
            gM.player.gameObject.GetComponent<Player>().vidas--;
            Debug.Log("No destruiste al fantasma a tiempo. Te queda " + gM.player.gameObject.GetComponent<Player>().vidas + " vida.");
        }

        // Reinicia el enfoque del jugador después del temporizador
        gM.player.gameObject.GetComponent<Player>().ResetearEnfoque();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.GetComponent<Player>().seEnfocoFantasmaPapel && !player.GetComponent<Player>().seEnfocoFantasmaPiedra
                && !player.GetComponent<Player>().seEnfocoFantasmaTijera)
            {
                player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().hurtSound;
                player.GetComponent<Player>().audioSourcePlayer.Play();

                player.GetComponent<Player>().vidas--;
                Debug.Log("No destruiste al fantasma. Te queda " + player.GetComponent<Player>().vidas + " vida.");
            }

            gM.contFantasmas++;
            Debug.Log("Entré papu");

            if (gM.contRonda >= 0 && gM.contRonda <= 2 && gM.contFantasmas == 2)
            {
                gM.SpawnerPacks("easy");

                gM.contFantasmas = 0;
                gM.contRonda++;

                if (gM.contRonda == 3)
                    gM.contFantasmas++;
            }

            else if (gM.contRonda >= 3 && gM.contRonda <= 6 && gM.contFantasmas == 3)
            {
                gM.SpawnerPacks("normal");

                gM.contFantasmas = 0;
                gM.contRonda++;

                if (gM.contRonda == 7)
                    gM.contFantasmas++;
            }

            else if (gM.contRonda >= 7 && gM.contRonda <= 9 && gM.contFantasmas == 4)
            {
                gM.SpawnerPacks("hard");

                gM.contFantasmas = 0;
                gM.contRonda++;

                if (gM.contRonda == 10)
                    gM.contFantasmas++;
            }

            else if (gM.contRonda >= 10 && gM.contFantasmas == 5)
            {
                Debug.Log("GANASTE DE MANERA BIEN SABROSONAAAAAAAAAAAAAAAA");

                player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().victoriaSound;
                player.GetComponent<Player>().audioSourcePlayer.Play();

                gM.victoriapanel.SetActive(true);
                Time.timeScale = 0;
                gM.seTerminoPartida = true;
                //Victoria
            }
        }
    }
}
