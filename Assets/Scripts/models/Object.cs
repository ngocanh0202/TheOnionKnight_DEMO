public class Object
{
    float health;
    float strength;
    float dexterity;
    public Object(){

    }
    public Object(float health, float strength, float dexterity){
        this.health = health;
        this.strength = strength;
        this.dexterity = dexterity;
    }

    public float Health { get => health; set => health = value; }
    public float Strength { get => strength; set => strength = value; }
    public float Dexterity { get => dexterity; set => dexterity = value; }
}
