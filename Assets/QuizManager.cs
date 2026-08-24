using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text resultText;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;

    // =========================
    // Stage Complete UI
    // =========================

    public GameObject quizPanel;
    public GameObject stageCompletePanel;

    public TMP_Text stageTitleText;
    public TMP_Text stageScoreText;

    public Button continueButton;
    public Button retryButton;

    // ชื่อ Scene ของ Stage 2
    public string nextSceneName = "scene2";


    // =========================
    // คำถาม
    // =========================

    string[] questions =
    {
        "สารที่มีอยู่ก่อนเกิดปฏิกิริยาเคมีเรียกว่าอะไร?",
        "สารที่เกิดขึ้นหลังปฏิกิริยาเคมีเรียกว่าอะไร?",
        "ข้อใดเป็นสัญญาณที่อาจบ่งบอกว่าเกิดปฏิกิริยาเคมี?",
        "สมการใดดุลถูกต้อง?"
    };


    // =========================
    // คำตอบ A B C D
    // =========================

    string[,] answers =
    {
        {
            "ผลิตภัณฑ์",
            "สารตั้งต้น",
            "ตัวทำละลาย",
            "ตัวเร่ง"
        },

        {
            "สารตั้งต้น",
            "ตัวทำละลาย",
            "ผลิตภัณฑ์",
            "ตัวบ่งชี้"
        },

        {
            "เกิดแก๊ส",
            "เปลี่ยนรูปร่างอย่างเดียว",
            "ถูกตัดเป็นชิ้นเล็กลง",
            "เปลี่ยนตำแหน่ง"
        },

        {
            "H<sub>2</sub> + O<sub>2</sub> → H<sub>2</sub>O",
            "2H<sub>2</sub> + O<sub>2</sub> → 2H<sub>2</sub>O",
            "H<sub>2</sub> + 2O<sub>2</sub> → H<sub>2</sub>O",
            "2H<sub>2</sub> + 2O<sub>2</sub> → H<sub>2</sub>O"
        }
    };


    // =========================
    // คำตอบที่ถูก
    // 0 = A
    // 1 = B
    // 2 = C
    // 3 = D
    // =========================

    int[] correctAnswers = { 1, 2, 0, 1 };


    // =========================
    // ตัวแปรระบบ
    // =========================

    int currentQuestion = 0;
    int score = 0;

    // ป้องกันการกดคำตอบซ้ำ
    bool answered = false;


    // =========================
    // START
    // =========================

    void Start()
    {
        Debug.Log("QUIZ MANAGER START แล้ว!");

        // ซ่อน Stage Complete ตอนเริ่ม
        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(false);
        }

        // เปิด Quiz
        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
        }

        // =========================
        // เชื่อมปุ่มคำตอบ
        // =========================

        answerButton1.onClick.AddListener(() => CheckAnswer(0));
        answerButton2.onClick.AddListener(() => CheckAnswer(1));
        answerButton3.onClick.AddListener(() => CheckAnswer(2));
        answerButton4.onClick.AddListener(() => CheckAnswer(3));


        // =========================
        // เชื่อมปุ่ม Stage Complete
        // =========================

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(GoToNextStage);
        }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RetryQuiz);
        }


        // =========================
        // เริ่มคำถามข้อแรก
        // =========================

        ShowQuestion();
    }


    // =========================
    // แสดงคำถาม
    // =========================

    void ShowQuestion()
    {
        answered = false;

        // เปิดปุ่มคำตอบ
        SetAnswerButtonsInteractable(true);

        // แสดงคำถาม
        questionText.text = questions[currentQuestion];


        // =========================
        // แสดงคำตอบ
        // =========================

        answerButton1.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 0];

        answerButton2.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 1];

        answerButton3.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 2];

        answerButton4.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 3];


        // ล้างข้อความผลลัพธ์
        resultText.text = "";

        Debug.Log(
            "กำลังแสดงข้อที่ " +
            (currentQuestion + 1)
        );
    }


    // =========================
    // ตรวจคำตอบ
    // =========================

    void CheckAnswer(int answer)
    {
        // กันกดซ้ำ
        if (answered)
        {
            return;
        }

        answered = true;

        // ปิดปุ่มคำตอบทันที
        SetAnswerButtonsInteractable(false);


        Debug.Log(
            "ตอบข้อ " +
            (currentQuestion + 1) +
            " หมายเลขคำตอบ: " +
            answer
        );


        // =========================
        // ตอบถูก
        // =========================

        if (answer == correctAnswers[currentQuestion])
        {
            score++;

            resultText.text = "ตอบถูก!";

            Debug.Log(
                "ตอบถูก! คะแนนปัจจุบัน: " +
                score
            );
        }


        // =========================
        // ตอบผิด
        // =========================

        else
        {
            // หาคำตอบที่ถูก
            int correctAnswer =
                correctAnswers[currentQuestion];


            // แปลงเลข 0,1,2,3 เป็น A,B,C,D
            string correctLetter =
                ((char)('A' + correctAnswer)).ToString();


            // ดึงข้อความของคำตอบที่ถูก
            string correctText =
                answers[currentQuestion, correctAnswer];


            // แสดงเฉลย
            resultText.text =
                "ตอบผิด!\n" +
                "เฉลย: " +
                correctLetter +
                ") " +
                correctText;


            Debug.Log(
                "ตอบผิด! " +
                "เฉลยคือ " +
                correctLetter +
                ") " +
                correctText +
                " | คะแนน: " +
                score
            );
        }


        // =========================
        // ไม่ว่าจะถูกหรือผิด
        // ไปข้อถัดไปเหมือนกัน
        // =========================

        StartCoroutine(NextQuestionAfterDelay());
    }


    // =========================
    // ไปข้อถัดไป
    // =========================

    IEnumerator NextQuestionAfterDelay()
    {
        // ให้เวลาอ่านผล/เฉลย
        yield return new WaitForSeconds(1.5f);


        currentQuestion++;


        // =========================
        // ยังมีคำถามเหลือ
        // =========================

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }


        // =========================
        // ทำครบทุกข้อแล้ว
        // =========================

        else
        {
            ShowStageComplete();
        }
    }


    // =========================
    // แสดงหน้า Stage Complete
    // =========================

    void ShowStageComplete()
    {
        Debug.Log(
            "🔥🔥🔥 SHOW STAGE COMPLETE เริ่มทำงาน!"
        );


        // =========================
        // ซ่อน Quiz
        // =========================

        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }


        // =========================
        // เปิด Stage Complete
        // =========================

        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(true);

            // เอา Stage Complete ขึ้นด้านบนสุด
            stageCompletePanel.transform.SetAsLastSibling();

            Debug.Log(
                "🔥 StageCompletePanel เปิดแล้ว!"
            );
        }
        else
        {
            Debug.LogError(
                "❌ stageCompletePanel เป็น NULL!"
            );
        }


        // =========================
        // ชื่อ Stage
        // =========================

        if (stageTitleText != null)
        {
            stageTitleText.text =
                "STAGE 1 COMPLETE!";
        }


        // =========================
        // คะแนน
        // =========================

        if (stageScoreText != null)
        {
            stageScoreText.text =
                "คะแนน " +
                score +
                " / " +
                questions.Length;
        }


        // =========================
        // ผ่าน
        // =========================

        if (score >= 2)
        {
            Debug.Log(
                "🎉 ผ่านด่าน! คะแนน: " +
                score +
                "/" +
                questions.Length
            );


            // แสดงปุ่มไปต่อ
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(true);
            }


            // ซ่อนปุ่มทำใหม่
            if (retryButton != null)
            {
                retryButton.gameObject.SetActive(false);
            }
        }


        // =========================
        // ไม่ผ่าน
        // =========================

        else
        {
            Debug.Log(
                "❌ ไม่ผ่าน! คะแนน: " +
                score +
                "/" +
                questions.Length
            );


            // ซ่อนปุ่มไปต่อ
            if (continueButton != null)
            {
                continueButton.gameObject.SetActive(false);
            }


            // แสดงปุ่มทำใหม่
            if (retryButton != null)
            {
                retryButton.gameObject.SetActive(true);
            }
        }
    }


    // =========================
    // ทำควิซใหม่
    // =========================

    void RetryQuiz()
    {
        Debug.Log(
            "🔄 เริ่มควิซใหม่!"
        );


        // รีเซ็ตคะแนน
        score = 0;


        // กลับไปข้อแรก
        currentQuestion = 0;


        // รีเซ็ตสถานะ
        answered = false;


        // =========================
        // ซ่อน Stage Complete
        // =========================

        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(false);
        }


        // =========================
        // เปิด Quiz
        // =========================

        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
        }


        // =========================
        // แสดงข้อแรก
        // =========================

        ShowQuestion();
    }


    // =========================
    // ไป Stage 2
    // =========================

    void GoToNextStage()
    {
        Debug.Log(
            "🚀 กำลังไป Stage 2..."
        );


        SceneManager.LoadScene(
            nextSceneName
        );
    }


    // =========================
    // เปิด / ปิดปุ่มคำตอบ
    // =========================

    void SetAnswerButtonsInteractable(
        bool value
    )
    {
        answerButton1.interactable = value;
        answerButton2.interactable = value;
        answerButton3.interactable = value;
        answerButton4.interactable = value;
    }
}