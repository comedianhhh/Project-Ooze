using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Health))]
public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData = new EnemyData();

    Health health;
    public bool CanBeSwallowed => health.CurrentHealth == 0;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void Start()
    {
        GameManager.RegisterEnemy(this);
    }
    private void Update()
    {
        if (CanBeSwallowed)
            GameManager.EnemyDied(this);
    }

    public void GetSwallowed(Vector3 pos)
    {
        Debug.Log("swallow");
        Transform animator = GetComponentInChildren<Animator>().transform;
        animator.DOMove(pos, 0.5f).SetEase(Ease.InSine);
        animator.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InSine);
        GetComponent<CircleCollider2D>().enabled = false;
        health.Die();
    }
}
