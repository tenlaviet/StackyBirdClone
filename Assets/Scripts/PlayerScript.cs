using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private GameObject m_Egg;
    [SerializeField] private Bullet m_Laser;
    
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
        
        Vector2 mid = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y);
        Vector2 btm = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y - _height + 0.05f);
        Vector2 top = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y + _height - 0.05f);
        
        RaycastHit2D midHit = Physics2D.Raycast(mid, Vector2.right, _rayCastLength, m_SurfaceLayerMask);
        RaycastHit2D btmHit = Physics2D.Raycast(btm, Vector2.right, _rayCastLength, m_SurfaceLayerMask);
        RaycastHit2D topHit = Physics2D.Raycast(top, Vector2.right, _rayCastLength, m_SurfaceLayerMask);
        
        Color color1 = midHit ? Color.green : Color.red;
        Color color2 = btmHit ? Color.green : Color.red;
        Color color3 = topHit ? Color.green : Color.red;
        
        
        Debug.DrawRay(mid, Vector2.right * _rayCastLength, color1);
        Debug.DrawRay(btm, Vector2.right * _rayCastLength, color2);
        Debug.DrawRay(top, Vector2.right * _rayCastLength, color3);

        if (midHit || btmHit || topHit)
        {
            // Debug.Log("btm:"+btmHit);
            // Debug.Log("mid:"+midHit);
            // Debug.Log("top:"+topHit);
            return true;
        }
        return false;


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
        GameManager.Instance.ResetLevel();
        //Destroy(gameObject);
    }
    
    
}
