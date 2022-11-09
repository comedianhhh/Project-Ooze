using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    [SerializeField] 
    private float activeTime = 0.1f;
    private float timeActivated;
    private float alpha;

    [SerializeField] 
    private float alphaSet = 0.8f;
    private float alphaMultiplier = 0.85f;


    private Transform enemy;

    private SpriteRenderer SR;
    private SpriteRenderer enemySR;

    private Color color;

    private void OnEnable()
    {
        SR = GetComponent<SpriteRenderer>();
        enemy = GameObject.FindGameObjectWithTag("cyclops").transform;
        enemySR = enemy.GetComponent<SpriteRenderer>();

        alpha = alphaSet;
        SR.sprite = enemySR.sprite;
        transform.position = enemy.position;
        transform.rotation = enemy.rotation;
        timeActivated = Time.time;
    }

    private void Update()
    {
        alpha *= alphaMultiplier;
        color = new Color(1f, 1f, 1f, alpha);
        SR.color = color;

        if (Time.time>=(timeActivated+activeTime))
        {
            AfterImagePool.Instance.AddToPool(gameObject);
            
        }

    }
}
