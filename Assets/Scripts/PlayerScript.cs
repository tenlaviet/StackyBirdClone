using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private GameObject m_Egg;
    [SerializeField] private Bullet m_Laser;
    
    [SerializeField] private Tilemap m_GroundTileMap;
    [SerializeField] private LayerMask m_SurfaceLayerMask;

    
    private BoxCollider2D _playerCollider;
    
    
     
    
    private float _rayCastLength = 0.1f;
    private float _width;
    private float _height;

    private float _bulletOffset = 0.05f;
    
    
    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
        
        
        _width = _playerCollider.bounds.extents.x;
        _height = _playerCollider.bounds.extents.y;

        //int surfaceLayerMask = LayerMask.GetMask("Surface");
        //_layer = ~(1 << surfaceLayerMask);
    }

    private void Start()
    {
        InputManager.Instance.Player = this;
        GameManager.Instance.Player = this;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        
        //IsGroundHit();
        if (IsWallHit())
        {
            Die();
        }

    }
    private bool IsWallHit()
    {
        Vector2 playerColliderCenter = _playerCollider.bounds.center;
        Vector2 origin = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, _rayCastLength, m_SurfaceLayerMask);

        Color color = new Color();
        color = hit ? Color.green : Color.red;
        Debug.DrawRay(origin, Vector2.right * _rayCastLength, color);
        return hit;
    }

    private bool IsGroundHit()
    {
        
        Vector2 playerColliderCenter = _playerCollider.bounds.center;
        Vector2 origin = new Vector2(playerColliderCenter.x, playerColliderCenter.y - _height);

        RaycastHit2D groundCheck = Physics2D.Raycast(origin, Vector2.down, _rayCastLength, m_SurfaceLayerMask);
        Color color = new Color();
        color = groundCheck ? Color.green : Color.red;

            
        Debug.DrawRay(origin, Vector2.down * _rayCastLength, color);
        return groundCheck;
    }


    public void LayEgg()
    {
        Vector3 eggSpawnPosition = _playerCollider.bounds.center;
        transform.position += Vector3.up;
        GameObject egg = Instantiate(m_Egg, eggSpawnPosition, Quaternion.identity);
        Debug.Log("lay egg");
    }


    private void Shoot()
    {
        Vector2 playerColliderCenter = _playerCollider.bounds.center;
        Vector3 bulletSpawnPosition = new Vector2(playerColliderCenter.x + _width + _bulletOffset, playerColliderCenter.y);

        Instantiate(m_Laser, bulletSpawnPosition, Quaternion.identity);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    
}
