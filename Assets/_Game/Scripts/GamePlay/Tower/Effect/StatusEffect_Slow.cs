using UnityEngine;

public class StatusEffect_Slow : StatusEffect
{
    private float slowFactor; // Example: 0.5f = slowing 50%

    public StatusEffect_Slow(float duration, float slowFactor, Enemy target) : base(duration, target)
    {
        this.slowFactor = slowFactor;
    }

    public override void ApplyEffect()
    {
        target.UpdateSpeed(target.GetBaseSpeed() * slowFactor);
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
