using UnityEngine;

public class HealthData : MonoBehaviour
{
    [SerializeField]
    private int _hp = 10;
    [SerializeField]
    private int _maxHP = 10;

    public int HP { get => _hp; set => _hp = value; }
    public int MaxHP => _maxHP;
}
