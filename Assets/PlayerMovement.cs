using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody rb;
    public/**/ bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();

        Movement();



    }
    void Movement()
    {
        //// Sa�a sola hareket
        //float moveInput = Input.GetAxis("Horizontal");
        ////rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput);
        //rb.AddForce(new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput));

       
        rb.velocity = new Vector3(0f, rb.velocity.y,  moveSpeed); // Yatay ve dikey hareketleri s�f�rlayarak sadece z ekseninde hareket sa�l�yoruz.

    }
    void Jump()
    {
        // Yerde olup olmad���m�z� kontrol etmek i�in bir raycast kullanarak groundCheck noktas�ndan yere do�ru bir �izgi �ekiyoruz.
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 1f, groundLayer);

        // Z�plama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
