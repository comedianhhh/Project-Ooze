using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Swallower : MonoBehaviour
{
    [SerializeField] float sizeModifer = 0.1f;
    [SerializeField] float sizeToTransfer = 5;
    public List<EnemyData> SwallowedEnemies = new List<EnemyData>();
    [SerializeField] Animator anim;
    private Ability ab;

    [SerializeField] private int currentVolume=0;
    [SerializeField] private int maxVolume = 10;
    [SerializeField] private EnemyData MagicData;
    [SerializeField] private EnemyData PoisonData;
    [SerializeField] private int MaxEnlargeTimes=5;
    [SerializeField] private int currentEnlargeTimes;


    private SpriteRenderer sprite;

    void Awake()
    {
        ab = GetComponent<Ability>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
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
            currentVolume += enemy.EnemyData.Bulk;
        }
    }

    public void Enlarge()
    {
        if (currentEnlargeTimes <= MaxEnlargeTimes)
        {
            AudioManager.Play("gurgle_loop");

            currentEnlargeTimes++;
            transform.DOKill();
            transform.DOScale(Vector3.one + Vector3.one * sizeModifer * SwallowedEnemies.Count, 0.5f).SetEase(Ease.InBounce);
        }

    }

    public void GainAbility()
    {
        if (SwallowedEnemies.Count >= sizeToTransfer||currentVolume>=maxVolume) // todo: update conditions
        {
            var abilityType = SwallowedEnemies.GroupBy(x => x) .OrderByDescending(x => x.Count()).First().Key;

            anim.SetTrigger("change");
            transform.DOScale(Vector3.one , 0.5f).SetEase(Ease.InBounce);
            currentVolume = 0;

            if (abilityType.Type == EnemyType.Goblin)
            {
                ab.CanDeflect = true;
                SwallowedEnemies.Clear();
                ab.BeGob();
            }
            else if (abilityType.Type == EnemyType.Mushroom&&SwallowedEnemies.Exists(t=> t.Type==EnemyType.MrPosion))//ate poison mushroom
            {
                ab.CanPoison = true;
                SwallowedEnemies.Clear();
                ab.BeMr();
            }
            else if (abilityType.Type == EnemyType.Mushroom && SwallowedEnemies.Exists(t => t.Type == EnemyType.MrMagic))//ate magic mushroom
            {
                ab.isMagic = true;
                SwallowedEnemies.Clear();
                ab.BeMagic();
            }
            else if (abilityType.Type == EnemyType.Mushroom&&!SwallowedEnemies.Exists(t => t.Type == EnemyType.MrPosion)&& !SwallowedEnemies.Exists(t => t.Type == EnemyType.MrMagic))//only ate mushroom
            {
                ab.CanHeal = true;
                SwallowedEnemies.Clear();
                ab.BeMr();
            }
            else if (abilityType.Type == EnemyType.Cyclops)
            {
                ab.BeCyc();
                ab.CanSpeedUp = true;
                SwallowedEnemies.Clear();
            }
            else if (abilityType.Type == EnemyType.Grass)
            {
                ab.CanBeGrass = true;
                SwallowedEnemies.Clear();
            }


        }
    }
}
