using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public World World;
    public PlayerScript Player;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }


    private void LoadLevel()
    {
        Debug.Log("loadlevel");

    }

    private void RestartLevel()
    {
        Debug.Log("restartlevel");

    }

    private void GameOver()
    {
        Debug.Log("gameover");
    }
    
}
