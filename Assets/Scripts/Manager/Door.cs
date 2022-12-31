using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    Animator anim;
    public string sceneName;

    public string entrancePassword;

    [SerializeField] private string newScenePassword;

    [SerializeField] bool isEntrance;
    [SerializeField] bool isLastDoor=false;
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
        if (isEntrance)
        {
            Open();
        }
    }
    private void Start()
    {
        //Entrance
        if (isEntrance)
        {
            if (Player.instance.ScenePassword == entrancePassword)
            {
                Player.instance.transform.position = transform.position;
            }
            else
            {
                Debug.Log("WRONG PW");
            }
        }
        else
        {
            GameManager.RegisterDoor(this);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEntrance)
        {
            //Exit
            if (other.tag == "Player")
            {
                Player.instance.ScenePassword = newScenePassword;
                if (isLastDoor)
                    GameManager.EndGame();
                SceneManager.LoadScene(sceneName);
            }
        }

    }



    public void Open()
    {
        anim.SetTrigger("Open");
        AudioManager.Play("stoneDoor_snd");
    }


}
