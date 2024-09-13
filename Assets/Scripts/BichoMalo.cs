using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoMalo : MonoBehaviour
{
    public float velocidad = 1f;
    [SerializeField] GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GM");
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
            gameManager.gameObject.GetComponentInParent<GameManager>().derrotapanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
