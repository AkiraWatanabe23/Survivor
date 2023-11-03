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
    private GameState _gameState = new();
    [SerializeField]
    private MovementSystem _movementSystem = new();
    [SerializeField]
    private SpawnSystem _spawnSystem = new();

    private GameEvent _gameEvent = default;
    private bool _isPause = false;

    private void Start()
    {
        _gameEvent = new();

        _gameEvent.OnPause += Pause;
        _gameEvent.OnResume += Resume;

        if (_flags.Movement) { _movementSystem.Initialize(_gameEvent, _gameState); }
        if (_flags.Spawn) { _spawnSystem.Initialize(_gameEvent, _gameState);}
    }

    private void Update()
    {
        if (_isPause) { return; }

        if (_flags.Movement) { _movementSystem.OnUpdate(); }
        if (_flags.Spawn) { _spawnSystem.OnUpdate(); }
    }

    private void OnDestroy()
    {
        _gameEvent.OnPause -= Pause;
        _gameEvent.OnResume -= Resume;

        if (_flags.Movement) { _movementSystem.OnDestroy(_gameEvent); }
        if (_flags.Spawn) { _spawnSystem.OnDestroy(_gameEvent); }
    }

    private void Pause() { _isPause = true; }

    private void Resume() { _isPause = false; }
}
