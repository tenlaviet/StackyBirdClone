using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{

    public Tilemap m_DestructibleMap; 
    public Tilemap m_InDestructibleMap; 
    
    private void Awake()
    {
        //GameManager.Instance.World = this;
    }
    private void Start()
    {
        GameManager.Instance.World = this;
    }

    void Update()
    {
        Traverse(Data.WorldSpeed);
    }

    private void Traverse(float speed)
    {
        transform.Translate(Vector2.left * (Data.WorldSpeed * Time.deltaTime));
    }
}
