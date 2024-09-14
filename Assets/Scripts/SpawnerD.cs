using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerD : MonoBehaviour
{
    [SerializeField] GameObject[] arrayFantasmas;
    [SerializeField] GameObject jugador;
    [SerializeField] Transform transformSpawner;
    GameObject fantasmaSpawneado;

    void Start()
    {
        spawnearFantasma();
        jugador = GameObject.FindGameObjectWithTag("Player");
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

    public IEnumerator IniciarTemporizador(float tiempo, GameObject fantasma, KeyCode teclaDestruir)
    {
        Debug.Log("Temporizador iniciado. Tienes " + tiempo + " segundos para destruir el fantasma o vas a mamar.");
        float tiempoRestante = tiempo;

        while (tiempoRestante > 0 && fantasma != null)
        {
            tiempoRestante -= Time.deltaTime;

            if (Input.GetKeyDown(teclaDestruir))
            {
                jugador.gameObject.GetComponent<Player>().DestruirFantasma(fantasma);
                yield break;
            }

            yield return null;
        }

        if (tiempoRestante <= 0)
        {
            jugador.gameObject.GetComponent<Player>().vidas--;
            Debug.Log("No destruiste al fantasma a tiempo. Te queda " + jugador.gameObject.GetComponent<Player>().vidas + " vida.");
        }

        jugador.gameObject.GetComponent<Player>().ResetearEnfoque();
    }
}
