using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Swallower : MonoBehaviour
{
    [SerializeField] float sizeModifer = 0.2f;
    [SerializeField] float sizeToTransfer = 1;
    public List<EnemyData> SwallowedEnemies = new List<EnemyData>();
    [SerializeField] Animator anim;
    [SerializeField] private Ability ab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();
        if (enemy != null && enemy.CanBeSwallowed && enemy.EnemyData.Type != EnemyType.None)
        {
            anim.SetTrigger("eat");
            enemy.GetSwallowed(transform.position);
            enemy.GetComponent<Health>().StopSelfDestroy();
            SwallowedEnemies.Add(enemy.EnemyData);
            Enlarge();
            GainAbility();

        }
    }

    public void Enlarge()
    {
        transform.DOKill();
        transform.DOScale(Vector3.one + Vector3.one * sizeModifer * SwallowedEnemies.Count, 0.5f);
    }

    public void GainAbility()
    {
        if (SwallowedEnemies.Count >= sizeToTransfer) // todo: update conditions
        {
            var abilityType = SwallowedEnemies.GroupBy(x => x) .OrderByDescending(x => x.Count()).First().Key;

            if (abilityType.Type == EnemyType.Goblin)
            {
                ab.CanDeflect = true;
            }
        }
    }
}
