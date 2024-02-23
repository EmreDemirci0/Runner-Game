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
    [Range(3f, 15f)] public float moveSpeed = 5f;

    /// <summary>
    /// Playerin Ziplama hizi
    /// </summary>
    public float jumpForce = 10f;

    /// <summary>
    /// Karakterin ziplamasi için Karakterin alt kisminin transformu
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
    [SerializeField] Animator anim;
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
       anim=GetComponent<Animator>();
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
        // Saða sola hareket
        if (Input.GetKeyDown(KeyCode.A))
        {
            value = Mathf.Clamp(value - 1, -1, 1); // value'yi -1 ile 1 arasýnda tutar
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            value = Mathf.Clamp(value + 1, -1, 1); // value'yi -1 ile 1 arasýnda tutar
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetTrigger("isSlide");
        }

        // Hedef pozisyonu belirle
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

        // Yumuþak geçiþ için Lerp fonksiyonunu kullanarak yeni pozisyonu belirle
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerpingDuration * Time.deltaTime);

        // Yatay ve dikey hareketleri sýfýrlayarak sadece z ekseninde hareket saðlýyoruz.
        rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
    }


    /// <summary>
    /// Karaterin ziplama methodu
    /// </summary>
    void Jump()
    {
        // Yerde olup olmadýðýmýzý kontrol etmek için bir raycast kullanarak groundCheck noktasýndan yere doðru bir çizgi çekiyoruz.
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, .3f, groundLayer);

        // Zýplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            print("s");
            anim.SetTrigger("isJump");
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }
    #endregion
}
