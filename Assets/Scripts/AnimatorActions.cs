using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorActions : MonoBehaviour
{
    [SerializeField] UnityEvent onAttack = new UnityEvent();
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
}
