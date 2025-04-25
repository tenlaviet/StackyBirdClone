using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private GameObject m_Egg;
    [SerializeField] private Bullet m_Laser;
    
    [SerializeField] private LayerMask m_WallLayerMask;
    [SerializeField] private LayerMask m_SurfaceLayerMask;
    
    private BoxCollider2D _playerCollider;
    
     
    
    private float _width;
    private float _height;
    private float _wallCheckRayCastLength = 0.1f;
    private float _floorCheckRayCastLength = 0.3f;
    private float _perfectCheckRayCastLength = 0.1f;

    
    private bool _perfectCheck = true;
    private int _perfectCount;
    readonly float _shootCycle = 0.1f;
    private float _shootyModeDuration = 7f;
    [SerializeField]private float _shootCycleTime;
    [SerializeField]private float _shootyModeDurationTime;
    private float _bulletOffset = 0.05f;
    
    
    private void Awake()
    {
        _playerCollider = GetComponent<BoxCollider2D>();
        _width = _playerCollider.bounds.extents.x;
        _height = _playerCollider.bounds.extents.y;

        _shootCycleTime = 0;
        _shootyModeDurationTime = _shootyModeDuration;
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
            LayEgg();
        }

        if (IsPerfectLand())
        {
            
            Debug.Log("Perfect");
            _perfectCount++;
        }
        ShootyMode();
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
        
        RaycastHit2D midHit = Physics2D.Raycast(mid, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        RaycastHit2D btmHit = Physics2D.Raycast(btm, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        RaycastHit2D topHit = Physics2D.Raycast(top, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        //

        // Color color3 = topHit ? Color.green : Color.red;
        //
        //
        // Debug.DrawRay(mid, Vector2.right * _wallCheckRayCastLength, color1);
        // Debug.DrawRay(btm, Vector2.right * _wallCheckRayCastLength, color2);
        // Debug.DrawRay(top, Vector2.right * _wallCheckRayCastLength, color3);

        if (midHit || btmHit || topHit)
        {
            // Debug.Log("btm:"+btmHit);
            // Debug.Log("mid:"+midHit);
            // Debug.Log("top:"+topHit);
            Die();
            return true;
        }
        return false;


    }
    private bool IsPerfectLand()
    {
        Vector2 playerColliderCenter = _playerCollider.bounds.center;
        Vector2 btmRight = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y - _height);
        Vector2 btmMiddle = new Vector2(playerColliderCenter.x, playerColliderCenter.y - _height);

        RaycastHit2D outerHit = Physics2D.Raycast(btmRight, Vector2.down, _perfectCheckRayCastLength, m_SurfaceLayerMask);
        RaycastHit2D innerHit = Physics2D.Raycast(btmMiddle, Vector2.down, _floorCheckRayCastLength, m_SurfaceLayerMask);
        
        Color color1 = outerHit ? Color.green : Color.red;
        Color color2 = innerHit ? Color.green : Color.red;

            
        Debug.DrawRay(btmRight, Vector2.down * _perfectCheckRayCastLength, color1);
        Debug.DrawRay(btmMiddle, Vector2.down * _floorCheckRayCastLength, color2);
        if (outerHit && !innerHit)
        {
            if (_perfectCheck)
            {
                _perfectCheck = false;
                return true;
            }
        }

        if (!outerHit && !innerHit)
        {
            _perfectCheck = true;
        }
        return false;
    }
    
    private void ShootyMode()
    {
        if (_perfectCount >=3)
        {
            if (_shootyModeDurationTime > 0)
            {
                if (_shootCycleTime <=0)
                {
                    Shoot();
                    _shootCycleTime = _shootCycle;
                }

                _shootCycleTime -= Time.deltaTime;
                _shootyModeDurationTime -= Time.deltaTime;
            }
            if (_shootyModeDurationTime < 0)
            {
                _shootyModeDurationTime = _shootyModeDuration;
                _perfectCount = 0;
            }
        }
        
    }

    public void LayEgg()
    {
        Vector3 eggSpawnPosition = _playerCollider.bounds.center;
        transform.position += Vector3.up*1.05f;
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
