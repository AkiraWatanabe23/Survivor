using UnityEngine;

public class AttackData : MonoBehaviour
{
    [SerializeField]
    private int _attackValue = 1;
    [SerializeField]
    private float _attackInterval = 1f;

    public int AttackValue
    {
        get => _attackValue;
        set => _attackValue = value;
    }
    public float AttackInterval
    {
        get => _attackInterval;
        set => _attackInterval = value;
    }
}
