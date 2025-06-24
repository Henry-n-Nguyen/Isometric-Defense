public abstract class StatusEffect
{
    protected float duration;
    protected Enemy target;

    public StatusEffect(float duration, Enemy target)
    {
        this.duration = duration;
        this.target = target;
    }

    public abstract bool UpdateEffect();

    public abstract void ApplyEffect();

    public abstract void EndEffect();
}
