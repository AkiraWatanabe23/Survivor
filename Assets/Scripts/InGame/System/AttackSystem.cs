using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackSystem : SystemBase
{
    [SerializeField]
    private List<AttackData> _attackDatas = default;
    [SerializeField]
    private int _defaultAttackValue = 1;

    public override void Initialize(GameEvent gameEvent)
    {
        gameEvent.OnActivate += AddData;
        gameEvent.OnDead += RemoveData;
    }

    public override void OnUpdate() { }

    public override void OnDestroy(GameEvent gameEvent)
    {
        gameEvent.OnActivate -= AddData;
        gameEvent.OnDead -= RemoveData;
    }

    private void AddData(GameObject go)
    {
        if (go.TryGetComponent(out AttackData attack))
        {
            _attackDatas.Add(attack);
        }
    }

    private void RemoveData(GameObject go)
    {
        if (go.TryGetComponent(out AttackData attack))
        {
            _attackDatas.Remove(attack);
        }
    }
}
