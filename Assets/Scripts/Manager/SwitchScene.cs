using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
    [SerializeField] string newSceneName;

    public void SwitchNewscene()
    {
        SceneManager.LoadScene(newSceneName);
    }

}
