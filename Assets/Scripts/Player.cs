using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 2f;
    private Rigidbody2D rb;
    private Animator animator;

    public bool seEnfocoFantasmaPiedra = false;
    public bool seEnfocoFantasmaTijera = false;
    public bool seEnfocoFantasmaPapelSup = false;
    public bool seEnfocoFantasmaPapelInf = false;
    public bool seEnfocoFantasmaPapelIzq = false;

    [SerializeField] GameObject linternaDerecha;
    [SerializeField] GameObject linternaArriba;
    [SerializeField] GameObject linternaAbajo;
    [SerializeField] GameObject linternaIzquierda;

    [SerializeField] GameObject fantasmaPiedra;
    [SerializeField] GameObject fantasmaTijera;
    [SerializeField] GameObject fantasmaPapelSup;
    [SerializeField] GameObject fantasmaPapelInf;
    [SerializeField] GameObject fantasmaPapelIzq;

    private float tiempoUltimaPulsacionW = 0f;
    private float tiempoUltimaPulsacionS = 0f;
    private float tiempoUltimaPulsacionA = 0f;
    private float intervaloPulsacion = 0.5f; // Intervalo entre pulsaciones en segundos

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        linternaDerecha = GameObject.FindGameObjectWithTag("LinternaDerecha");
        linternaArriba = GameObject.FindGameObjectWithTag("LinternaArriba");
        linternaAbajo = GameObject.FindGameObjectWithTag("LinternaAbajo");
        linternaIzquierda = GameObject.FindGameObjectWithTag("LinternaIzquierda");

        linternaDerecha.SetActive(false);
        linternaArriba.SetActive(false);
        linternaAbajo.SetActive(false);
        linternaIzquierda.SetActive(false);
    }

    void Update()
    {
        if (!seEnfocoFantasmaPiedra && !seEnfocoFantasmaTijera && !seEnfocoFantasmaPapelSup && !seEnfocoFantasmaPapelInf && !seEnfocoFantasmaPapelIzq)
        {
            Movimiento();
            GirarLinterna();
        }

        MatarFantasma();
    }

    public void MatarFantasma()
    {
        if (seEnfocoFantasmaTijera && Input.GetKey(KeyCode.Q))
        {
            DestruirFantasma(fantasmaTijera);
        }

        if (seEnfocoFantasmaPiedra && Input.GetKey(KeyCode.E))
        {
            DestruirFantasma(fantasmaPiedra);
        }

        if (seEnfocoFantasmaPapelSup)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (Time.time - tiempoUltimaPulsacionW < intervaloPulsacion)
                {
                    DestruirFantasma(fantasmaPapelSup);
                }
                tiempoUltimaPulsacionW = Time.time;
            }
        }

        if (seEnfocoFantasmaPapelInf)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Time.time - tiempoUltimaPulsacionS < intervaloPulsacion)
                {
                    DestruirFantasma(fantasmaPapelInf);
                }
                tiempoUltimaPulsacionS = Time.time;
            }
        }

        if (seEnfocoFantasmaPapelIzq)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - tiempoUltimaPulsacionA < intervaloPulsacion)
                {
                    DestruirFantasma(fantasmaPapelIzq);
                }
                tiempoUltimaPulsacionA = Time.time;
            }
        }
    }

    public void Movimiento()
    {
        Vector2 movimiento = Vector2.right * velocidad * Time.deltaTime;
        rb.MovePosition(rb.position + movimiento);
    }

    public void GirarLinterna()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("MirandoArriba", true);
            animator.SetBool("MirandoAbajo", false);
            animator.SetBool("MirandoIzq", false);
            animator.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(true);
            linternaAbajo.SetActive(false);
            linternaIzquierda.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("MirandoArriba", false);
            animator.SetBool("MirandoAbajo", true);
            animator.SetBool("MirandoIzq", false);
            animator.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(false);
            linternaAbajo.SetActive(true);
            linternaIzquierda.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("MirandoArriba", false);
            animator.SetBool("MirandoAbajo", false);
            animator.SetBool("MirandoIzq", true);
            animator.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(false);
            linternaAbajo.SetActive(false);
            linternaIzquierda.SetActive(true);
        }
        else
        {
            animator.SetBool("MirandoArriba", false);
            animator.SetBool("MirandoAbajo", false);
            animator.SetBool("MirandoIzq", false);
            animator.SetBool("MirandoDer", true);

            linternaDerecha.SetActive(true);
            linternaArriba.SetActive(false);
            linternaAbajo.SetActive(false);
            linternaIzquierda.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FantasmaPiedra"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaPiedra = true;
            fantasmaPiedra = collision.gameObject;
            Debug.Log("Presiona E para destruir el fantasma Piedra.");
        }

        else if (collision.gameObject.CompareTag("FantasmaTijera"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaTijera = true;
            fantasmaTijera = collision.gameObject;
            Debug.Log("Presiona Q para destruir el fantasma Tijera.");
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelSup"))
        {
            if (animator.GetBool("MirandoArriba"))
            {
                seEnfocoFantasmaPapelSup = true;
                fantasmaPapelSup = collision.gameObject;
                Debug.Log("Presiona W dos veces para destruir el fantasma Papel superior.");
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelInf"))
        {
            if (animator.GetBool("MirandoAbajo"))
            {
                seEnfocoFantasmaPapelInf = true;
                fantasmaPapelInf = collision.gameObject;
                Debug.Log("Presiona S dos veces para destruir el fantasma Papel inferior.");
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelIzq"))
        {
            if (animator.GetBool("MirandoIzq"))
            {
                seEnfocoFantasmaPapelIzq = true;
                fantasmaPapelIzq = collision.gameObject;
                Debug.Log("Presiona A dos veces para destruir el fantasma Papel izquierdo.");
            }
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
        seEnfocoFantasmaPapelSup = false;
        seEnfocoFantasmaPapelInf = false;
        seEnfocoFantasmaPapelIzq = false;
    }
}
