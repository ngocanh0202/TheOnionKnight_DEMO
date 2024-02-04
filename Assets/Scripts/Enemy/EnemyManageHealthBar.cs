using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManageHealthBar : MonoBehaviour
{
    [SerializeField] Slider slider_health;
    [SerializeField] float max_health;
    [SerializeField] float min_health;
    private float health;
    void Start(){
        slider_health.maxValue = max_health;
        slider_health.minValue = min_health;
        slider_health.value = max_health;
        health = max_health;
    }
    
    public void UpdateHealth(float value){
        health += value;
        if(health <= 0){
            health = min_health;
            slider_health.value = health;
            Destroy(gameObject);
        }
        else if(health > max_health){
            health = max_health;
            slider_health.value = health;
        }else{
            slider_health.value = health;
        }
    }
}
