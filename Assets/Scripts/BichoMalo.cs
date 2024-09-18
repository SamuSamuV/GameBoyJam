using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BichoMalo : MonoBehaviour
{
    public float velocidad = 1f;
    [SerializeField] GameManager gM;
    [SerializeField] public GameObject player;

    void Start()
    {
        gM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
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
            player.GetComponent<Player>().vidas = 0;
        }
    }
}
