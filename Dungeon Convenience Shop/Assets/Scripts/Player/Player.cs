using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] public float walkSpd = 5f;
    [SerializeField] public float runSpd = 10f;
    [SerializeField] public float jump = 5f;

    [Header("Player Camera")]
    [SerializeField] public float mouseSens = 2f;
    [SerializeField] public float camWidth = 90f;

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        //HandleJump();
        //HandleGrav();
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -camWidth, camWidth);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpd : walkSpd;
        controller.Move(move * speed * Time.deltaTime);
    }
}
