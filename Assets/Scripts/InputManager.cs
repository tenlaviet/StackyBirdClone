using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

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


    public void OnTap()
    {
        Player.LayEgg();
    }
}
