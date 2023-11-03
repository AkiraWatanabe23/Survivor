using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField]
    private CharacterType _characterType = CharacterType.None;

    public CharacterType CharacterType => _characterType;
}

public enum CharacterType
{
    None,
    Player,
    Enemy
}
