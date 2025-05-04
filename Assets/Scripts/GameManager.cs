using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public int Stage { get; private set; } = 1;
    
    public World World;
    public PlayerScript Player;

    [SerializeField] private int _goldCount;
    public TextMeshProUGUI m_GoldCounter;
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
        _goldCount = 0;
        m_GoldCounter.text = "0";
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }
    public void LoadLevel(int stage)
    {
        this.Stage = stage;
        _goldCount = 0;
        SceneManager.LoadScene($"World_1-{stage}");
    }

    public void NextLevel()
    {
        _goldCount = 0;
        LoadLevel(this.Stage + 1);
    }

    public void ResetLevel()
    {
        _goldCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //LoadLevel(this.stage);
    }


    public void GoldIncrement()
    {
        _goldCount++;
        m_GoldCounter.text = _goldCount.ToString();
    }
}
