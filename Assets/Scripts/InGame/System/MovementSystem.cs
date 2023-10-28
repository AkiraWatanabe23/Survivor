using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

/// <summary> 方向転換、移動のシステム </summary>
[Serializable]
public class MovementSystem : SystemBase
{
    [SerializeField]
    private List<MovementData> _movementData = default;

    private InputBase _input = default;
    private Transform _player = default;

    private readonly CancellationTokenSource _ctsLookAt = new();
    private readonly CancellationTokenSource _ctsMovement = new();

    /// <summary> 初期化処理 </summary>
    public override async void Initialize(GameEvent gameEvent)
    {
        if (_movementData == null || _movementData.Count == 0) { return; }

        foreach (var movement in _movementData)
        {
            TransformSetting(movement);

            if (_input == null && movement.gameObject.TryGetComponent(out _input))
            {
                _player = movement.Transform;
            }
        }

        gameEvent.OnActivate += AddData;
        gameEvent.OnDead += RemoveData;

        Debug.Log("initialized");

        var lookAtTask = LookAtAsync(_ctsLookAt.Token);
        var movementTask = MoveAsync(_ctsMovement.Token);

        await lookAtTask;
        await movementTask;
    }

    public override void OnDestroy(GameEvent gameEvent)
    {
        _ctsLookAt?.Cancel(); _ctsLookAt?.Dispose();
        _ctsMovement?.Cancel(); _ctsMovement?.Dispose();

        gameEvent.OnActivate -= AddData;
        gameEvent.OnDead -= RemoveData;
    }

    private async Task LookAtAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested) { LookAt(); await Task.Yield(); }
    }

    private async Task MoveAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested) { Move(); await Task.Yield(); }
    }

    /// <summary> 方向転換処理 </summary>
    private void LookAt()
    {
        foreach (var movement in _movementData) //移動ステータスを持つキャラクターの方向を動かす
        {
            if (movement.IsGetInput)
            {
                var direction = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);

                if (direction != Vector3.zero)
                {
                    Quaternion rot = Quaternion.LookRotation(direction);
                    movement.Transform.rotation =
                        Quaternion.Slerp(movement.Transform.rotation, rot, Time.deltaTime * movement.RotateSpeed);
                }
            }
            else
            {
                var direction = (_player.position - movement.Transform.position).normalized;
                direction.y = 0;

                var lookRotation = Quaternion.LookRotation(direction * movement.RotateSpeed, Vector3.up);
                movement.Transform.rotation = lookRotation;
            }
        }
    }

    /// <summary> 移動処理 </summary>
    private void Move()
    {
        foreach (var movement in _movementData) //移動ステータスを持つキャラクターを動かす
        {
            if (movement.IsGetInput)
            {
                movement.Transform.position +=
                    new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y) * movement.MoveSpeed * Time.deltaTime;
            }
            else
            {
                movement.Transform.Translate(
                    new Vector3(0f, 0f, Time.deltaTime * movement.MoveSpeed));
            }
        }
    }

    private void AddData(GameObject go)
    {
        if (go.TryGetComponent(out MovementData movement))
        {
            _movementData.Add(movement);
            TransformSetting(movement);
        }
    }

    private void RemoveData(GameObject go)
    {
        if (go.TryGetComponent(out MovementData movement))
        {
            _movementData.Remove(movement);
        }
    }

    private void TransformSetting(MovementData movement)
    {
        movement.Transform ??= movement.gameObject.transform;
    }
}
