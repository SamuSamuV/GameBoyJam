using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField] Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name.Contains("Rock"))
        {
            player.seEnfocoFantasmaPiedra = true;

            Debug.Log("Presiona de nuevo la dirección a donde estás mirando para destruir el fantasma.");

            if (collision.gameObject.name.Contains("Up"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.W));

            else if (collision.gameObject.name.Contains("Left"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.A));

            else if (collision.gameObject.name.Contains("Down"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.S));
        }

        else if (collision.gameObject.name.Contains("Paper"))
        {
            player.seEnfocoFantasmaPapel = true;

            Debug.Log("Presiona J para destruir el fantasma.");
            StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.J));
        }

        else if (collision.gameObject.name.Contains("Scissors"))
        {
            player.seEnfocoFantasmaTijera = true;

            Debug.Log("Presiona K para destruir el fantasma.");
            StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.K));
        }
    }
}
