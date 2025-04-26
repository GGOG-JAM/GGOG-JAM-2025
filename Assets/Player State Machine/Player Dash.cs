using JetBrains.Annotations;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : BaseMovementState 
{
    private System.Action<InputAction.CallbackContext> onAttack;

    private Vector2 currentDirection;
    private float timer = 0f;


    public override void OnStateEnter()
    {
        if (PlayerStateMachine.instance.canDash)
        {
            PlayerStateMachine.instance.playerAnimator.SetTrigger("Dash");

            onAttack = i => PlayerStateMachine.instance.ChangeCurrentState(new PlayerSwordAttack1());
            PlayerInputManager.instance.playerInput.Player.Attack.performed += onAttack;

            Dash(currentDirection);
        }
    }
    private void Dash(Vector2 direction)
    {
        PlayerStateMachine.instance.rb.AddForce(direction.normalized * PlayerStateMachine.instance.dashForce, ForceMode2D.Impulse);
    }

    public PlayerDash(Vector2 currentDirection)
    {
        this.currentDirection = currentDirection;
    }

    public override void OnStateUpdate()
    {
        if(timer < PlayerStateMachine.instance.dashTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            PlayerStateMachine.instance.ChangeCurrentState(new PlayerIdle());
        }
    }

    public override void OnStateExit()
    {
        PlayerInputManager.instance.playerInput.Player.Attack.performed -= onAttack;
        PlayerStateMachine.instance.canDash = false;
        PlayerStateMachine.instance.CallDashCourotine(PlayerStateMachine.instance.timeBeforeNextDash);
    }
}
