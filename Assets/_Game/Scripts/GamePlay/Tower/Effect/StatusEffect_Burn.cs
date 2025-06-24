using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StatusEffect_Burn : StatusEffect
{
    private int damagePerTick;
    private float timeBetweenTicks;
    private float tickTimer;

    public StatusEffect_Burn(float duration, int damagePerTick, float timeBetweenTicks, Enemy target) : base(duration, target)
    {
        this.damagePerTick = damagePerTick;
        this.timeBetweenTicks = timeBetweenTicks;
        this.tickTimer = timeBetweenTicks;
    }

    public override void ApplyEffect() { }
    public override void EndEffect() { }

    public override bool UpdateEffect()
    {
        duration -= Time.deltaTime;
        tickTimer -= Time.deltaTime;

        if (tickTimer <= 0)
        {
            target.Hit(damagePerTick);
            tickTimer = timeBetweenTicks;
        }

        return duration <= 0;
    }
}
