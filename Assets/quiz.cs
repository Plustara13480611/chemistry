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
            // ถ้า Quiz ยังไม่เปิด ให้แสดงข้อความ "กด E"
            if (!quizPanel.activeSelf)
            {
                quizPrompt.SetActive(true);
            }
            else
            {
                // ถ้า Quiz เปิดอยู่ ให้ซ่อนข้อความ "กด E"
                quizPrompt.SetActive(false);
            }

            // กด E
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ถ้า Quiz เปิดอยู่ → ปิด Quiz
                if (quizPanel.activeSelf)
                {
                    quizPanel.SetActive(false);
                }
                // ถ้า Quiz ปิดอยู่ → เปิด Quiz
                else
                {
                    quizPrompt.SetActive(false);
                    quizPanel.SetActive(true);
                }
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