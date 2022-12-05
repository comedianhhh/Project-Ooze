using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : MonoBehaviour
{

    private float time;

    [SerializeField] private float stillTimer;
    public bool isInvincible;
    private bool isStill;
    private CharacterMover mover;


    void Awake()
    {
        mover = GetComponent<CharacterMover>();
    }
    void Update()
    {
        isStill = mover.GetStillState();


        if (isStill&&!isInvincible)
        {
            stillTimer += Time.deltaTime;
            if (stillTimer > 2)
            {
                isInvincible = true;
                stillTimer = 0f;
            }
        }
        else if (!isStill)
        {
            isInvincible = false;
        }

    }
}
