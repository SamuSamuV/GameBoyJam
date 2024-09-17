using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 2f;
    private Rigidbody2D rb;
    public Animator animator;
    [SerializeField] public float vidas = 3f;

    public bool seEnfocoFantasmaPiedra = false;
    public bool seEnfocoFantasmaTijera = false;
    public bool seEnfocoFantasmaPapel = false;

    [SerializeField] public GameObject linternaDerecha;
    [SerializeField] public GameObject linternaArriba;
    [SerializeField] public GameObject linternaAbajo;
    [SerializeField] public GameObject linternaIzquierda;

    [SerializeField] public GameManager gM;

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
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        linternaDerecha.SetActive(false);
        linternaArriba.SetActive(false);
        linternaAbajo.SetActive(false);
        linternaIzquierda.SetActive(false);
    }

    void Update()
    {
        if (!seEnfocoFantasmaPiedra && !seEnfocoFantasmaTijera && !seEnfocoFantasmaPapel && !gM.seTerminoPartida)
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
        seEnfocoFantasmaPapel = false;
    }
}
