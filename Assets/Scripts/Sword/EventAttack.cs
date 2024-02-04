using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAttack : MonoBehaviour
{
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("Enemy")){
    //         Debug.Log("Health dame");
    //         other.gameObject.GetComponent<EnemyManageHealthBar>().UpdateHealth(-20);
    //     }
    // }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Enemy")){
            Debug.Log("Health dame");
            other.gameObject.GetComponent<EnemyManageHealthBar>().UpdateHealth(-20);
        }
    }
}
