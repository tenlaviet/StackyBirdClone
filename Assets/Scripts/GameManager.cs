using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerScript Player { get; set; }
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
