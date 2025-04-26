using System;
using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public static PlayerStateMachine instance { get; private set; }

    [Header("Move State")]
    [HideInInspector] public Rigidbody2D rb;
    public float forcePower;
    public Transform attackColliderPivotTransform;

    [Header("Dash State")]
    public float dashTime;
    public float dashForce;
    [HideInInspector] public Vector2 dashDirection;
    [HideInInspector] public bool canDash;
    public float timeBeforeNextDash;


    [Header("Sword Combo States")]
    public float swordattack1Time;
    public float swordattack2Time;
    public float swordattack3Time;

    [HideInInspector] public BaseMovementState currentState;

    [HideInInspector] public Animator playerAnimator;
    

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        rb = GetComponent<Rigidbody2D>();

        playerAnimator = GetComponentInChildren<Animator>();

        canDash = true;

        
    }

    private void Start()
    {
        //Buranýn Nasýl Çalýþtýðýný Bilmiyorum.Movement Set Trigger Yapmazsam Bozuk Çalýþýyor
        ChangeCurrentState(new PlayerIdle());
        playerAnimator.SetTrigger("Movement");
    }

    public void ChangeCurrentState(BaseMovementState state)
    {
        currentState?.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    private void Update()
    {

        currentState.OnStateUpdate();

        
    }

    public void CallDashCourotine(float time)
    {
        StartCoroutine(WaitForNextDash(time));
    }

    private IEnumerator WaitForNextDash(float time)
    {
        yield return new WaitForSeconds(time);
        canDash = true;
    }

    private void FixedUpdate()
    {
        currentState.OnStateFixedUpdate();
    }
}
