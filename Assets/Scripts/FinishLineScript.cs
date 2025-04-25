using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    private BoxCollider2D _col;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {            
            Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + col.gameObject.tag);
            GameManager.Instance.ResetLevel();
        }
    }
    
    
}
