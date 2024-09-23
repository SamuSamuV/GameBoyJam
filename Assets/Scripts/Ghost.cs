using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour
{
    [SerializeField] public GameManager gM; // Referencia al GM, que es el que les ir� dando todo cuando spawneen.

    [SerializeField] public string tipo;        // Pueden ser Piedra, Papel o Tijera.
    [SerializeField] public char dificultad;  // Pueden ser F�ciles, Normales o Dif�ciles.
    [SerializeField] public float tiempo;       // Pueden durar 2, 3 o 5 segundos.
    [SerializeField] public Animator anim;      // Tienen diferentes animaciones dependiendo del tipo.
    [SerializeField] public GameObject papa;      // Tienen diferentes animaciones dependiendo del tipo.
    [SerializeField] public GameObject player;
    [SerializeField] public int contadorVecesMorido = 0;
    [SerializeField] public bool fantasmaDestruido = false;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.tag = "Fantasma";
        WhoAmI();

        //Animator animator = gameObject.GetComponent<Animator>();
        //if (animator != null) animator = anim;
    }
    public void WhoAmI()
    {
        MyTypeIs();
        MyDifficultIs();
    }

    public void MyTypeIs()
    {
        if (gameObject.name.Contains("Rock"))
            tipo = "Roca";

        else if (gameObject.name.Contains("Paper"))
            tipo = "Papel";

        else if (gameObject.name.Contains("Scissors"))
            tipo = "Tijeras";
    }

    public void MyDifficultIs()
    {
        if (gameObject.name.Contains("Easy"))
        {
            dificultad = 'f';
            tiempo = 5;
        }

        else if (gameObject.name.Contains("Normal"))
        {
            dificultad = 'n';
            tiempo = 3;
        }

        else if (gameObject.name.Contains("Hard"))
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

                    Debug.Log("Acertaste");

                    gM.player.gameObject.GetComponent<Player>().DestruirFantasma(gameObject);
                    player.GetComponent<Player>().vidas++;
                    fantasmaDestruido = true;
                    gM.ComprobarAnim();
                    yield break;
                }

                else
                {
                    ToyMuerto();
                    Debug.Log("Tecla incorrecta. Te queda " + gM.player.gameObject.GetComponent<Player>().vidas + " vida.");
                }
            }

            yield return null;
        }

        if (tiempoRestante <= 0)
        {
            ToyMuerto();
            Debug.Log("No destruiste al fantasma a tiempo. Te queda " + gM.player.gameObject.GetComponent<Player>().vidas + " vida.");
        }

        gM.player.gameObject.GetComponent<Player>().ResetearEnfoque();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColliderAnimFantasma"))
        {
            gM.SettingHands(tipo, gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!player.GetComponent<Player>().seEnfocoFantasmaPapel && !player.GetComponent<Player>().seEnfocoFantasmaPiedra
                && !player.GetComponent<Player>().seEnfocoFantasmaTijera && !fantasmaDestruido)
            {
                ToyMuerto();
                Debug.Log("No destruiste al fantasma. Te queda " + player.GetComponent<Player>().vidas + " vida.");
            }
        }
    }

    public void ComprobarRonda()
    {
        if (player.GetComponent<Player>().vidas > 0)
        {
            if (gM.contFantasmas == 2)
            {
                if (gM.contRonda < 2)
                {
                    gM.contFantasmas = 0;
                    gM.contRonda++;
                    gM.SpawnerPacks("Easy");
                }

                else if (gM.contRonda == 2)
                {
                    gM.contFantasmas = 0;
                    gM.contRonda++;
                    gM.SpawnerPacks("Normal");
                }
            }

            else if (gM.contFantasmas == 3)
            {
                if (gM.contRonda < 6)
                {
                    gM.contFantasmas = 0;
                    gM.contRonda++;
                    gM.SpawnerPacks("Normal");
                }

                else if (gM.contRonda == 6)
                {
                    gM.contFantasmas = 0;
                    gM.contRonda++;
                    gM.SpawnerPacks("Hard");
                }
            }

            else if (gM.contFantasmas == 4)
            {
                if (gM.contRonda < 10)
                {
                    gM.contFantasmas = 0;
                    gM.contRonda++;
                    gM.SpawnerPacks("Hard");
                }

                else if (gM.contRonda == 10)
                {
                    Debug.Log("GANASTE DE MANERA BIEN SABROSONAAAAAAAAAAAAAAAA");

                    player.GetComponent<Player>().audioSourcePlayer.clip = player.GetComponent<Player>().victoriaSound;
                    player.GetComponent<Player>().audioSourcePlayer.Play();

                    gM.victoriapanel.SetActive(true);
                    Time.timeScale = 0;
                    gM.seTerminoPartida = true;
                    StartCoroutine(Creditos());
                }
            }
        }
    }
    IEnumerator Creditos()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("CreditosFinal");
    }
    public void ToyMuerto()
    {
        contadorVecesMorido++;

        if (contadorVecesMorido == 1)
        {
            Debug.Log("Luis Rubio es como el corchopan");
            gM.contFantasmas++;
            gM.PlayerDamage();

            if (player.GetComponent<Player>().vidas > 0)
            {
                gM.currentGhostSpawners.Remove(this.gameObject);
                player.gameObject.GetComponent<Player>().ResetearEnfoque();
                ComprobarRonda();
                Destroy(gameObject);
                gM.manual.SetActive(false);
            }
        }
    }
}
