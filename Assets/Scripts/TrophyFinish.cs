using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public GameObject textToShow;
    private bool canReset = false;

    void Start()
    {
        if (textToShow != null)
            textToShow.SetActive(false);
    }

    private void Update()
    {
        if (canReset && Input.GetKeyDown(KeyCode.E))
        {
            ResetScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            canReset = true;
            textToShow.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            textToShow.SetActive(false);
        }
    }

    void ResetScene()
    {
       UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }




}

