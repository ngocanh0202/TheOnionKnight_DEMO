using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerToolBar : MonoBehaviour
{
    [SerializeField] Slider slider_health;
    [SerializeField] Slider slider_stamina;
    [SerializeField] float max_health;
    [SerializeField] float min_health;
    [SerializeField] float max_stamina;
    [SerializeField] float min_stamina;
    private float health;
    public float stamina;
    [SerializeField] private float stamina_recover_per_frame;
    [SerializeField] Image image_potion;
    void Start(){
        slider_health.maxValue = max_health;
        slider_health.minValue = min_health;
        slider_health.value = max_health;
        health = max_health;

        slider_stamina.maxValue =  max_stamina;
        slider_stamina.minValue = min_stamina;
        slider_stamina.value = max_stamina;
        stamina = max_stamina;

        image_potion.fillAmount = 1;
    }

    void Update(){
        if(stamina <= max_stamina){
            float recover_stamina = stamina_recover_per_frame * Time.deltaTime;
            UpdateStamina(recover_stamina);
        }
    }
    
    public void UpdateHealth(float value){
        health += value;
        if(health <= 0){
            health = min_health;
            slider_health.value = health;
            Debug.Log($"Game Over!!");
        }
        else if(health > max_health){
            health = max_health;
            slider_health.value = health;
        }else{
            slider_health.value = health;
        }
    }
    public void UpdateStamina(float value){
        stamina += value;
        if(stamina <= 0){
            stamina = min_stamina;
            slider_stamina.value = stamina;
        }
        else if(stamina >= 100){
            stamina = max_stamina;
            slider_stamina.value = stamina;
        }else{
            slider_stamina.value = stamina;
        }
    }
    public void UsePotion(float value){
        image_potion.fillAmount += value;
    }
    public void UpdatePotion(float value){
        image_potion.fillAmount += value;
    }
}
