using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    [SerializeField] private Sprite m_BlockSprite;
    [SerializeField] private Vector2 m_MoveDirection;

    private void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();


        spriteRenderer.sprite = m_BlockSprite;
    }
}
