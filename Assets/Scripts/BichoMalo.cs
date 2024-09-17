using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoMalo : MonoBehaviour
{
    public float velocidad = 1f;
    [SerializeField] GameManager gM;

    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    void Update()
    {
        MovimientoBicho();
    }

    public void MovimientoBicho()
    {
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gM.derrotapanel.SetActive(true);
            gM.seTerminoPartida = true;
            Time.timeScale = 0;
        }
    }
}
