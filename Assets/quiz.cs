
using UnityEngine;

public class QuizTrigger : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject quizPrompt;

    private bool playerInside = false;

    void Awake()
    {
        // ปิด Quiz และข้อความตั้งแต่ก่อนเริ่มเกม
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }

        if (quizPrompt != null)
        {
            quizPrompt.SetActive(false);
        }
    }

    void Start()
    {
        // ยืนยันอีกครั้งว่า Quiz ปิดตอนเริ่มเกม
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }

        if (quizPrompt != null)
        {
            quizPrompt.SetActive(false);
        }

        playerInside = false;
    }

    void Update()
    {
        // ถ้าผู้เล่นไม่ได้อยู่ในพื้นที่ Quiz
        if (!playerInside)
        {
            if (quizPrompt != null)
            {
                quizPrompt.SetActive(false);
            }

            return;
        }

        // ถ้า Player อยู่ในพื้นที่ Quiz
        if (quizPanel != null && quizPrompt != null)
        {
            // Quiz ยังไม่เปิด → แสดงข้อความให้กด E
            if (!quizPanel.activeSelf)
            {
                quizPrompt.SetActive(true);
            }
            else
            {
                // Quiz เปิดอยู่ → ซ่อนข้อความ
                quizPrompt.SetActive(false);
            }
        }

        // เปิด Quiz เฉพาะตอนกด E
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("กด E แล้ว | PlayerInside = " + playerInside);

            if (quizPanel == null)
            {
                Debug.LogError("QuizPanel ยังไม่ได้ใส่ใน QuizTrigger!");
                return;
            }

            // ถ้า Quiz เปิดอยู่ → ปิด
            if (quizPanel.activeSelf)
            {
                quizPanel.SetActive(false);

                Debug.Log("ปิด QuizPanel");
            }
            // ถ้า Quiz ปิดอยู่ → เปิด
            else
            {
                quizPanel.SetActive(true);

                if (quizPrompt != null)
                {
                    quizPrompt.SetActive(false);
                }

                Debug.Log("เปิด QuizPanel จากการกด E");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            Debug.Log("Player เข้า Quiz Trigger");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;

            if (quizPrompt != null)
            {
                quizPrompt.SetActive(false);
            }

            Debug.Log("Player ออกจาก Quiz Trigger");
        }
    }
}

