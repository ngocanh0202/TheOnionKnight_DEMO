using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwordController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool isAttacking;
    [SerializeField] float radius;
    [SerializeField] Animator animator;
    [SerializeField] float _cooldownAttacking;
    [SerializeField] public GameObject areaAttack;

    // Start is called before the first frame update
    void Awake()
    {
        isAttacking = false;
        areaAttack = transform.GetChild(0).gameObject;
    }
    public void Attacking(){
        if(!isAttacking){
            isAttacking = true;
            areaAttack.SetActive(true);
            animator.SetBool("isAttacking", true);
            StartCoroutine(cooldownAttacking());
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if(isAttacking) return;
        // Set sword move and rotation
       

        //rb_sword.MovePosition(player.transform.position);
        Vector2 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 dir = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        transform.up = dir;
        
        
        float distance = Vector2.Distance(player.transform.position,transform.position);
        if(distance > radius){
            Vector2 directionToCenter = (player.transform.position - transform.position).normalized;
            Vector2 center = new Vector2(player.transform.position.x, player.transform.position.y);
            transform.position = center - directionToCenter * radius;
        }
        
    }
    IEnumerator cooldownAttacking(){
        yield return new WaitForSeconds(_cooldownAttacking);
        areaAttack.SetActive(false);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
        //spriteRenderer.enabled = false;
    }
    
}
