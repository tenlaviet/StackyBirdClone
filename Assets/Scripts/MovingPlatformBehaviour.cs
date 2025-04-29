using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlatformBehaviour : MonoBehaviour
{
    private MovingPlatformBehaviour platformScript;
    
    //[SerializeField] private Sprite m_BlockSprite;
    [SerializeField] private Transform m_PointA;
    [SerializeField] private Transform m_PointB;

    [SerializeField] private float m_speed;
    private Vector3 _destination;

    private bool _active;
    
    private void Awake()
    {
        platformScript = GetComponent<MovingPlatformBehaviour>();
        //SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = m_BlockSprite;
    }

    private void Start()
    {
        _destination = m_PointB.localPosition;
    }

    private void Update()
    {
        //CheckTriggered();
        if (_active)
        {
            Move();
        }
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination, m_speed * Time.deltaTime);
        if (transform.localPosition == _destination)
        {
            _active = false;
            platformScript.enabled = false;
        }
    }

    // private void CheckTriggered()
    // {
    //     RaycastHit2D triggerHit = Physics2D.BoxCast(transform.position, new Vector2(1, 20), 0f, Vector2.left, 7f, LayerMask.GetMask("Player"));
    //
    //     if (triggerHit)
    //     {
    //         _active = true;
    //     }
    // }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _active = true;
        }
    }
}
