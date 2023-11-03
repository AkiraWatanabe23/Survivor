using System;
using UnityEngine;

/// <summary> インゲーム全体を通して共通して使用するデータを保持するクラス </summary>
[Serializable]
public class GameState
{
    [SerializeField]
    private CharacterData[] _characters = default;

    public CharacterData[] Characters => _characters;
}
