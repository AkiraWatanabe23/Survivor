using System;
using UnityEngine;

/// <summary> ゲーム内で実行される各イベント </summary>
public class GameEvent
{
    public Func<GameObject, GameObject> OnSpawn;
    public Action<GameObject> OnRemove;

    public Action<GameObject> OnActivate;
    public Action<GameObject> OnDead;

    public Action<GameObject> OnLvUp;

    public Action<AttackData, int> OnPowerUp;
    public Action<AttackData, int> OnPowerDown;
    public Action<AttackData, float> OnIntervalChange;
    public Action OnStatusReset;

    public Action<HealthData, int> OnHeal;

    public Action OnPause;
    public Action OnResume;
}
