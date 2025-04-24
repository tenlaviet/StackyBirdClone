using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Update = Unity.VisualScripting.Update;

public class Bullet : MonoBehaviour
{

    private void Update()
    {
        transform.Translate(new Vector3(1,0,0) * (7 * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}
