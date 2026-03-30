using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;
    public event EventHandler OnPlayerJump;


    [HideInInspector] public Transform _player;
    private PlayerInputActions inputActions;
    private CharacterController controller;

    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isJumping;
    private float verticalVelocity;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float maxTurnAngle = 30f; // Max angle to rotate on turning
    private float currentYRotation = 0f;

    [Header("Other")]
    [SerializeField] private float jumpDelay = 0.05f; // Delay before jump action is executed

    private Animator _animator;
    private const string JUMP = "Jump";
    private const string IS_GROUNDED = "IsGrounded";

    private bool isDead = false;

    private void Awake()
    {
        Instance = this;
        _player = transform;
        _animator = GetComponent<Animator>();

        inputActions = new PlayerInputActions();
        controller = GetComponent<CharacterController>();


    }

    private void Start()
    {
        isDead = false;

    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Jump.performed -= OnJump;
    }

    private void Update()
    {
        isDead = PlayerCollisionDetection.Instance.GetIfIsDead();
        if (isDead) return;

        Movement();
        Rotation();
        GroundedChecker();

        _animator.SetBool(IS_GROUNDED, isGrounded);
    }

    private void Rotation()
    {
        Vector3 moveDirection = new Vector3(moveInput.x, 0f, 0f); // just example
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);


        float targetYRotation = 0f;

        if (moveInput.x > 0.1f)
            targetYRotation = maxTurnAngle;
        else if (moveInput.x < -0.1f)
            targetYRotation = -maxTurnAngle;
        else
            targetYRotation = 0f; // Return to center when idle

        // Smoothly interpolate toward target rotation
        currentYRotation = Mathf.Lerp(currentYRotation, targetYRotation, rotationSpeed * Time.deltaTime);

        // Clamp final Y rotation
        currentYRotation = Mathf.Clamp(currentYRotation, -maxTurnAngle, maxTurnAngle);

        // Apply rotation only on Y axis
        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);


    }

    private void Movement()
    {
        // Get movement input
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();

        // Movement vector
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);




        // Apply vertical movement (jump/fall)
        controller.Move(velocity * Time.deltaTime);

        //Apply rotation
    }

    private void GroundedChecker()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // small negative to stick to ground

        }

        // Apply gravity
        verticalVelocity += gravity * Time.deltaTime;
        velocity.y = verticalVelocity;

        
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            StartCoroutine(JumpAction());
            OnPlayerJump?.Invoke(this, EventArgs.Empty);

        }
    }

    private IEnumerator JumpAction()
    {
        yield return new WaitForSeconds(jumpDelay);


        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        _animator.SetBool(JUMP, true);

        yield return new WaitForSeconds(.2f);

        _animator.SetBool(JUMP, false);

    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void DefficultyController()
    {

    }
}



//private void KeyBoardMovement()
//{
//    Vector3 newPosition;

//    if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > minX)
//    {
//        newPosition = transform.position + new Vector3(-Speed * Time.deltaTime, 0, 0);
//        transform.position = newPosition;
//    }

//    if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < maxX)
//    {
//        newPosition = transform.position + new Vector3(Speed * Time.deltaTime, 0, 0);
//        transform.position = newPosition;
//    }



//}
