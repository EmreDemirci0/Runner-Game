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
        //// Saða sola hareket
        //float moveInput = Input.GetAxis("Horizontal");
        ////rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput);
        //rb.AddForce(new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput));

       
        rb.velocity = new Vector3(0f, rb.velocity.y,  moveSpeed); // Yatay ve dikey hareketleri sýfýrlayarak sadece z ekseninde hareket saðlýyoruz.

    }
    void Jump()
    {
        // Yerde olup olmadýðýmýzý kontrol etmek için bir raycast kullanarak groundCheck noktasýndan yere doðru bir çizgi çekiyoruz.
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, 1f, groundLayer);

        // Zýplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
}
