using UnityEngine;

public class StatusEffect_Poison : StatusEffect
{
    private int damage;

    public StatusEffect_Poison(float duration, int damage, Enemy target) : base(duration, target)
    {
        this.damage = damage;
    }

    public override void ApplyEffect() 
    {
        target.OnPoisoned();
    }
    public override void EndEffect() { }

    public override bool UpdateEffect()
    {
        duration -= Time.deltaTime;
        return duration <= 0;
    }
}
