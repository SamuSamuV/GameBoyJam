using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameManager gM;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Rock"))
        {
            player.seEnfocoFantasmaPiedra = true;

            Debug.Log("Presiona de nuevo la dirección a donde estás mirando para destruir el fantasma.");

            if (collision.gameObject.name.Contains("Up"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.UpArrow, KeyCode.W));

            else if (collision.gameObject.name.Contains("Left"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.LeftArrow, KeyCode.A));

            else if (collision.gameObject.name.Contains("Down"))
                StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.DownArrow, KeyCode.S));
        }

        else if (collision.gameObject.name.Contains("Paper"))
        {
            player.seEnfocoFantasmaPapel = true;

            Debug.Log("Presiona J para destruir el fantasma.");
            StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.J, KeyCode.J));
        }

        else if (collision.gameObject.name.Contains("Scissors"))
        {
            player.seEnfocoFantasmaTijera = true;

            Debug.Log("Presiona K para destruir el fantasma.");
            StartCoroutine(collision.GetComponent<Ghost>().IniciarTemporizador(KeyCode.K, KeyCode.K));
        }

        EreToto(collision);
    }

    public void EreToto(Collider2D tuCuloPeluo)
    {
        if(gM.contRonda == 1)
        {
            if (tuCuloPeluo.gameObject.name.Contains("Up"))
            {
                gM.manual.SetActive(true);
                gM.manual.GetComponent<Image>().sprite = gM.ayuditas[0];
            }


            else if (tuCuloPeluo.gameObject.name.Contains("Left"))
            {
                gM.manual.SetActive(true);
                gM.manual.GetComponent<Image>().sprite = gM.ayuditas[1];
            }

            else if (tuCuloPeluo.gameObject.name.Contains("Down"))
            {
                gM.manual.SetActive(true);
                gM.manual.GetComponent<Image>().sprite = gM.ayuditas[2];
            }
        }        
    }
}
