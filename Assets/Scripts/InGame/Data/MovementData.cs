using UnityEngine;
using UnityEngine.AI;

/// <summary> 移動に関するデータのみを保持するクラス </summary>
public class MovementData : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 1f;
    [SerializeField]
    private float _rotateSpeed = 1f;

    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public Transform Transform { get; set; }
    public CharacterType CharacterType { get; set; }
    public NavMeshAgent Agent { get; set; }
}
