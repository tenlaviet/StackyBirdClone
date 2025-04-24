using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    private MovingPlatformBehaviour platformScript;
    
    //[SerializeField] private Sprite m_BlockSprite;
    [SerializeField] private Transform m_PointA;
    [SerializeField] private Transform m_PointB;

    [SerializeField] private float m_speed;
    private Vector3 _nextPosition;
    
    
    
    private void Awake()
    {
        platformScript = GetComponent<MovingPlatformBehaviour>();
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = m_BlockSprite;
    }

    private void Start()
    {
        _nextPosition = m_PointB.position;
    }

    private void Update()
    {
        Move(false);
    }

    private void Move(bool patrol)
    {
        transform.position = Vector3.MoveTowards(transform.position, _nextPosition, m_speed * Time.deltaTime);
        if (transform.position == _nextPosition)
        {
            // if (!patrol)
            // {
            //     platformScript.enabled = false;
            // }
            _nextPosition = (_nextPosition == m_PointA.position) ? m_PointB.position : m_PointA.position;
        }
    }
}
