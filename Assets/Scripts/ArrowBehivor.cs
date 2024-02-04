using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehivor : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float time_exist;
    Rigidbody2D rb_arrow;
    void Awake(){
        rb_arrow = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb_arrow.velocity = transform.up * speed;
        if(time_exist < 0){
            Destroy(gameObject);
        }else{
            time_exist -= Time.deltaTime;
        }   
    }
    
    void OnCollisionEnter2D(Collision2D other) 
    { 
        if(other.gameObject.CompareTag("Enemy")){
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyManageHealthBar>().UpdateHealth(-10);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
