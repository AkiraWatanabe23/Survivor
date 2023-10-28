/// <summary> Systemを記述する基底クラス </summary>
public abstract class SystemBase
{
    public abstract void Initialize(GameEvent gameEvent);

    public virtual void OnUpdate() { }

    public abstract void OnDestroy(GameEvent gameEvent);
}
