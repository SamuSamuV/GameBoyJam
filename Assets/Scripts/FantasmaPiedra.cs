using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantasmaPiedra : MonoBehaviour
{
    [SerializeField] GameObject linterna;

    void Start()
    {
        linterna = GameObject.FindGameObjectWithTag("Linterna");
    }

    void Update()
    {

    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Linterna"))
    //    {
    //        Player player = collision.gameObject.GetComponentInParent<Player>();
    //        player.seEnfocoFantasma = true;
    //    }
    //}
}