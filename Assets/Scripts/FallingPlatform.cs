using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.2f;
    [SerializeField] private float respawnDelay = 4f;
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Rigidbody2D rb;


    private bool falling = false;

    private void Awake()
    {
        initialPosition = transform.position;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (falling) return;

        if (collision.transform.tag == "Player")
        {
            StartCoroutine(StartFall());
            StartCoroutine(RespawnSequence());
        }
    }

    private IEnumerator StartFall()
    {
        falling = true;

        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    IEnumerator RespawnSequence()
    {
    
        yield return new WaitForSeconds(respawnDelay);

     
        rb.bodyType = RigidbodyType2D.Kinematic;

    
        rb.linearVelocity = Vector2.zero;

    
        transform.position = initialPosition;
    }

}
