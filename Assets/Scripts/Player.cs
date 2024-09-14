using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 2f;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] public float vidas = 3f;

    public bool seEnfocoFantasmaPiedra = false;
    public bool seEnfocoFantasmaTijera = false;
    public bool seEnfocoFantasmaPapelSup = false;
    public bool seEnfocoFantasmaPapelInf = false;
    public bool seEnfocoFantasmaPapelIzq = false;

    [SerializeField] GameObject linternaDerecha;
    [SerializeField] GameObject linternaArriba;
    [SerializeField] GameObject linternaAbajo;
    [SerializeField] GameObject linternaIzquierda;

    [SerializeField] GameObject fantasmaPiedraF;
    [SerializeField] GameObject fantasmaPiedraN;
    [SerializeField] GameObject fantasmaPiedraD;
    [SerializeField] GameObject fantasmaTijeraF;
    [SerializeField] GameObject fantasmaTijeraN;
    [SerializeField] GameObject fantasmaTijeraD;
    [SerializeField] GameObject fantasmaPapelSupF;
    [SerializeField] GameObject fantasmaPapelSupN;
    [SerializeField] GameObject fantasmaPapelSupD;
    [SerializeField] GameObject fantasmaPapelInfF;
    [SerializeField] GameObject fantasmaPapelInfN;
    [SerializeField] GameObject fantasmaPapelInfD;
    [SerializeField] GameObject fantasmaPapelIzqF;
    [SerializeField] GameObject fantasmaPapelIzqN;
    [SerializeField] GameObject fantasmaPapelIzqD;

    [SerializeField] GameObject spawnerF;
    [SerializeField] GameObject spawnerN;
    [SerializeField] GameObject spawnerD;

    private float tiempoUltimaPulsacionW = 0f;
    private float tiempoUltimaPulsacionS = 0f;
    private float tiempoUltimaPulsacionA = 0f;
    private float intervaloPulsacion = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        linternaDerecha = GameObject.FindGameObjectWithTag("LinternaDerecha");
        linternaArriba = GameObject.FindGameObjectWithTag("LinternaArriba");
        linternaAbajo = GameObject.FindGameObjectWithTag("LinternaAbajo");
        linternaIzquierda = GameObject.FindGameObjectWithTag("LinternaIzquierda");

        spawnerF = GameObject.FindGameObjectWithTag("SpawnerF");
        spawnerN = GameObject.FindGameObjectWithTag("SpawnerN");
        spawnerD = GameObject.FindGameObjectWithTag("SpawnerD");

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
        if (collision.gameObject.CompareTag("FantasmaPiedraF"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaPiedra = true;
            fantasmaPiedraF = collision.gameObject;
            Debug.Log("Presiona K para destruir el fantasma Piedra.");

            StartCoroutine(spawnerF.gameObject.GetComponent<SpawnerF>().IniciarTemporizador(5f, fantasmaPiedraF, KeyCode.K));
        }

        else if (collision.gameObject.CompareTag("FantasmaPiedraN"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaPiedra = true;
            fantasmaPiedraN = collision.gameObject;
            Debug.Log("Presiona K para destruir el fantasma Piedra.");

            StartCoroutine(spawnerN.gameObject.GetComponent<SpawnerN>().IniciarTemporizador(3f, fantasmaPiedraN, KeyCode.K));
        }

        else if (collision.gameObject.CompareTag("FantasmaPiedraD"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaPiedra = true;
            fantasmaPiedraD = collision.gameObject;
            Debug.Log("Presiona K para destruir el fantasma Piedra.");

            StartCoroutine(spawnerD.gameObject.GetComponent<SpawnerD>().IniciarTemporizador(2f, fantasmaPiedraD, KeyCode.K));
        }

        else if (collision.gameObject.CompareTag("FantasmaTijeraF"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaTijera = true;
            fantasmaTijeraF = collision.gameObject;
            Debug.Log("Presiona J para destruir el fantasma Tijera.");

            StartCoroutine(spawnerF.gameObject.GetComponent<SpawnerF>().IniciarTemporizador(5f, fantasmaTijeraF, KeyCode.J));
        }

        else if (collision.gameObject.CompareTag("FantasmaTijeraN"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaTijera = true;
            fantasmaTijeraN = collision.gameObject;
            Debug.Log("Presiona J para destruir el fantasma Tijera.");

            StartCoroutine(spawnerN.gameObject.GetComponent<SpawnerN>().IniciarTemporizador(3f, fantasmaTijeraN, KeyCode.J));
        }

        else if (collision.gameObject.CompareTag("FantasmaTijeraD"))
        {
            if (animator.GetBool("MirandoDer"))
            {
                return;
            }

            seEnfocoFantasmaTijera = true;
            fantasmaTijeraD = collision.gameObject;
            Debug.Log("Presiona J para destruir el fantasma Tijera.");

            StartCoroutine(spawnerD.gameObject.GetComponent<SpawnerD>().IniciarTemporizador(2f, fantasmaTijeraD, KeyCode.J));
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelSupF"))
        {
            if (animator.GetBool("MirandoArriba"))
            {
                seEnfocoFantasmaPapelSup = true;
                fantasmaPapelSupF = collision.gameObject;
                Debug.Log("Presiona W dos veces para destruir el fantasma Papel superior.");

                StartCoroutine(spawnerF.gameObject.GetComponent<SpawnerF>().IniciarTemporizador(5f, fantasmaPapelSupF, KeyCode.W));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelSupN"))
        {
            if (animator.GetBool("MirandoArriba"))
            {
                seEnfocoFantasmaPapelSup = true;
                fantasmaPapelSupN = collision.gameObject;
                Debug.Log("Presiona W dos veces para destruir el fantasma Papel superior.");

                StartCoroutine(spawnerN.gameObject.GetComponent<SpawnerN>().IniciarTemporizador(3f, fantasmaPapelSupN, KeyCode.W));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelSupD"))
        {
            if (animator.GetBool("MirandoArriba"))
            {
                seEnfocoFantasmaPapelSup = true;
                fantasmaPapelSupD = collision.gameObject;
                Debug.Log("Presiona W dos veces para destruir el fantasma Papel superior.");

                StartCoroutine(spawnerD.gameObject.GetComponent<SpawnerD>().IniciarTemporizador(2f, fantasmaPapelSupD, KeyCode.W));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelInfF"))
        {
            if (animator.GetBool("MirandoAbajo"))
            {
                seEnfocoFantasmaPapelInf = true;
                fantasmaPapelInfF = collision.gameObject;
                Debug.Log("Presiona S dos veces para destruir el fantasma Papel inferior.");

                StartCoroutine(spawnerF.gameObject.GetComponent<SpawnerF>().IniciarTemporizador(5f, fantasmaPapelInfF, KeyCode.S));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelInfN"))
        {
            if (animator.GetBool("MirandoAbajo"))
            {
                seEnfocoFantasmaPapelInf = true;
                fantasmaPapelInfN = collision.gameObject;
                Debug.Log("Presiona S dos veces para destruir el fantasma Papel inferior.");

                StartCoroutine(spawnerN.gameObject.GetComponent<SpawnerN>().IniciarTemporizador(3f, fantasmaPapelInfN, KeyCode.S));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelInfD"))
        {
            if (animator.GetBool("MirandoAbajo"))
            {
                seEnfocoFantasmaPapelInf = true;
                fantasmaPapelInfD = collision.gameObject;
                Debug.Log("Presiona S dos veces para destruir el fantasma Papel inferior.");

                StartCoroutine(spawnerD.gameObject.GetComponent<SpawnerD>().IniciarTemporizador(2f, fantasmaPapelInfD, KeyCode.S));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelIzqF"))
        {
            if (animator.GetBool("MirandoIzq"))
            {
                seEnfocoFantasmaPapelIzq = true;
                fantasmaPapelIzqF = collision.gameObject;
                Debug.Log("Presiona A dos veces para destruir el fantasma Papel izquierdo.");

                StartCoroutine(spawnerF.gameObject.GetComponent<SpawnerF>().IniciarTemporizador(5f, fantasmaPapelSupD, KeyCode.A));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelIzqN"))
        {
            if (animator.GetBool("MirandoIzq"))
            {
                seEnfocoFantasmaPapelIzq = true;
                fantasmaPapelIzqN = collision.gameObject;
                Debug.Log("Presiona A dos veces para destruir el fantasma Papel izquierdo.");

                StartCoroutine(spawnerN.gameObject.GetComponent<SpawnerN>().IniciarTemporizador(3f, fantasmaPapelIzqN, KeyCode.A));
            }
        }

        else if (collision.gameObject.CompareTag("FantasmaPapelIzqD"))
        {
            if (animator.GetBool("MirandoIzq"))
            {
                seEnfocoFantasmaPapelIzq = true;
                fantasmaPapelIzqD = collision.gameObject;
                Debug.Log("Presiona A dos veces para destruir el fantasma Papel izquierdo.");

                StartCoroutine(spawnerD.gameObject.GetComponent<SpawnerD>().IniciarTemporizador(2f, fantasmaPapelIzqD, KeyCode.A));
            }
        }
    }

    public void DestruirFantasma(GameObject fantasma)
    {
        Destroy(fantasma);
        ResetearEnfoque();
        Debug.Log("Fantasma destruido.");
    }

    public void ResetearEnfoque()
    {
        seEnfocoFantasmaPiedra = false;
        seEnfocoFantasmaTijera = false;
        seEnfocoFantasmaPapelSup = false;
        seEnfocoFantasmaPapelInf = false;
        seEnfocoFantasmaPapelIzq = false;
    }
}
