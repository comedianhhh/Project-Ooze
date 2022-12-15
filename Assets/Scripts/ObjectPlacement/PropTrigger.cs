using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropTrigger : MonoBehaviour
{

    public Collider2D col;
    public Animator ani;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
        ani = GetComponentInChildren<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            Debug.Log("enter");
            ani.SetTrigger("shake");
        }
    }
}
