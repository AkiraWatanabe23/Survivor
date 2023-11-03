/// <summary> Systemを記述する基底クラス </summary>
public abstract class SystemBase
{
    public abstract void Initialize(GameEvent gameEvent, GameState gameState);

    public virtual void OnUpdate() { }

    public abstract void OnDestroy(GameEvent gameEvent);
}
