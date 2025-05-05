using UnityEngine;
using UnityEngine.Tilemaps;

public class Bullet : MonoBehaviour
{
    private SpriteRenderer _renderer;

    public Tilemap _destructibleMap;

    public LayerMask m_HitScanLayerMask;
    
    private float _rayCastLength = 0.1f;
    private float _width;
    private float _projectileSpeed = 7;
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
        transform.Translate(Vector2.right * (_projectileSpeed * Time.deltaTime));
        HitScan();
    }

    private void HitScan()
    {
        Vector2 center = _renderer.bounds.center;
        Vector2 origin = new Vector2(center.x + _width, center.y);

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right, _rayCastLength, m_HitScanLayerMask);


                
        if (hit)
        {
            if (hit.collider.CompareTag("Destructible"))
            {
                Vector3Int gridPosition = _destructibleMap.WorldToCell(hit.point);
                _destructibleMap.SetTile(gridPosition, null);
            }

            if (hit.collider.CompareTag("DestructiblePlatform"))
            {
                Destroy(hit.transform.gameObject);
            }

            if (hit.collider.CompareTag("Present"))
            {
                GameManager.Instance.GoldIncrement(7);
                Destroy(hit.transform.gameObject);
            }
            Destroy(gameObject);
        }

    }
}
