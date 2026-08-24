using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public TMP_Text questionText;
    public TMP_Text resultText;

    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public Button answerButton4;

    // คำถาม
    string[] questions =
    {
        "สารที่มีอยู่ก่อนเกิดปฏิกิริยาเคมีเรียกว่าอะไร?",
        "สารที่เกิดขึ้นหลังปฏิกิริยาเคมีเรียกว่าอะไร?",
        "ข้อใดเป็นสัญญาณที่อาจบ่งบอกว่าเกิดปฏิกิริยาเคมี?",
        "สมการใดดุลถูกต้อง?"
    };

    // คำตอบ A B C D ของแต่ละข้อ
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

    // คำตอบที่ถูกของแต่ละข้อ
    // 0 = A
    // 1 = B
    // 2 = C
    // 3 = D
    int[] correctAnswers = { 1, 2, 0, 1 };

    int currentQuestion = 0;
    int score = 0;

    void Start()
    {
        Debug.Log("QUIZ MANAGER START แล้ว!");

        answerButton1.onClick.AddListener(() => CheckAnswer(0));
        answerButton2.onClick.AddListener(() => CheckAnswer(1));
        answerButton3.onClick.AddListener(() => CheckAnswer(2));
        answerButton4.onClick.AddListener(() => CheckAnswer(3));

        ShowQuestion();
    }

    void ShowQuestion()
    {
        // แสดงคำถาม
        questionText.text = questions[currentQuestion];

        // แสดงคำตอบ
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

        Debug.Log("กำลังแสดงข้อที่ " + (currentQuestion + 1));
    }

   
    void CheckAnswer(int answer)
    {
        Debug.Log(
            "ตอบข้อ " + (currentQuestion + 1) +
            " หมายเลขคำตอบ: " + answer
        );

        if (answer == correctAnswers[currentQuestion])
        {
            score++;

            resultText.text = "ตอบถูก!";
            Debug.Log("ตอบถูก! คะแนนปัจจุบัน: " + score);

            StartCoroutine(NextQuestionAfterDelay());
        }
        else
        {
            resultText.text = "ตอบผิด!";
            Debug.Log("ตอบผิด! คะแนนปัจจุบัน: " + score);
        }
    }

    IEnumerator NextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            ShowQuestion();
        }
        else
        {
            if (score >= 2)
            {
                resultText.text = "ผ่านด่าน! คะแนน: " + score + "/4";
                Debug.Log("ผ่านด่าน! คะแนน: " + score);
            }
            else
            {
                resultText.text = "ไม่ผ่าน! คะแนน: " + score + "/4";
                Debug.Log("ไม่ผ่าน! ต้องทำควิซใหม่");
            }

            yield return new WaitForSeconds(2f);

            gameObject.SetActive(false);

            if (score < 2)
            {
                currentQuestion = 0;
                score = 0;
            }
        }   }
    }