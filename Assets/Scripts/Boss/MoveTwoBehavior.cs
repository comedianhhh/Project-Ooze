using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTwoBehavior : StateMachineBehaviour
{
    public float timer;
    public float minTime;
    public float maxTime;


    Transform playerPos;
    [SerializeField] float speed;
    GameObject _gameObject;
    public GameObject _particle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        timer = Random.Range(minTime, maxTime);
        _gameObject = animator.GetComponentInParent<Boss>().gameObject;
        Instantiate(_particle,_gameObject.transform.position,Quaternion.identity);
   
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("shoot");
        }
        else
        {
            timer -= Time.deltaTime;
        }
        Vector2 target = new Vector2(playerPos.position.x, playerPos.position.y);

        Vector2 _pos = _gameObject.transform.position;
        _gameObject.transform.position = Vector2.MoveTowards(_pos, (target- _pos)*0.5f, speed * Time.deltaTime);

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


}
