using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;
using Update = Unity.VisualScripting.Update;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Tilemap _destructibleMap;
    
    private float _rayCastLength = 0.1f;
    private float _width;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        
        
        _width = _renderer.bounds.extents.x;
    }

    private void Start()
    {
        _destructibleMap = GameManager.Instance.World.m_DestructibleMap;

    }

    private void Update()
    {
        transform.Translate(new Vector3(1,0,0) * (7 * Time.deltaTime));
        Hit();
    }

    private void Hit()
    {
        Vector2 center = _renderer.bounds.center;
        Vector2 origin = new Vector2(center.x + _width, center.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, _rayCastLength);


        
        if (hit)
        {
            if (hit.collider.CompareTag("Destructible"))
            {
                Vector3Int gridPosition = _destructibleMap.WorldToCell(hit.point);
                _destructibleMap.SetTile(gridPosition, null);
            }
            Destroy(gameObject);
        }

    }
}
