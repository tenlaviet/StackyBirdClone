using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMovement : MonoBehaviour
{
    [SerializeField] private float m_Speed;
    private Vector2 _direction = new Vector2(-1,0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Traverse(_direction, m_Speed);
    }

    private void Traverse(Vector2 direction, float speed)
    {
        transform.Translate(direction * (speed * Time.deltaTime));
    }
}
