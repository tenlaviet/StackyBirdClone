using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int stage { get; private set; } = 1;
    
    public World World;
    public PlayerScript Player;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        throw new NotImplementedException();
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }
    public void LoadLevel(int stage)
    {
        this.stage = stage;

        SceneManager.LoadScene($"World_1-{stage}");
    }

    public void NextLevel()
    {
        LoadLevel(this.stage + 1);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //LoadLevel(this.stage);
    }

}
