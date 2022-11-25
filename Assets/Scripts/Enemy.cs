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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetSwallowed(Vector3 pos)
    {
        Transform animator = GetComponentInChildren<Animator>().transform;
        animator.DOMove(pos, 0.2f).SetEase(Ease.InSine);
        animator.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InSine);
    }
}
