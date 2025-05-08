using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))] // Ensure there's an Animator
public class CharacterControllerFPS : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float jumpForce = 10f;
    public float gravityScale = 0.4f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 2f;
    private Transform playerCamera;

    private Rigidbody rb;
    private bool isGrounded;
    private float cameraPitch = 0f;

    // Animation reference
    private Animator animator;

    // Oxygen system
    private OxygenCounter oxygenCounter;
    public int O2Tank_refill_Amount = 10;
    public AudioClip oxygen_Refill_Sound;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>(); // Get the Animator

        if (Camera.main != null)
        {
            playerCamera = Camera.main.transform;
        }
        else
        {
            Debug.LogError("No Main Camera found in the scene.");
        }

        GameObject oxygenManager = GameObject.Find("OxygenLevelManager");
        if (oxygenManager != null)
        {
            oxygenCounter = oxygenManager.GetComponent<OxygenCounter>();
            if (oxygenCounter == null)
                Debug.LogError("OxygenCounter script not found on OxygenLevelManager.");
        }
        else
        {
            Debug.LogError("OxygenLevelManager GameObject not found in the scene.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        ApplyCustomGravity();
    }

    private void HandleMouseLook()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -75f, 75f);
        playerCamera.localEulerAngles = new Vector3(cameraPitch, 0f, 0f);
    }

    private void HandleMovement()
    {
       float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        // This line applies movement, but we should stop if no input:
        rb.linearVelocity = new Vector3(move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);

        // Only walk if actually pressing movement keys:
        bool isWalking = move.magnitude > 0.1f;
        animator.SetBool("isWalking", isWalking);
        Debug.Log("isWalking = " + isWalking);
 

        if (move.magnitude < 0.1f)
{
         rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f); // Stop movement
}



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void ApplyCustomGravity()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * gravityScale * 9.81f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("OxygenTank"))
        {
            Debug.Log("COLLISION WITH O2 TANK");

            handleO2Collisions();

            if (oxygen_Refill_Sound != null)
            {
                AudioSource.PlayClipAtPoint(oxygen_Refill_Sound, collision.transform.position);
            }

            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void handleO2Collisions()
    {
        if (oxygenCounter != null)
        {
            oxygenCounter.oxygenLevel += O2Tank_refill_Amount;
            if (oxygenCounter.oxygenLevel > 100)
                oxygenCounter.oxygenLevel = 100;

            oxygenCounter.setOxygen(oxygenCounter.oxygenLevel);
        }
        else
        {
            Debug.LogWarning("oxygenCounter reference not set.");
        }
    }




}
