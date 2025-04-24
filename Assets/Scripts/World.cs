using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{

    public Tilemap m_DestructibleMap; 
    public Tilemap m_InDestructibleMap; 

        
        
    [SerializeField] private float m_Speed;
    private void Awake()
    {
        //GameManager.Instance.World = this;
    }
    private void Start()
    {
        GameManager.Instance.World = this;
    }

    void Update()
    {
        Traverse(m_Speed);
    }

    private void Traverse(float speed)
    {
        transform.Translate(Vector2.left * (speed * Time.deltaTime));
    }
}
