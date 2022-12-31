using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    SceneFader fader;
    public CamController cam;
    List<Enemy> enemies;

    Door lockedDoor;

    public int deathNum;
    public int enemyNum;

    public float GameTime;

    public bool isEnd=false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        enemies = new List<Enemy>();

        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        //enemyNum = instance.enemies.Count;
        if(!isEnd)
        {
            GameTime += Time.deltaTime;
        }
        if (isEnd)
        {
            End_UIManager.UpdateTimeUI((int)GameTime);
            End_UIManager.UpdateDeathUI(enemyNum);

        }

    }

    public static void RegisterDoor(Door door)
    {
        instance.lockedDoor=door;
    }
    public static void RegisterEnemy(Enemy enemy)
    {
        if (!instance.enemies.Contains(enemy))
        {
            instance.enemies.Add(enemy);
        }
    }
    public static void RegisterSceneFader(SceneFader obj)
    {
        instance.fader = obj;
    }

    public static  void RegisterCam(CamController cam)
    {
        instance.cam = cam;
    }

    public static void EnemyDied(Enemy enemy)
    {
        if (!instance.enemies.Contains(enemy))
            return;
        instance.enemies.Remove(enemy);

        if (instance.enemies.Count == 0)
            instance.lockedDoor.Open();
    }
    public static void PlayerHurt()
    {
        instance.cam.Flash();
    }

    public static void PlayerDied()
    {
        instance.fader.FadeOut();
        instance.deathNum++;
        End_UIManager.UpdateDeathUI(instance.deathNum);
        instance.Invoke("RestartScene",1.5f);
        instance.isEnd = true;

    }

    void RestartScene()
    {
        instance.enemies.Clear();
        SceneManager.LoadScene("end");
    }
    public void EnterScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public static void EndGame()
    {
        instance.isEnd = true;
    }

}
