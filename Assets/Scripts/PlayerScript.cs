using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject m_Egg;
    [SerializeField]private LayerMask m_SurfaceLayerMask;

    private BoxCollider2D _playerCollider;
    
    
     
    
    [SerializeField] float _rayCastLength;
    private float _width;
    private float _height;

    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
        //playerCollider.bounds.
        
        
        _width = _playerCollider.bounds.extents.x;
        _height = _playerCollider.bounds.extents.y;

        //int surfaceLayerMask = LayerMask.GetMask("Surface");
        //_layer = ~(1 << surfaceLayerMask);
    }

    private void Start()
    {
        InputManager.Instance.Player = this;

        
    }

    void Update()
    {
        
        //IsGroundHit();
        IsWallHit();

    }

    public void LayEgg()
    {
        Vector3 eggSpawnPosition = _playerCollider.bounds.center;
        transform.position += Vector3.up;
        GameObject egg = Instantiate(m_Egg, eggSpawnPosition, Quaternion.identity);
        Debug.Log("lay egg");
    }

    private bool IsWallHit()
    {
        Vector2 playerColliderCenter = _playerCollider.bounds.center;
        Vector2 origin = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y);

        RaycastHit2D wallCheck = Physics2D.Raycast(origin, Vector2.right, _rayCastLength, m_SurfaceLayerMask);
        Color color = new Color();
        color = wallCheck ? Color.green : Color.red;
        Debug.DrawRay(origin, Vector2.right * _rayCastLength, color);
        return wallCheck;
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

    private void Die()
    {
        if (IsWallHit())
        {
            //kill player
            return;
        }

        if (transform.position.y < -4)
        {
            //kill player
            return;
        }
        //if player falls off bound
        //if gets hit by a projectile
    }
}
