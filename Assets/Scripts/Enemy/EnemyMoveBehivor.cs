using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMoveBehivor : MonoBehaviour
{
    Rigidbody2D rb_body;
    [SerializeField] float speed;
    [SerializeField ] GameObject player;
    [SerializeField] float radius_range;
    Animator animator_enemy;
    float scale_prefabs;
    [Header("Cooldown Time Attack")]
    [SerializeField] float cooldown_time;
    [SerializeField] float set_time;
    [Header("Raycast Check Enemy")]
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] LayerMask layerMask_player;
    [SerializeField] float range_boxCast;
    [SerializeField] float wight_RC;
    
    // Start is called before the first frame update
    void Start()
    {
        rb_body = GetComponent<Rigidbody2D>();
        animator_enemy = GetComponent<Animator>();
        scale_prefabs = transform.localScale.x;
        set_time = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 dir = (player.transform.position - rb_body.transform.position).normalized;
        Move(dir);
        checkplayerIsRangeToHit();
    }
    void Move(Vector2 dir){
        //float distance = Vector2.Distance(player.transform.position, rb_body.transform.position);
        if(RayCastIsPlayerInArea()){
            if(dir.x < -0.05f){
                transform.localScale = new Vector3(-scale_prefabs, scale_prefabs, scale_prefabs);
            }
            else{
                transform.localScale = new Vector3(scale_prefabs, scale_prefabs, scale_prefabs);
            }
            rb_body.velocity = dir.normalized * speed;
        }
        else{
            rb_body.velocity = new Vector2(0,0);
        }
    }
    void checkplayerIsRangeToHit(){
        if(RayCastIsHitPlayer() && (Time.time - set_time)  >= cooldown_time){
            animator_enemy.SetTrigger("Attack");
            rb_body.velocity = new Vector2(0,0);
            rb_body.position = transform.position;
            set_time = Time.time;
        }
    }
    bool RayCastIsPlayerInArea(){
        RaycastHit2D hit = Physics2D.CircleCast(
            boxCollider2D.bounds.center,
            radius_range,
            transform.right,
            0.05f,
            layerMask_player);
        return hit.collider != null;
    }
    bool RayCastIsHitPlayer(){
        RaycastHit2D hit = Physics2D.BoxCast(
        boxCollider2D.bounds.center + transform.right * range_boxCast * transform.localScale.x,
        new Vector2(boxCollider2D.size.x * wight_RC, boxCollider2D.size.y)/(1/Mathf.Abs(scale_prefabs))
        ,0
        ,transform.right
        ,0
        , layerMask_player);
        return hit.collider != null;
    } 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
        boxCollider2D.bounds.center + transform.right * range_boxCast * transform.localScale.x,
        new Vector2(boxCollider2D.size.x * wight_RC, boxCollider2D.size.y)/(1/Mathf.Abs(scale_prefabs))
        );
        Gizmos.DrawWireSphere(boxCollider2D.bounds.center, radius_range);
    }
    void TakeDamgePlayer(float dame){
        if(RayCastIsHitPlayer()){
            player.GetComponent<ManagerToolBar>().UpdateHealth(dame);
            player.GetComponent<PlayerController>().onHit = true;
            StartCoroutine(time_stunned_cooldown());
        }
    }
    IEnumerator time_stunned_cooldown(){
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().onHit = false;
    }
}
