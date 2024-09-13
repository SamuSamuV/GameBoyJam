using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 2f;
    public bool seEnfocoFantasmaPiedra = false;
    public bool seEnfocoFantasmaTijera = false;
    public bool seEnfocoFantasmaPapel = false;

    [SerializeField] GameObject pivotLinterna;
    [SerializeField] GameObject linterna;
    [SerializeField] GameObject fantasmaPiedra;
    [SerializeField] GameObject fantasmaTijera;
    [SerializeField] GameObject fantasmaPapel;

    void Start()
    {
        pivotLinterna = GameObject.FindGameObjectWithTag("PivotLinterna");
        linterna = GameObject.FindGameObjectWithTag("Linterna");
    }

    void Update()
    {
        if (!seEnfocoFantasmaPiedra && !seEnfocoFantasmaTijera && !seEnfocoFantasmaPapel)
        {
            Movimiento();
            GirarLinterna();
        }

        if (seEnfocoFantasmaTijera && Input.GetKey(KeyCode.Q))
        {
            DestruirFantasma(fantasmaTijera);
        }

        if (seEnfocoFantasmaPiedra && Input.GetKey(KeyCode.E))
        {
            DestruirFantasma(fantasmaPiedra);
        }

        // Destruir Fantasma Papel (completar lógica cuando esté lista)
        // if (seEnfocoFantasmaPapel && Input.GetKey(KeyCode.X)) 
        // {
        //     DestruirFantasma(fantasmaPapel);
        // }
    }

    public void Movimiento()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
    }

    public void GirarLinterna()
    {
        if (Input.GetKey(KeyCode.W))
        {
            pivotLinterna.transform.eulerAngles = new Vector3(0f, 0f, 180f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            pivotLinterna.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            pivotLinterna.transform.eulerAngles = new Vector3(0f, 0f, 270f);
        }
        else
        {
            pivotLinterna.transform.eulerAngles = new Vector3(0f, 0f, 90f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FantasmaPiedra"))
        {
            seEnfocoFantasmaPiedra = true;
            fantasmaPiedra = collision.gameObject;
            Debug.Log("Presiona E para destruir el fantasma Piedra.");
        }
        else if (collision.gameObject.CompareTag("FantasmaTijera"))
        {
            seEnfocoFantasmaTijera = true;
            fantasmaTijera = collision.gameObject;
            Debug.Log("Presiona Q para destruir el fantasma Tijera.");
        }
        else if (collision.gameObject.CompareTag("FantasmaPapel"))
        {
            seEnfocoFantasmaPapel = true;
            fantasmaPapel = collision.gameObject;
            Debug.Log("Presiona la tecla correspondiente para destruir el fantasma Papel.");
        }
    }

    public void DestruirFantasma(GameObject fantasma)
    {
            Destroy(fantasma);
            ResetearEnfoque();
            Debug.Log("Fantasma destruido.");
    }

    private void ResetearEnfoque()
    {
        seEnfocoFantasmaPiedra = false;
        seEnfocoFantasmaTijera = false;
        seEnfocoFantasmaPapel = false;
    }
}
