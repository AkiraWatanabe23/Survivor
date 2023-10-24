using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MovementSystem
{
    [SerializeField]
    private List<MovementData> _movementData = default;

    private InputBase _input = default;
    private Transform _player = default;

    public void Initialize()
    {
        if (_movementData == null || _movementData.Count == 0) { return; }

        foreach (var movement in _movementData)
        {
            TransformSetting(movement);

            if (_input == null && movement.gameObject.TryGetComponent(out _input))
            {
                _player = movement.Transform;
                movement.LatestPos = movement.Transform.position;
            }
        }
    }

    public void OnUpdate()
    {
        if (_movementData == null || _movementData.Count == 0) { return; }

        LookAt();
        Move();
    }

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

                var lookRotation = Quaternion.LookRotation(direction, Vector3.up);
                movement.Transform.rotation = lookRotation;
            }
        }
    }

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

    private void TransformSetting(MovementData movement)
    {
        movement.Transform = movement.transform;
    }
}
