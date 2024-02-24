using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
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
    [Range(5f, 20f)] public float moveSpeed = 5f;

    /// <summary>
    /// Playerin Ziplama hizi
    /// </summary>
    public float jumpForce = 10f;

    /// <summary>
    /// Karakterin ziplamasi icin Karakterin alt kisminin transformu
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
    bool isGrounded;

    /// <summary>
    /// Karakterin Yerde mi Kontrolu icin uzaklik degeri
    /// </summary>
    public float jumpIsGroundedCheckDistance = .3f;

    /// <summary>
    /// Playerin sagda mi ortada mi sol da mi oldugunu tutan degisken
    /// </summary>
    int value;

    /// <summary>
    /// Playerin saga sola giderkenki lerp degeri
    /// </summary>
    public float lerpingDuration = 0.2f; // Lerpleme süresi

    /// <summary>
    /// Playerin animasyonu
    /// </summary>
    Animator anim;
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
        anim = GetComponent<Animator>();
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
        if (Input.GetKeyDown(KeyCode.A))
        {
            value = Mathf.Clamp(value - 1, -1, 1); // value'yi -1 ile 1 arasinda tutar
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            value = Mathf.Clamp(value + 1, -1, 1); // value'yi -1 ile 1 arasinda tutar
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("isSlide");
        }

        Vector3 targetPosition = Vector3.zero;
        switch (value)
        {
            case -1:
                targetPosition = new Vector3(2, transform.position.y, transform.position.z);
                break;
            case 0:
                targetPosition = new Vector3(5, transform.position.y, transform.position.z);
                break;
            case 1:
                targetPosition = new Vector3(8, transform.position.y, transform.position.z);
                break;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpingDuration * Time.deltaTime);

        rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
    }


    /// <summary>
    /// Karaterin ziplama methodu
    /// </summary>
    void Jump()
    {
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, jumpIsGroundedCheckDistance, groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            anim.SetTrigger("isJump");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    #endregion
}
