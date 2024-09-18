using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float velocidad = 2f;
    private Rigidbody2D rb;
    public Animator animatorPlayer;
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

    public AudioSource audioSourcePlayer;
    public AudioClip derrotaSound;
    public AudioClip victoriaSound;
    public AudioClip hurtSound;
    public AudioClip ghostDieSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();

        linternaDerecha = GameObject.FindGameObjectWithTag("LinternaDerecha");
        linternaArriba = GameObject.FindGameObjectWithTag("LinternaArriba");
        linternaAbajo = GameObject.FindGameObjectWithTag("LinternaAbajo");
        linternaIzquierda = GameObject.FindGameObjectWithTag("LinternaIzquierda");
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();

        linternaDerecha.SetActive(false);
        linternaArriba.SetActive(false);
        linternaAbajo.SetActive(false);
        linternaIzquierda.SetActive(false);

        audioSourcePlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!seEnfocoFantasmaPiedra && !seEnfocoFantasmaTijera && !seEnfocoFantasmaPapel && !gM.seTerminoPartida)
        {
            Movimiento();
            GirarLinterna();
        }

        if (vidas <= 0 && !gM.seTerminoPartida)
        {
            animatorPlayer.SetBool("Muerte", true);
            audioSourcePlayer.clip = derrotaSound;
            audioSourcePlayer.Play();

            gM.seTerminoPartida = true;
        }
    }

    void AbrirDerrotaPanel()
    {
        gM.derrotapanel.SetActive(true);
        Time.timeScale = 0;
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
            animatorPlayer.SetBool("MirandoArriba", true);
            animatorPlayer.SetBool("MirandoAbajo", false);
            animatorPlayer.SetBool("MirandoIzq", false);
            animatorPlayer.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(true);
            linternaAbajo.SetActive(false);
            linternaIzquierda.SetActive(false);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            animatorPlayer.SetBool("MirandoArriba", false);
            animatorPlayer.SetBool("MirandoAbajo", true);
            animatorPlayer.SetBool("MirandoIzq", false);
            animatorPlayer.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(false);
            linternaAbajo.SetActive(true);
            linternaIzquierda.SetActive(false);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            animatorPlayer.SetBool("MirandoArriba", false);
            animatorPlayer.SetBool("MirandoAbajo", false);
            animatorPlayer.SetBool("MirandoIzq", true);
            animatorPlayer.SetBool("MirandoDer", false);

            linternaDerecha.SetActive(false);
            linternaArriba.SetActive(false);
            linternaAbajo.SetActive(false);
            linternaIzquierda.SetActive(true);
        }

        else
        {
            animatorPlayer.SetBool("MirandoArriba", false);
            animatorPlayer.SetBool("MirandoAbajo", false);
            animatorPlayer.SetBool("MirandoIzq", false);
            animatorPlayer.SetBool("MirandoDer", true);

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
