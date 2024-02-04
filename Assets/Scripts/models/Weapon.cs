public class Weapon 
{
    float damage;
    int w_flus;
    float minus_stamina;

    public float Damage { get => damage; set => damage = value; }
    public int W_flus {
         get => w_flus; 
         set {
            if(value <= 10){
                w_flus = value;
            }else{
                w_flus = 10;
            }
         }
    }
}
