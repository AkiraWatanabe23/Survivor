using System;
using UnityEngine;

/// <summary> ゲーム内で実行される各イベント </summary>
public class GameEvent
{
    public Func<GameObject, GameObject> OnSpawn;

    public Action<GameObject> OnActivate;
    public Action<GameObject> OnDead;

    public Action OnPause;
    public Action OnResume;
}
