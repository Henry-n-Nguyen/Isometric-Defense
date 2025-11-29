using UnityEngine;

public class StatusEffect_Stun : StatusEffect
{
    public StatusEffect_Stun(float duration, Enemy target) : base(duration, target)
    {
        
    }

    public override void ApplyEffect()
    {
        target.UpdateSpeed(0f);
    }

    public override void EndEffect()
    {
        target.UpdateSpeed(target.GetBaseSpeed());
    }

    public override bool UpdateEffect()
    {
        duration -= Time.deltaTime;
        return duration <= 0;
    }
}
