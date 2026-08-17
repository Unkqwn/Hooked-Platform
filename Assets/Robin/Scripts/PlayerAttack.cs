using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : AttackBase
{
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Attack(transform.forward);
        }
    }
}