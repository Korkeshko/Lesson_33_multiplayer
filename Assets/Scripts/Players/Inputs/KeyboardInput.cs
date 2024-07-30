using UnityEngine;

public class KeyboardInput : IInput
{
    public float Direction()
    {
        return Input.GetAxis("Horizontal");
    }

    public bool Jump()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    public bool Shoot()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}
