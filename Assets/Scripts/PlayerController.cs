using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 20f;
    [SerializeField] private Animator animator;
    [SerializeField] TMPro.TMP_Text healthText;
    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    private SpriteRenderer spriteRenderer;
    private float xInput;
    private bool isGrounded;
    private int playerHealth = 100;
    private int score = 0;


    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

    }
    void FixedUpdate()
    {
         isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            // Changes color based on whether the player is grounded
            Gizmos.color = isGrounded ? Color.green : Color.red;

            // Draws the circle at the groundCheck position
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }


    private void HandleMovement()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (xInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        if (xInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            playerHealth -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());
            updateHealthText();
            if (playerHealth <= 0)
            {
                Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Saw") || collision.gameObject.CompareTag("Spike"))
        {
            playerHealth -= 10;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());
            updateHealthText();
            if (playerHealth <= 0)
            {
                Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Fire"))
        {
            playerHealth -= 50;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(BlinkRed());
            updateHealthText();
            if (playerHealth <= 0)
            {
                Reset();
            }
        }
        else if (collision.gameObject.CompareTag("Void"))
        {
            Reset();
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }


    private void updateHealthText()
    {
        healthText.text = "Health:" + playerHealth.ToString();
    }


    private void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
