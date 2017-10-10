using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExamMultipleChoiceQuestion : MonoBehaviour {

    public string[] questionList;

    public Text questionDisplay;
    public Text correctAnswerDisplay;
    public Text falseAnswerDisplay;
    public Text falseAnswerDisplay2;
    public Text falseAnswerDisplay3;

    // Use this for initialization
    void Start () {
        /*for (int i = 0; i < alpha.Count; i++) {
         string temp = alpha[i];
         int randomIndex = Random.Range(i, alpha.Count);
         alpha[i] = alpha[randomIndex];
         alpha[randomIndex] = temp;
        }*/



	}
	
	// Update is called once per frame
	void Update () {
        if (questionList.Length > 0)
        {
            questionDisplay.text = questionList[0];
            correctAnswerDisplay.text = questionList[1];
            falseAnswerDisplay.text = questionList[2];
            falseAnswerDisplay2.text = questionList[3];
            falseAnswerDisplay3.text = questionList[4];
        }
    }
}
