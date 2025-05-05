using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI Instance { get; private set; }
    public TextMeshProUGUI m_GoldCounter;
    public TextMeshProUGUI m_PerfectCounter;

    public PlayerScript Player { get; set; }

    private void Awake()
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

    private void Start()
    {
        GameManager.Instance.SetGoldCounter(m_GoldCounter);
        GameManager.Instance.SetPerfectCounter(m_PerfectCounter);

    }

    public void OnTap()
    {
        Player.LayEgg();
    }
}
