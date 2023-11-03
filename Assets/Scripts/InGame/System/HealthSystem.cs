using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : SystemBase
{
    [SerializeField]
    private List<HealthData> _healthDatas = default;

    public override void Initialize(GameEvent gameEvent, GameState gameState)
    {
        _healthDatas ??= new();
        foreach (var obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (obj.TryGetComponent(out HealthData health)) { _healthDatas.Add(health); }
        }

        gameEvent.OnActivate += AddData;
        gameEvent.OnDead += RemoveData;
        gameEvent.OnHeal += Heal;
    }

    public override void OnDestroy(GameEvent gameEvent)
    {
        gameEvent.OnActivate -= AddData;
        gameEvent.OnDead -= RemoveData;
        gameEvent.OnHeal -= Heal;
    }

    private void Heal(HealthData health, int value)
    {
        if (health.HP + value > health.MaxHP) { health.HP = health.MaxHP; }
        else { health.HP += value; }
    }

    private void AddData(GameObject go)
    {
        if (go.TryGetComponent(out HealthData health))
        {
            _healthDatas.Add(health);
        }
    }

    private void RemoveData(GameObject go)
    {
        if (go.TryGetComponent(out HealthData health))
        {
            _healthDatas.Remove(health);
        }
    }
}
