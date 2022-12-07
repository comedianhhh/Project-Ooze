using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorActions : MonoBehaviour
{
    [SerializeField] UnityEvent onAttack = new UnityEvent();
    [SerializeField] UnityEvent onExit = new UnityEvent();
    [SerializeField] UnityEvent onInActive = new UnityEvent();
    [SerializeField] UnityEvent onActive = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimatorAttack()
    {
        onAttack.Invoke();
    }

    public void ExitAttack()
    {
        onExit.Invoke();
    }

    public void Inactive()
    {
        onInActive.Invoke();
    }

    public void Active()
    {
        onActive.Invoke();
    }
}
