using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour
{
    private Rigidbody2D _rb;
    private BoxCollider2D _col;
    
    private Vector3 _velocity;
    private float _gravity = 7f;

    private bool _wallHit;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() == false)
        {
           HandleGravity();
        }
        IsWallHit();
        if (_wallHit)
        {
            PushBack();
        }
    }

    private void FixedUpdate()
    {
    }
    
    
    private bool IsGrounded()
    {
        float length = 0.01f;
        Vector3 btmLeft = new Vector3(_col.bounds.center.x - _col.bounds.extents.x, _col.bounds.center.y - _col.bounds.extents.y -0.01f, 0);
        //Vector3 btmMid = new Vector3(_col.bounds.center.x, _col.bounds.center.y - _col.bounds.extents.y -0.01f, 0);
        Vector3 btmRight = new Vector3(_col.bounds.center.x + _col.bounds.extents.x, _col.bounds.center.y - _col.bounds.extents.y -0.01f, 0);
        RaycastHit2D btmLeftHit = Physics2D.Raycast(btmLeft, Vector2.down, length);
        //RaycastHit2D btmMidHit = Physics2D.Raycast(btmMid, Vector2.down, length);
        RaycastHit2D btmRightHit = Physics2D.Raycast(btmRight, Vector2.down, length);
        // Color color1 = groundHit ? Color.green : Color.red;
        // Debug.DrawRay(origin, Vector2.down * (length), color1);
        bool groundHit = false;
        if (btmLeftHit || btmRightHit)
        {
            groundHit = true;
            if (transform.position.y % 0.5f != 0)
            {
                float snapYPos = Mathf.Round(transform.position.y / 0.5f)*0.5f;
                transform.position = new Vector2(transform.position.x, snapYPos);
            }
            //transform.position.y
        }
        return groundHit;
    }
    
    private void IsWallHit()
    {
        if (_wallHit == false)
        {
            float length = 0.01f;
            Vector2 colCenter = _col.bounds.center;
            float width = _col.bounds.extents.x;
            float height = _col.bounds.extents.y;
            Vector2 btm = new Vector2(colCenter.x + width + 0.01f, colCenter.y - height + 0.1f);
            Vector2 top = new Vector2(colCenter.x + width + 0.01f, colCenter.y + height - 0.1f);
        
            RaycastHit2D btmHit = Physics2D.Raycast(btm, Vector2.right, length, LayerMask.GetMask("Surface"));
            RaycastHit2D topHit = Physics2D.Raycast(top, Vector2.right, length, LayerMask.GetMask("Surface"));
        

            // Color color1 = btmHit ? Color.green : Color.red;
            // Color color2 = topHit ? Color.green : Color.red;
            //
            //
            // Debug.DrawRay(btm, Vector2.right * length, color1);
            // Debug.DrawRay(top, Vector2.right * length, color2);

            if (btmHit || topHit)
            {
                // if (transform.position.x % 0.5f != 0)
                // {
                //     float snapXPos = Mathf.Round(transform.position.x / 0.5f)*0.5f;
                //     transform.position = new Vector2(snapXPos, transform.position.x);
                // }
                _wallHit = true;
            }
        }

    }
    private void HandleGravity()
    {

        transform.Translate(Vector2.down * (_gravity * Time.deltaTime));
        //transform.position = new Vector3(transform.position.x, transform.position.y - (gravity* Time.deltaTime));
    }
    private void PushBack()
    {
        transform.Translate(Vector2.left * (2 * Time.deltaTime));
    }
}
