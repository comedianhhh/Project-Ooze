using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    SceneFader fader;
    List<Enemy> enemies;

    Door lockedDoor;

    public int deathNum;
    public int enemyNum;
    
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
        enemyNum = instance.enemies.Count;
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

    public static void EnemyDied(Enemy enemy)
    {
        if (!instance.enemies.Contains(enemy))
            return;
        instance.enemies.Remove(enemy);

        if (instance.enemies.Count == 0)
            instance.lockedDoor.Open();
    }


    public static void PlayerDied()
    {
        instance.fader.FadeOut();
        instance.deathNum++;
        instance.Invoke("RestartScene",1.5f);

    }

    void RestartScene()
    {
        instance.enemies.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EnterScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
