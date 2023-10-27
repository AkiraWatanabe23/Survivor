using System;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    #region Debug Flags
    [Serializable]
    public class SystemFlags
    {
        [SerializeField]
        private bool _movement = false;
        [SerializeField]
        private bool _attack = false;
        [SerializeField]
        private bool _spawn = false;

        public bool Movement => _movement;
        public bool Attack => _attack;
        public bool Spawn => _spawn;
    }
    #endregion

    [Tooltip("どの動きのテストを行うかのフラグ集")]
    [SerializeField]
    private SystemFlags _flags = new();
    [SerializeField]
    private MovementSystem _movementSystem = new();

    private GameEvent _gameEvent = default;

    private void Start()
    {
        _gameEvent = new();

        if (_flags.Movement) { _movementSystem.Initialize(_gameEvent); }
    }

    private void OnDestroy()
    {
        if (_flags.Movement) { _movementSystem.OnDestroy(); }
    }
}
