using UnityEngine;

public class MovingPlatformBehaviour : MonoBehaviour
{
    private MovingPlatformBehaviour platformScript;
    
    //[SerializeField] private Sprite m_BlockSprite;
    [SerializeField] private Transform m_PointA;
    [SerializeField] private Transform m_PointB;

    [SerializeField] private float m_speed;
    private Vector3 _destination;
    
    
    
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
        Move();
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _destination, m_speed * Time.deltaTime);
        if (transform.localPosition == _destination)
        {
            this.enabled = false;
        }
    }
}
