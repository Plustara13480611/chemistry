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

    void Start()
    {
        Debug.Log("QUIZ MANAGER START แล้ว!");

        questionText.text = "คำถามทดสอบ";

        answerButton1.GetComponentInChildren<TMP_Text>().text = "คำตอบ A";
        answerButton2.GetComponentInChildren<TMP_Text>().text = "คำตอบ B";
        answerButton3.GetComponentInChildren<TMP_Text>().text = "คำตอบ C";
        answerButton4.GetComponentInChildren<TMP_Text>().text = "คำตอบ D";

        answerButton1.onClick.AddListener(() => CheckAnswer(0));
        answerButton2.onClick.AddListener(() => CheckAnswer(1));
        answerButton3.onClick.AddListener(() => CheckAnswer(2));
        answerButton4.onClick.AddListener(() => CheckAnswer(3));
    }

    void CheckAnswer(int answer)
    {
        Debug.Log("กดคำตอบแล้ว! หมายเลข: " + answer);

        if (answer == 1)
        {
            resultText.text = "ตอบถูก!";
            Debug.Log("ตอบถูก!");

            StartCoroutine(CloseQuizAfterDelay());
        }
        else
        {
            resultText.text = "ตอบผิด!";
            Debug.Log("ตอบผิด!");
        }
    }

    IEnumerator CloseQuizAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
    }
}