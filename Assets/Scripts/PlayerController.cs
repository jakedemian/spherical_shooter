using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    private float rotation;
    private Rigidbody rb;
    private Vector2 moveInput;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update ()
    {
        rotation = Input.GetAxisRaw("Horizontal");
        ReadInput();
    }
    
    private void ReadInput() {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate ()
    {
        if (Util.Vector2IsFull(moveInput)) {
            moveInput.x /= Mathf.Sqrt(2);
            moveInput.y /= Mathf.Sqrt(2);
        }
        
        rb.MovePosition(rb.position + transform.forward * moveSpeed * moveInput.y * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + transform.right * moveSpeed * moveInput.x * Time.fixedDeltaTime);
        
        Vector3 yRotation = Vector3.up * rotation * rotationSpeed * Time.fixedDeltaTime;
        Quaternion deltaRotation = Quaternion.Euler(yRotation);
        Quaternion targetRotation = rb.rotation * deltaRotation;
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, 50f * Time.deltaTime));
        //transform.Rotate(0f, rotation * rotationSpeed * Time.fixedDeltaTime, 0f, Space.Self);
    }

}