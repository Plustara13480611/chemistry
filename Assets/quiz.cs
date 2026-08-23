using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject quizPrompt;

    private bool playerInside = false;

    void Start()
    {
        quizPanel.SetActive(false);
        quizPrompt.SetActive(false);
    }

    void Update()
    {
        if (playerInside)
        {
            quizPrompt.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                quizPrompt.SetActive(false);
                quizPanel.SetActive(true);
            }
        }
        else
        {
            quizPrompt.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}