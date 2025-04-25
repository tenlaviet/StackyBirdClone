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
        _nextPosition = m_PointB.localPosition;
    }

    private void Update()
    {
        Move(false);
    }

    private void Move(bool patrol)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _nextPosition, m_speed * Time.deltaTime);
        if (transform.localPosition == _nextPosition)
        {
            // if (!patrol)
            // {
            //     platformScript.enabled = false;
            // }
            _nextPosition = (_nextPosition == m_PointA.localPosition) ? m_PointB.localPosition : m_PointA.localPosition;
        }
    }
}
