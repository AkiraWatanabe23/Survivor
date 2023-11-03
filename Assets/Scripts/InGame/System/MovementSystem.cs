using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> 方向転換、移動のシステム </summary>
[Serializable]
public class MovementSystem : SystemBase
{
    [SerializeField]
    private List<MovementData> _movementDatas = default;

    private InputBase _input = default;
    private Transform _player = default;

    /// <summary> 初期化処理 </summary>
    public override void Initialize(GameEvent gameEvent, GameState gameState)
    {
        //シーン上のオブジェクトから移動ステータスを持っているオブジェクトを抽出
        _movementDatas ??= new();
        foreach (var obj in UnityEngine.Object.FindObjectsOfType<GameObject>())
        {
            if (obj.TryGetComponent(out MovementData movement)) { _movementDatas.Add(movement); }
        }

        foreach (var movement in _movementDatas)
        {
            MovementDataSetting(movement);

            if (_input == null && movement.gameObject.TryGetComponent(out _input))
            {
                _player = movement.Transform;
            }

            if (movement.gameObject.TryGetComponent(out CharacterData character))
            {
                movement.CharacterType = character.CharacterType;
                if (movement.CharacterType == CharacterType.Enemy) //EnemyにNavMeshAgentを設定
                {
                    movement.Agent =
                        movement.gameObject.TryGetComponent(out NavMeshAgent agent) ?
                        agent : movement.gameObject.AddComponent<NavMeshAgent>();
                }
            }
        }

        gameEvent.OnActivate += AddData;
        gameEvent.OnDead += RemoveData;
    }

    public override void OnUpdate()
    {
        foreach (var movement in _movementDatas)
        {
            LookAt(movement);
            Move(movement);
        }
    }

    public override void OnDestroy(GameEvent gameEvent)
    {
        gameEvent.OnActivate -= AddData;
        gameEvent.OnDead -= RemoveData;
    }

    /// <summary> 方向転換処理 </summary>
    private void LookAt(MovementData movement)
    {
        if (movement.CharacterType != CharacterType.Player) { return; }

        var direction = new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y);

        if (direction != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(direction);
            movement.Transform.rotation =
                Quaternion.Slerp(movement.Transform.rotation, rot, Time.deltaTime * movement.RotateSpeed);
        }
    }

    /// <summary> 移動処理 </summary>
    private void Move(MovementData movement)
    {
        if (movement.CharacterType == CharacterType.Player) //for input
        {
            movement.Transform.position +=
                new Vector3(_input.MoveInput.x, 0f, _input.MoveInput.y) * movement.MoveSpeed * Time.deltaTime;
        }
        else if (movement.CharacterType == CharacterType.Enemy) //navmesh
        {
            movement.Agent.SetDestination(_player.position);
        }
        else
        {
            movement.Transform.Translate(
                new Vector3(0f, 0f, Time.deltaTime * movement.MoveSpeed));
        }
    }

    private void AddData(GameObject go)
    {
        if (go.TryGetComponent(out MovementData movement))
        {
            _movementDatas.Add(movement);
            MovementDataSetting(movement);
        }
    }

    private void RemoveData(GameObject go)
    {
        if (go.TryGetComponent(out MovementData movement))
        {
            _movementDatas.Remove(movement);
        }
    }

    private void MovementDataSetting(MovementData movement)
    {
        movement.Transform ??= movement.gameObject.transform;
        if (movement.Agent != null) { movement.Agent.speed = movement.MoveSpeed; }
    }
}
