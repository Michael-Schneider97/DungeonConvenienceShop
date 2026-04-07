using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float walkSpd = 5f;
    [SerializeField] public float runSpd = 10f;
    [SerializeField] public float jump = 5f;
    [SerializeField] public float grav = -9.81f;

    [Header("Player Camera")]
    [SerializeField] public float mouseSens = 0.2f;
    [SerializeField] public float camWidth = 90f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private Vector2 lookInput;
    private Vector2 moveInput;
    private PlayerInput inputActions;
    private bool isOnGround;
    private float currentSpd = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        inputActions = new PlayerInput();
        inputActions.Player.Jump.performed += ctx => handleJump();
        currentSpd = walkSpd;
    }

    public void OnMove(InputValue val)
    {
        moveInput = val.Get<Vector2>();
    }

    public void OnJump()
    {
        if (isOnGround)
            velocity.y = Mathf.Sqrt(jump * -2f * grav);
    }

    public void OnSprint(InputValue value)
    {
        currentSpd = value.isPressed ? walkSpd : runSpd;
    }

    // Update is called once per frame
    void Update()
    {
        handleSprint();

        isOnGround = controller.isGrounded;
        if (isOnGround && velocity.y < 0)
            velocity.y = -2f;

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * Time.deltaTime * currentSpd);

        velocity.y += grav * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

};