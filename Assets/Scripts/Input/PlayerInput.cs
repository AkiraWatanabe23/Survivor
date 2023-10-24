using UnityEngine;

public class PlayerInput : InputBase
{
    public override Vector2 MoveInput => new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
}
