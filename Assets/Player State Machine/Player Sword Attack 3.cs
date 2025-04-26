using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSwordAttack3 : BaseMovementState
{
    private System.Action<InputAction.CallbackContext> onAttack;

    private float timer = 0f;
    private bool attack1Flag = false;

    public override void OnStateEnter()
    {
        PlayerStateMachine.instance.playerAnimator.SetTrigger("Sword Attack 3");
        onAttack = i => { attack1Flag = true; };
        PlayerInputManager.instance.playerInput.Player.Attack.performed += onAttack;

    }

    public override void OnStateUpdate()
    {
        if (timer < PlayerStateMachine.instance.swordattack1Time)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (attack1Flag)
            {
                PlayerStateMachine.instance.ChangeCurrentState(new PlayerSwordAttack1());
            }
            else
            {
                PlayerStateMachine.instance.ChangeCurrentState(new PlayerIdle());
            }
        }
    }

    public override void OnStateFixedUpdate()
    {
        PlayerStateMachine.instance.rb.AddForce(PlayerInputManager.instance.playerInput.Player.Movement.ReadValue<Vector2>().normalized * PlayerStateMachine.instance.forcePower);
    }

    public override void OnStateExit()
    {
        PlayerInputManager.instance.playerInput.Player.Attack.performed -= onAttack;
    }
}
