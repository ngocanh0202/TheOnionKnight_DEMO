using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    
    [Header("Animation")]
    [SerializeField] Animator animator_player;
    [Header("Player move")]
    Vector2 movement;
    [SerializeField] float speed;
    [Header("Components of player")]
    Rigidbody2D rigidbody2D_player;
    ManagerToolBar toolBar_player;
    [Header("Player roll")]
    [SerializeField] float coolCooDownl_rolling;
    bool isRolling;
    [SerializeField]
    float power_roll;
    TrailRenderer trailRenderer_player_roll;
    public List<GameObject> gameObject_weapon;
    SwordController swordController;
    BowController bowController;
    [Header("Weapon display")]
    [SerializeField] GameObject weapon_sword;
    [SerializeField] GameObject weapon_bow;
    [Header("CC")]
    public bool onHit;
    public float powerPushPlayer;
    [SerializeField] float stunn_cooldown;
    int type_weapon;
    void Awake(){
        rigidbody2D_player = GetComponent<Rigidbody2D>();
        toolBar_player = GetComponent<ManagerToolBar>();
        trailRenderer_player_roll = GetComponent<TrailRenderer>();
        isRolling = false;

        swordController = gameObject_weapon[0].GetComponent<SwordController>();
        bowController = gameObject_weapon[1].GetComponent<BowController>();
        type_weapon = 0;

        weapon_sword.SetActive(true);
        weapon_bow.SetActive(false);
    }
    void Update(){
        if(StopOnCondition()){
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            // Sword
            type_weapon = 0;
            gameObject_weapon[0].SetActive(true);
            gameObject_weapon[1].SetActive(false);
            weapon_sword.SetActive(true);
            weapon_bow.SetActive(false);
            return;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            // Bow
            type_weapon = 1;
            gameObject_weapon[0].SetActive(false);
            gameObject_weapon[1].SetActive(true);
            weapon_bow.SetActive(true);
            weapon_sword.SetActive(false);
            return;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            toolBar_player.UpdatePotion(-0.25f);
        }
        

        if(movement.magnitude > 0.05){
            if(toolBar_player.stamina >= 25){
                if(Input.GetKeyDown(KeyCode.Space)){
                    toolBar_player.UpdateStamina(-25);            
                    //Dash player
                    trailRenderer_player_roll.emitting = true;
                    isRolling = true;
                    animator_player.SetBool("isRoll", isRolling);
                    StartCoroutine(RollingPlayer());
                }
            }
        }

        if(Input.GetMouseButtonDown(0)){
            if(toolBar_player.stamina >= 15){
                if(type_weapon == 0){
                    toolBar_player.UpdateStamina(-25);
                    swordController.Attacking();
                    animator_player.SetFloat("speed", 0f);
                }
                else if (type_weapon == 1){
                    if(bowController.numbers_arrow > 0){
                        toolBar_player.UpdateStamina(-15);
                        bowController.Attacking();
                        animator_player.SetFloat("speed", 0f);
                    }
                }
            }
            rigidbody2D_player.velocity = new Vector2(0,0);
        }

        if(Input.GetKeyDown(KeyCode.R)){
            toolBar_player.UpdateHealth(25);
        }
        
    }
    void FixedUpdate(){
        if(StopOnCondition()){
            return;
        }
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        rigidbody2D_player.velocity = movement * speed;
        //rigidbody2D_player.MovePosition(rigidbody2D_player.position + movement * speed * Time.fixedDeltaTime);

        SetAnimationPlayer();
        CheckDirectionPlayer();
        
    }
    bool StopOnCondition(){
        if(isRolling) return true;
        if(type_weapon == 0){
            if(swordController.isAttacking) {
                return true;
            }
            
        }else if(type_weapon == 1){
            if(bowController.isShotting)
            return true;
        }
        if(onHit){
            rigidbody2D_player.velocity = new Vector2(0,1) * powerPushPlayer;
            // StartCoroutine(timeStunnCooldown());
            return true;
        }
        
        return false;
    }
    void DirectionPlayerRolling(){
        var dir = movement.normalized;
        rigidbody2D_player.velocity = dir * power_roll;
    }
    IEnumerator RollingPlayer(){
        DirectionPlayerRolling();
        yield return new WaitForSeconds(coolCooDownl_rolling);
        trailRenderer_player_roll.emitting = false;
        isRolling = false;
        animator_player.SetBool("isRoll", isRolling);
        rigidbody2D_player.velocity = new Vector2(0,0);
    }
    
    void SetAnimationPlayer(){
        animator_player.SetFloat("Horizontal", movement.x);
        animator_player.SetFloat("Vertical", movement.y);
        animator_player.SetFloat("speed", movement.magnitude);
    }
    void CheckDirectionPlayer(){
        if(movement.x < -0.01){
            transform.localScale = new Vector3(-0.075f,0.075f,0.075f);
            animator_player.SetBool("isFront", false);
            animator_player.SetBool("isBack", false);
            animator_player.SetBool("isLeft", true);
        }
        else if(movement.x > 0.01){
            transform.localScale = new Vector3(0.075f,0.075f,0.075f);
            animator_player.SetBool("isFront", false);
            animator_player.SetBool("isBack", false);
            animator_player.SetBool("isLeft", true);
        }
        else if(movement.y < -0.01){
            animator_player.SetBool("isFront", true);
            animator_player.SetBool("isBack", false);
            animator_player.SetBool("isLeft", false);

        }else if(movement.y > 0.01){
            animator_player.SetBool("isFront", false);
            animator_player.SetBool("isBack", true);
            animator_player.SetBool("isLeft", false);
        }
    }

}
