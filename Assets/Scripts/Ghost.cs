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

    public int contRonda;
    int contFantasmas = 0;

    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
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

            if (Input.GetKeyDown(teclaDestruir))
            {
                // Mirar la dificultad del fantasma y en base a eso, sumar uno al contador de fantasmasdestruidos de dicha dificultad
                // En caso de haber destruido el total de fantasmas de esa dificultad, sumar 1 al contador de oleadas de dicha dificultad
                gM.player.gameObject.GetComponent<Player>().DestruirFantasma(gameObject);

                // Si el contador de oleadasfáciles es menor a 3 y el contador de fantasmasfácilespasados es 2, spawneo el siguiente de spawner de fáciles,
                // en cambio, si el contador de oleadasfáciles es 3 o superior, spawneo el primer spawner de oleadasnormales.
                
                //if (gameObject.transform.position.x == 48)
                //{
                //    gM.SpawnerPacks("hard");
                //}
                yield break;
            }

            yield return null;
        }

        if (tiempoRestante <= 0)
        {
            //if (gameObject.transform.position.x == 48)
            //{
            //    gM.SpawnerPacks("hard");
            //}

            gM.player.gameObject.GetComponent<Player>().vidas--;
            Debug.Log("No destruiste al fantasma a tiempo. Te queda " + gM.player.gameObject.GetComponent<Player>().vidas + " vida.");
        }


        gM.player.gameObject.GetComponent<Player>().ResetearEnfoque();
    }

    //Poner a todos los fondos un collider que al entrar en contacto con el jugador compruebe si ha terminado la ronda y si la ha terminado te spawnee los fantasmas con la logica de arriba.
    //Hacer un OnColliderExit que al salir le quite 1 vida, pero el problema viene en que si se destruye el fantasma tmb se va a salir y le va a quitar una vida cuando no debería, entonces activar antes una bool que se asegure de entrar si no ha sido activada, por lo que significará que ha fallado.
    //De esta forma se podrá contar el numero de fantasmas por el que se ha pasado, problema, que depende de la dificultad. (Puedo hacer que salga del triger del fantasma SIEMPRE, me aumente +1 en una variable que vaya definiendo en que ronda estamos. Ej, que si esta variable es == 2, spawnee la siguiente ronda comprobando que ronda toca, puedo usar lo que está arriba)

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            contFantasmas++;

            if (contRonda >= 0 && contRonda <= 3 && contFantasmas == 2)
            {
                gM.player.gameObject.GetComponent<GameManager>().SpawnerPacks("easy");
                gM.gameObject.GetComponent<GameManager>().SpawnerPacks("easy");
                gM.GetComponent<GameManager>().SpawnerPacks("easy");

                contFantasmas = 0;
                contRonda++;
            }

            else if (contRonda >= 4 && contRonda <= 7 && contFantasmas == 3)
            {
                gM.player.gameObject.GetComponent<GameManager>().SpawnerPacks("normal");
                contFantasmas = 0;
                contRonda++;
            }

            else if (contRonda >= 8 && contRonda <= 10 && contFantasmas == 4)
            {
                gM.gameObject.GetComponent<GameManager>().SpawnerPacks("hard");
                contFantasmas = 0;
                contRonda++;
            }

            else
            {
                //Victoria
            }
        }
    }

}
