using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Constants
    const string WALKING_ANIMATION_NAME = "Walking";
    const string CLIMB_ANIMATION_NAME = "Climbing";
    const string DEATH_ANIMATION_NAME = "Dying";
    const string GROUND_LAYER_NAME = "Ground";
    const string LADDERS_LAYER_NAME = "Ladder";
    const string ENEMY_LAYER_NAME = "Enemy";
    const string HAZARDS_LAYER_NAME = "Hazards";

    // Config
    [SerializeField] float walkSpeed = 3f;
    [SerializeField] float climbSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(15f, 20f);
    [SerializeField] float SecondsToReloadOnDeath = 1f;

    // State
    bool isAlive = true;

    // Cached components references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    float gravityScaleAtStart;

    // Input
    InputAction moveAction;
    InputAction jumpAction;

    void Awake()
    {
        moveAction = new InputAction("Move", InputActionType.Value, expectedControlType: "Vector2");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Up", "<Keyboard>/upArrow")
            .With("Down", "<Keyboard>/s")
            .With("Down", "<Keyboard>/downArrow")
            .With("Left", "<Keyboard>/a")
            .With("Left", "<Keyboard>/leftArrow")
            .With("Right", "<Keyboard>/d")
            .With("Right", "<Keyboard>/rightArrow");
        moveAction.AddBinding("<Gamepad>/leftStick");

        jumpAction = new InputAction("Jump", InputActionType.Button, binding: "<Keyboard>/space");
        jumpAction.AddBinding("<Gamepad>/buttonSouth");
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; }
        Walk();
        Jump();
        ClimbLadder();
        FlipSprite();
        Die();
    }

    private void Walk()
    {
        MoveHorizontally();
        ChangeWalkingAnimationState();
    }

    private void Jump()
    {
        if (!PlayerIsOnGround()) { return; }
        if (jumpAction.WasPressedThisFrame())
        {
            myRigidBody.linearVelocity = new Vector2(myRigidBody.linearVelocity.x, jumpSpeed);
        }
    }

    private void ClimbLadder()
    {
        if (!PlayerIsTouchingLadder())
        {
            myRigidBody.gravityScale = gravityScaleAtStart;
            ChangeClimbingAnimationState(false);
            return;
        }
        MoveVertically();
        ChangeClimbingAnimationState(PlayerHasVerticalSpeed());
        myRigidBody.gravityScale = 0;
    }


    private void MoveVertically()
    {
        float controlThrow = moveAction.ReadValue<Vector2>().y; //value between -1 to +1
        Vector2 playerClimbVelocity = new Vector2(myRigidBody.linearVelocity.x, controlThrow * climbSpeed);
        myRigidBody.linearVelocity = playerClimbVelocity;
    }

    private void MoveHorizontally()
    {
        float controlThrow = moveAction.ReadValue<Vector2>().x; //value between -1 to +1
        Vector2 playerWalkVelocity = new Vector2(controlThrow * walkSpeed, myRigidBody.linearVelocity.y);
        myRigidBody.linearVelocity = playerWalkVelocity;
    }

    private void ChangeClimbingAnimationState(bool state)
    {
        myAnimator.SetBool(CLIMB_ANIMATION_NAME, state);
    }

    private void ChangeWalkingAnimationState()
    {
        myAnimator.SetBool(WALKING_ANIMATION_NAME, PlayerHasHorizontalSpeed());
    }

    private void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.linearVelocity.x), 1f);
        }
    }

    private bool PlayerIsOnGround()
    {
        return myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask(GROUND_LAYER_NAME));
    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(myRigidBody.linearVelocity.x) > Mathf.Epsilon;
    }

    private bool PlayerHasVerticalSpeed()
    {
        return Mathf.Abs(myRigidBody.linearVelocity.y) > Mathf.Epsilon;
    }

    private bool PlayerIsTouchingLadder()
    {
        return myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask(LADDERS_LAYER_NAME));
    }

    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask(ENEMY_LAYER_NAME, HAZARDS_LAYER_NAME)) && isAlive)
        {
            isAlive = false;
            myRigidBody.linearVelocity = deathKick;
            myAnimator.SetTrigger(DEATH_ANIMATION_NAME);
            myBodyCollider2D.enabled = false;
            StartCoroutine(ProcessDeath());
        }
    }

    private IEnumerator ProcessDeath()
    {
        yield return new WaitForSecondsRealtime(SecondsToReloadOnDeath);
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
