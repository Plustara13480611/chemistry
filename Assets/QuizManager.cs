
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    // =========================
    // คำถามและผลลัพธ์
    // =========================

    public TMP_Text questionText;
    public TMP_Text resultText;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;


    // =========================
    // UI
    // =========================

    public GameObject quizPanel;
    public GameObject stageCompletePanel;
    public GameObject demoEndPanel;

    public TMP_Text stageTitleText;
    public TMP_Text stageScoreText;

    public Button continueButton;
    public Button retryButton;

    // ปุ่มกลับ Main Menu ในหน้า Demo End
    public Button returnToMainMenuButton;


    // =========================
    // เลือก Stage
    // =========================

    // Scene 1 = false
    // Scene 2 = true
    public bool isStage2 = false;

    // Scene ถัดไปของ Stage 1
    public string nextSceneName = "scene2";


    // =========================
    // คำถาม Stage 1
    // =========================

    string[] questions =
    {
        "สารที่มีอยู่ก่อนเกิดปฏิกิริยาเคมีเรียกว่าอะไร?",
        "สารที่เกิดขึ้นหลังปฏิกิริยาเคมีเรียกว่าอะไร?",
        "ข้อใดเป็นสัญญาณที่อาจบ่งบอกว่าเกิดปฏิกิริยาเคมี?",
        "สมการใดดุลถูกต้อง?"
    };


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


    // A = 0
    // B = 1
    // C = 2
    // D = 3

    int[] correctAnswers = { 1, 2, 0, 1 };


    // =========================
    // คำถาม Stage 2
    // สารละลายและกรด-เบส
    // =========================

    string[] stage2Questions =
    {
        "ในน้ำเกลือ น้ำทำหน้าที่เป็นอะไร?",
        "สารที่มีค่า pH น้อยกว่า 7 โดยทั่วไปจัดเป็นสารประเภทใด?",
        "สารที่มีค่า pH เท่ากับ 7 โดยทั่วไปมีสมบัติอย่างไร?",
        "ข้อใดเป็นตัวอย่างของสารที่มีสมบัติเป็นกรด?"
    };


    string[,] stage2Answers =
    {
        {
            "ตัวถูกละลาย",
            "ตัวทำละลาย",
            "ตัวเร่งปฏิกิริยา",
            "ผลิตภัณฑ์"
        },

        {
            "กรด",
            "เบส",
            "กลาง",
            "เกลือเท่านั้น"
        },

        {
            "เป็นกรดแก่",
            "เป็นเบสแก่",
            "เป็นกลาง",
            "เป็นโลหะ"
        },

        {
            "น้ำสบู่",
            "น้ำมะนาว",
            "น้ำปูนใส",
            "สารละลาย NaOH"
        }
    };


    // ข้อ 1 = B
    // ข้อ 2 = A
    // ข้อ 3 = C
    // ข้อ 4 = B

    int[] stage2CorrectAnswers = { 1, 0, 2, 1 };


    // =========================
    // ตัวแปรระบบ
    // =========================

    int currentQuestion = 0;
    int score = 0;

    bool answered = false;


    // =========================
    // START
    // =========================

    void Start()
    {
        Debug.Log("QUIZ MANAGER START แล้ว!");

        // ถ้าเป็น Stage 2
        // เปลี่ยนไปใช้ชุดคำถาม Stage 2
        if (isStage2)
        {
            questions = stage2Questions;
            answers = stage2Answers;
            correctAnswers = stage2CorrectAnswers;
        }


        // ซ่อน Stage Complete
        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(false);
        }


        // ซ่อน Demo End
        if (demoEndPanel != null)
        {
            demoEndPanel.SetActive(false);
        }


        // เชื่อมปุ่มคำตอบ
        if (answerButton1 != null)
        {
            answerButton1.onClick.AddListener(() => CheckAnswer(0));
        }

        if (answerButton2 != null)
        {
            answerButton2.onClick.AddListener(() => CheckAnswer(1));
        }

        if (answerButton3 != null)
        {
            answerButton3.onClick.AddListener(() => CheckAnswer(2));
        }

        if (answerButton4 != null)
        {
            answerButton4.onClick.AddListener(() => CheckAnswer(3));
        }


        // เชื่อมปุ่ม Stage Complete
        if (continueButton != null)
        {
            continueButton.onClick.AddListener(GoToNextStage);
        }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RetryQuiz);
        }


        // เชื่อมปุ่มกลับ Main Menu
        if (returnToMainMenuButton != null)
        {
            returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }


        // เตรียมคำถามข้อแรก
        ShowQuestion();
    }


    // =========================
    // แสดงคำถาม
    // =========================

    void ShowQuestion()
    {
        answered = false;

        SetAnswerButtonsInteractable(true);

        questionText.text = questions[currentQuestion];

        answerButton1.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 0];

        answerButton2.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 1];

        answerButton3.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 2];

        answerButton4.GetComponentInChildren<TMP_Text>().text =
            answers[currentQuestion, 3];

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
        if (answered)
        {
            return;
        }

        answered = true;

        SetAnswerButtonsInteractable(false);

        Debug.Log(
            "ตอบข้อ " +
            (currentQuestion + 1) +
            " หมายเลขคำตอบ: " +
            answer
        );


        // ตอบถูก
        if (answer == correctAnswers[currentQuestion])
        {
            score++;

            resultText.text = "ตอบถูก!";

            Debug.Log(
                "ตอบถูก! คะแนนปัจจุบัน: " +
                score
            );
        }


        // ตอบผิด
        else
        {
            int correctAnswer =
                correctAnswers[currentQuestion];

            string correctLetter =
                ((char)('A' + correctAnswer)).ToString();

            string correctText =
                answers[currentQuestion, correctAnswer];

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

        StartCoroutine(NextQuestionAfterDelay());
    }


    // =========================
    // ไปข้อถัดไป
    // =========================

    IEnumerator NextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
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
            "SHOW STAGE COMPLETE เริ่มทำงาน!"
        );


        // ซ่อน Quiz
        if (quizPanel != null)
        {
            quizPanel.SetActive(false);
        }


        // เปิด Stage Complete
        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(true);

            stageCompletePanel.transform.SetAsLastSibling();

            Debug.Log(
                "StageCompletePanel เปิดแล้ว!"
            );
        }
        else
        {
            Debug.LogError(
                "stageCompletePanel เป็น NULL!"
            );
        }


        // ชื่อ Stage
        if (stageTitleText != null)
        {
            if (isStage2)
            {
                stageTitleText.text =
                    "STAGE 2 COMPLETE!";
            }
            else
            {
                stageTitleText.text =
                    "STAGE 1 COMPLETE!";
            }
        }


        // คะแนน
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
                "ผ่านด่าน! คะแนน: " +
                score +
                "/" +
                questions.Length
            );


            // เปิดปุ่มไปต่อ
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
                "ไม่ผ่าน! คะแนน: " +
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
    // ทำ Quiz ใหม่
    // =========================

    void RetryQuiz()
    {
        Debug.Log(
            "เริ่มควิซใหม่!"
        );

        score = 0;

        currentQuestion = 0;

        answered = false;


        // ซ่อน Stage Complete
        if (stageCompletePanel != null)
        {
            stageCompletePanel.SetActive(false);
        }


        // เปิด Quiz
        if (quizPanel != null)
        {
            quizPanel.SetActive(true);
        }


        // แสดงข้อแรก
        ShowQuestion();
    }


    // =========================
    // ไป Stage ถัดไป / Demo End
    // =========================

    void GoToNextStage()
    {
        // =========================
        // ถ้าเป็น Stage 2
        // ให้เปิด Demo End
        // =========================

        if (isStage2)
        {
            Debug.Log("DEMO END");

            // ปิด Quiz
            if (quizPanel != null)
            {
                quizPanel.SetActive(false);
            }

            // ปิด Stage Complete
            if (stageCompletePanel != null)
            {
                stageCompletePanel.SetActive(false);
            }

            // เปิด Demo End
            if (demoEndPanel != null)
            {
                demoEndPanel.SetActive(true);

                demoEndPanel.transform.SetAsLastSibling();

                Debug.Log("DemoEndPanel เปิดแล้ว!");
            }
            else
            {
                Debug.LogError(
                    "demoEndPanel เป็น NULL! " +
                    "อย่าลืมลาก DemoEndPanel ใส่ช่อง Demo End Panel ใน Inspector"
                );
            }

            return;
        }


        // =========================
        // ถ้าเป็น Stage 1
        // ไป Scene 2
        // =========================

        Debug.Log(
            "กำลังไป Stage 2..."
        );

        SceneManager.LoadScene(
            nextSceneName
        );
    }


    // =========================
    // กลับ Main Menu
    // =========================

    public void ReturnToMainMenu()
    {
        Debug.Log("กำลังกลับ Main Menu...");

        SceneManager.LoadScene("mainmenu");
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

