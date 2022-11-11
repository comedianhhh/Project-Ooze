using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclops : MonoBehaviour
{
    enum State { Idle, Move, Charge, PlayerDetected }

    [Header("Settings")]
    [SerializeField] float speed = 2;


    [Header("Data")]
    [SerializeField] State currentState = State.Idle;

    Health target;
    float stateTimer = 0;

    Rigidbody2D rigidbody2D;
    TargetReceiver targetReceiver;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        targetReceiver = GetComponent<TargetReceiver>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                stateTimer += Time.deltaTime;

                if (stateTimer > 3)
                    ToMove();

                if (targetReceiver.Target != null)
                    ToPlayerDetected();
                break;

            case State.Move:
                Vector2 dir = (target.transform.position - transform.position).normalized;
                rigidbody2D.velocity = dir * speed;

                if (target == null)
                    ToIdle();
                break;

            case State.Charge:
                // update
                break;

            case State.PlayerDetected:
                // update
                break;
        }
    }
    
    void ToIdle()
    {
        currentState = State.Idle;
        stateTimer = 0;
    }

    void ToMove()
    {
        currentState = State.Move;
        // move enter actions
    }

    void ToPlayerDetected()
    {
        currentState = State.PlayerDetected;
    }
}
