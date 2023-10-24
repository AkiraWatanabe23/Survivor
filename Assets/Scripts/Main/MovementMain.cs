using UnityEngine;

public class MovementMain : MonoBehaviour
{
    [SerializeField]
    private MovementSystem _movementSystem = new();

    private void Start()
    {
        _movementSystem.Initialize();
    }

    private void Update()
    {
        _movementSystem.OnUpdate();
    }
}
