using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAutoDestruction : MonoBehaviour
{
    private ParticleSystem[] particleSystems;

    [SerializeField]
    private float lifeTime=5f;

    void Start()
    {
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }

    void Awake()
    {
        StartCoroutine(IDestroy());
    }


    void Update()
    {
        bool allStopped = true;
        foreach (ParticleSystem ps in particleSystems)
        {
            if (!ps.isStopped)
            {
                allStopped = false;
            }
        }
        if (allStopped&&gameObject!=null)
            GameObject.Destroy(gameObject);
    }

    IEnumerator IDestroy()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
