using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    private BoxCollider2D _col;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {            
            GameManager.Instance.NextLevel();
        }
    }
    
    
}
