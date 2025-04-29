using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] private GameObject m_Egg;
    [SerializeField] private Bullet m_Laser;
    
    [SerializeField] private LayerMask m_WallLayerMask;
    [SerializeField] private LayerMask m_SurfaceLayerMask;
    
    private BoxCollider2D _col;
    
    private float _width;
    private float _height;
    private float _wallCheckRayCastLength = 0.05f;
    private float _floorCheckRayCastLength = 0.3f;
    private float _perfectCheckRayCastLength = 0.1f;

    
    private bool _perfectCheck = true;
    private int _perfectCount;
    readonly float _shootCycle = 0.1f;
    private float _shootyModeDuration = 7f;
    private float _shootCycleTime;
    private float _shootyModeDurationTime;
    private float _bulletOffset = 0.05f;
    
    
    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _width = _col.bounds.extents.x;
        _height = _col.bounds.extents.y;

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
        if (IsGrounded() == false)
        {
            HandleGravity(); 
        }

        if (IsWallHit())
        {
            Die();
        }

        if (IsPerfectLand())
        {
            _perfectCount++;
        }
        if (_perfectCount >=3)
        {
            ShootyMode();
        }

        IsTopEmpty();
    }
    
    
    private bool IsGrounded()
    {
        float length = 0.01f;
        
        Vector3 btmLeft = new Vector3(_col.bounds.center.x - _col.bounds.extents.x, _col.bounds.center.y - _col.bounds.extents.y -0.01f, 0);
        Vector3 btmRight = new Vector3(_col.bounds.center.x + _col.bounds.extents.x, _col.bounds.center.y - _col.bounds.extents.y -0.01f, 0);
        
        RaycastHit2D btmLeftHit = Physics2D.Raycast(btmLeft, Vector2.down, length);
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
        }//may or maynot needed
        return groundHit;
    }
    private bool IsWallHit()
    {
        Vector2 playerColliderCenter = _col.bounds.center;
        
        Vector2 mid = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y);
        Vector2 btm = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y - _height + 0.1f);
        Vector2 top = new Vector2(playerColliderCenter.x + _width, playerColliderCenter.y + _height - 0.1f);
        
        RaycastHit2D midHit = Physics2D.Raycast(mid, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        RaycastHit2D btmHit = Physics2D.Raycast(btm, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        RaycastHit2D topHit = Physics2D.Raycast(top, Vector2.right, _wallCheckRayCastLength, m_WallLayerMask);
        //

        // Color color1 = midHit ? Color.green : Color.red;
        // Color color2 = btmHit ? Color.green : Color.red;
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
            //Die();
            return true;
        }
        return false;


    }

    private bool IsTopEmpty()
    {
        float length = 0.99f;
        Vector3 topLeft = new Vector3(_col.bounds.center.x - _col.bounds.extents.x, _col.bounds.center.y + _col.bounds.extents.y, 0);
        Vector3 topRight = new Vector3(_col.bounds.center.x + _col.bounds.extents.x, _col.bounds.center.y + _col.bounds.extents.y, 0);
        RaycastHit2D topLeftHit = Physics2D.Raycast(topLeft, Vector2.up, length, ~(LayerMask.GetMask("Player")));
        RaycastHit2D topRighttHit = Physics2D.Raycast(topRight, Vector2.up, length, ~(LayerMask.GetMask("Player")));
        // Color color1 = topLeftHit ? Color.green : Color.red;
        // Color color2 = topRighttHit ? Color.green : Color.red;
        // Debug.DrawRay(topLeft, Vector2.up * (length), color1);
        // Debug.DrawRay(topRight, Vector2.up * (length), color2);
        bool topHit = (!topLeftHit && !topRighttHit);
        return topHit;
    }
    private bool IsPerfectLand()
    {
        Vector2 playerColliderCenter = _col.bounds.center;
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
    private void HandleGravity()
    {

        transform.Translate(Vector2.down * (Data.Gravity * Time.deltaTime));
        //transform.position = new Vector3(transform.position.x, transform.position.y - (gravity* Time.deltaTime));
    }
    private void ShootyMode()
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

    public void LayEgg()
    {
        if (!IsTopEmpty())
        {
            Debug.Log("no space to lay egg");
            return;
        }
        Vector3 eggSpawnPosition = _col.bounds.center;
        transform.position += Vector3.up;
        GameObject egg = Instantiate(m_Egg, eggSpawnPosition, Quaternion.identity);
        Debug.Log("lay egg");
    }

    private void Shoot()
    {
        
        Vector3 bulletSpawnPosition = new Vector2(_col.bounds.center.x + _width, _col.bounds.center.y);
    
        Instantiate(m_Laser, bulletSpawnPosition, Quaternion.identity);
    }

    private void Die()
    {
        GameManager.Instance.ResetLevel();
        //Destroy(gameObject);
    }
    
    
}
