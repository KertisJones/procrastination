using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamMultipleChoiceQuestion : MonoBehaviour {

    public string[] questionList;
    public List<GameObject> possibleAnswers;

    public Text questionDisplay;
    public GameObject correctAnswerDisplay;
    public GameObject falseAnswerDisplay;
    public GameObject falseAnswerDisplay2;
    public GameObject falseAnswerDisplay3;

    public bool correctAnswer = false;

    public ColorBlock cb;


    // Use this for initialization
    void Start()
    {
        /*for (int i = 0; i < alpha.Count; i++) {
         string temp = alpha[i];
         int randomIndex = Random.Range(i, alpha.Count);
         alpha[i] = alpha[randomIndex];
         alpha[randomIndex] = temp;
        }*/
        /*
        correctAnswerDisplay = GameObject.Find("/CorrectButtonAnswer");
        falseAnswerDisplay = GameObject.Find("/FalseButtonAnswer (1)");
        falseAnswerDisplay2 = GameObject.Find("/FalseButtonAnswer (2)");
        falseAnswerDisplay3 = GameObject.Find("/FalseButtonAnswer (3)");
        */
        for (int i = 2; i < questionList.Length; i++)
        {

        }

        if (questionList.Length > 0)
        {
            questionDisplay.text = questionList[0];
        }
        if (questionList.Length > 1)
        {
            correctAnswerDisplay.GetComponentInChildren<Text>().text = questionList[1];
            correctAnswerDisplay.GetComponentInChildren<Button>().onClick.AddListener(delegate { CorrectClick(correctAnswerDisplay); });
            possibleAnswers.Add(correctAnswerDisplay);
        }
        if (questionList.Length > 2)
        {
            falseAnswerDisplay.GetComponentInChildren<Text>().text = questionList[2];
            falseAnswerDisplay.GetComponentInChildren<Button>().onClick.AddListener(delegate { IncorrectClick(falseAnswerDisplay); });
            possibleAnswers.Add(falseAnswerDisplay);
        }
        else
        {
            Destroy(falseAnswerDisplay);
            //falseAnswerDisplay.SetActive(false);
        }
        if (questionList.Length > 3)
        {
            falseAnswerDisplay2.GetComponentInChildren<Text>().text = questionList[3];
            falseAnswerDisplay2.GetComponentInChildren<Button>().onClick.AddListener(delegate { IncorrectClick(falseAnswerDisplay2); });
            possibleAnswers.Add(falseAnswerDisplay2);
        }
        else
        {
            Destroy(falseAnswerDisplay2);
            //falseAnswerDisplay2.SetActive(false);
        }
        if (questionList.Length > 4)
        {
            falseAnswerDisplay3.GetComponentInChildren<Text>().text = questionList[4];
            falseAnswerDisplay3.GetComponentInChildren<Button>().onClick.AddListener(delegate { IncorrectClick(falseAnswerDisplay3); });
            possibleAnswers.Add(falseAnswerDisplay3);
        }
        else
        {
            Destroy(falseAnswerDisplay3);
            //falseAnswerDisplay3.SetActive(false);
        }

        for (int n = 0; n < 10; n++)
        {
            for (int i = 0; i < possibleAnswers.Count; i++)
            {
                GameObject temp = possibleAnswers[i];
                int randomIndex = Random.Range(i, possibleAnswers.Count);
                possibleAnswers[i] = possibleAnswers[randomIndex];
                possibleAnswers[randomIndex] = temp;

            }
        }
        float questionY = this.transform.position.y - 70;
        for (int i = 0; i < possibleAnswers.Count; i++)
        {
            possibleAnswers[i].transform.position = new Vector3(possibleAnswers[i].transform.position.x, questionY, possibleAnswers[i].transform.position.z);

            questionY -= 55;
        }

    }

    void CorrectClick(GameObject buttonClicked)
    {
        correctAnswer = true;


        if (correctAnswerDisplay != null)
        {
            cb = correctAnswerDisplay.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(1, 1, 1, 1.0f);
            correctAnswerDisplay.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay != null)
        {
            cb = falseAnswerDisplay.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay2 != null)
        {
            cb = falseAnswerDisplay2.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay2.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay3 != null)
        {
            cb = falseAnswerDisplay3.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay3.GetComponentInChildren<Button>().colors = cb;
        }
    }

    void IncorrectClick(GameObject buttonClicked)
    {
        correctAnswer = false;

        if (correctAnswerDisplay != null)
        {
            cb = correctAnswerDisplay.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            correctAnswerDisplay.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay != null)
        {
            cb = falseAnswerDisplay.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay2 != null)
        {
            cb = falseAnswerDisplay2.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay2.GetComponentInChildren<Button>().colors = cb;
        }
        if (falseAnswerDisplay3 != null)
        {
            cb = falseAnswerDisplay3.GetComponentInChildren<Button>().colors;
            cb.normalColor = new Color(152f/255f, 145f/255f, 145f/255f, 1.0f);
            falseAnswerDisplay3.GetComponentInChildren<Button>().colors = cb;
        }

        cb = buttonClicked.GetComponentInChildren<Button>().colors;
        cb.normalColor = new Color(1, 1, 1, 1.0f);
        buttonClicked.GetComponentInChildren<Button>().colors = cb;
    }

    // Update is called once per frame
    void Update () {

    }
}
