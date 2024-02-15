using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields

    /// <summary>
    /// Singleton
    /// </summary>
    public static PlayerMovement Instance { get; private set; }

    /// <summary>
    /// Playerin hizi, 3 ila 15 arasinda deger aliyor.
    /// </summary>
    [Range(3f, 15f)] public float moveSpeed = 5f;

    /// <summary>
    /// Playerin Ziplama hizi
    /// </summary>
    public float jumpForce = 10f;

    /// <summary>
    /// Karakterin ziplamasi i�in Karakterin alt kisminin transformu
    /// </summary>
    public Transform groundCheck;

    /// <summary>
    /// Ziplamasi icin Zemin Layeri
    /// </summary>
    public LayerMask groundLayer;

    /// <summary>
    /// Karakter RigidBodysi
    /// </summary>
    private Rigidbody rb;

    /// <summary>
    /// Karakterin Yerde mi Kontrolu
    /// </summary>
    public/**/ bool isGrounded;


    #endregion

    #region Methods
    /// <summary>
    /// Singleton atamalari yapilan kisim
    /// </summary>
    private void Awake()
    {

        if (Instance != null && Instance != this)
            Destroy(this);

        else
            Instance = this;

    }

    /// <summary>
    /// Rigidbody gibi degiskenlerin atandigi Start Methodu
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
   
    /// <summary>
    /// Ziplama ve Hareket icin Update Methodu
    /// </summary>
    void Update()
    {
        Jump();

        Movement();



    }
    
    /// <summary>
    /// Karaterin hareket methodu
    /// </summary>
    void Movement()
    {
        //// Sa�a sola hareket
        //float moveInput = Input.GetAxis("Horizontal");
        ////rb.velocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput);
        //rb.AddForce(new Vector3(moveInput * moveSpeed, rb.velocity.y, moveInput));


        rb.velocity = new Vector3(0f, rb.velocity.y, moveSpeed); // Yatay ve dikey hareketleri s�f�rlayarak sadece z ekseninde hareket sa�l�yoruz.

    }

    /// <summary>
    /// Karaterin ziplama methodu
    /// </summary>
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
    #endregion
}
