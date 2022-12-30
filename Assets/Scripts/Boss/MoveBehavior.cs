using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;

    Transform playerPos;
    [SerializeField] float speed;
    GameObject _gameObject;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
        _gameObject = animator.GetComponentInParent<Boss>().gameObject;
        

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else
        {
            timer -= Time.deltaTime;
        }
        
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);
        _gameObject.transform.position = Vector2.MoveTowards(_gameObject.transform.position, target, speed * Time.deltaTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
