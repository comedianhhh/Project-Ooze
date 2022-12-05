using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField] GameObject poison;



    private float Poisontimer;

    public bool CanPoison;
    public bool CanDeflect;
    public bool CanHeal;
    public bool isInvincible;


    [SerializeField] private float stillTimer;
    private bool isStill;
    private CharacterMover mover;


    void Awake()
    {
        mover = GetComponent<CharacterMover>();
    }
    void Update()
    {
        ChangeInvincible();
        if(CanPoison) ReleasePoison();

    }

    void ChangeInvincible()
    {
        isStill = mover.GetStillState();
        if (isStill && !isInvincible)
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
     void ReleasePoison()
    {
        Poisontimer += Time.deltaTime;
        if (Poisontimer > 4)
        {
            Instantiate(poison, transform.position, Quaternion.identity);
            Poisontimer = 0;
        }
    }

}
