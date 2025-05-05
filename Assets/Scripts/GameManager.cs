using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int CurrentStage { get; private set; } = 1;
    
    public World World;
    public PlayerScript Player;

    private int _goldCount;
    private TextMeshProUGUI m_GoldCounter;
    private TextMeshProUGUI m_PerfectCounter;
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

        Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    private void Start()
    {
        _goldCount = 0;
    }
    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }
    
    public void LoadLevel(int stage)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath($"Scenes/World_1-{stage}");
        if(sceneIndex >= 0)
        {
            CurrentStage = stage;
            SceneManager.LoadScene(sceneIndex);
        } else
        {
            CurrentStage = 0;
            SceneManager.LoadScene("StartScene");
        }
        CurrentStage = stage;
        
        _goldCount = 0;

    }

    public void NextLevel()
    {
        LoadLevel(CurrentStage + 1);

    }

    public void ResetLevel()
    {
        _goldCount = 0;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

        //LoadLevel(this.Stage);
    }

    public void SetGoldCounter(TextMeshProUGUI tmp)
    {
        m_GoldCounter = tmp;
    }    
    public void SetPerfectCounter(TextMeshProUGUI tmp)
    {
        m_PerfectCounter = tmp;
    }
    public void UpdatePerfectCount()
    {
        string count = Player._perfectCount.ToString();
        m_PerfectCounter.text = $"Perfect count: {count}/3";
    }
    public void GoldIncrement(int amount)
    {
        _goldCount += amount;
        string count = _goldCount.ToString();
        m_GoldCounter.text = $"Gold coins: {count}";
    }
}
