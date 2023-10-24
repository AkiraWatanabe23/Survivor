using UnityEngine;

/// <summary> 移動に関するデータのみを保持するクラス </summary>
public class MovementData : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _rotateSpeed = 1f;
    [Tooltip("入力を受け付けるかどうか")]
    [SerializeField]
    private bool _isGetInput = false;

    private Transform _transform = default;
    private Vector3 _latestPos = Vector3.zero;

    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public bool IsGetInput => _isGetInput;
    public Transform Transform { get => _transform; set => _transform = value; }
    public Vector3 LatestPos { get => _latestPos; set => _latestPos = value; }
}
