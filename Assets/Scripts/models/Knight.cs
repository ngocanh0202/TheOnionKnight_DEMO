public class Knight : Object
{
    float stamina;
    public Knight(){}
    public Knight(float health, float strength, float dexterity, float stamina ) : base(health, strength,dexterity){
        this.Stamina = stamina;
    }
    public float Stamina { get => stamina; set => stamina = value; }
}
