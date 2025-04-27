using UnityEngine;

public abstract class BaseMovementState
{
    public virtual void OnStateEnter()
    {

    }

    public virtual void OnStateUpdate()
    {

    }

    public virtual void OnStateFixedUpdate()
    {

    }
    public virtual void OnStateExit()
    {

    }

    public void ChangeAttackColliderDirection(ref Transform transform,Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.normalized.y, direction.normalized.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }


    
    
}
