using UnityEngine;
using TMPro;

public class PlayerScoring : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    private int totalScore = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Apple"))
        {
            AddScore(20);
            Destroy(collision.gameObject); 
        }
        else if (collision.CompareTag("Banana"))
        {
            AddScore(10);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Strawberry"))
        {
            AddScore(50);
            Destroy(collision.gameObject);
        }
    }

    void AddScore(int amount)
    {
        totalScore += amount;
        scoreText.text = "Score: " + totalScore.ToString();
    }
}
