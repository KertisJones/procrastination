using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateArrayFromText : MonoBehaviour {

    public TextAsset dataFile;

    public string[][] dataPairs;
    public List<GameObject> listOfQuestionObjects;

    public Object questionPrefab;
    public GameObject submitExamObj;

    public GameObject mainCamera;

    public Text resultsText;

    private float questionYValue = 0;
    public float distanceBetweenQuestions = 200;

    public int correctAnswers = 0;
    public int incorrectAnswers = 0;
    public float examScore = 0;

	// Use this for initialization
	void Start () {
        /*
           Format is: 
           Question 1? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
           Question 2? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
           Question 3? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
         */
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");


        string[] dataLines = dataFile.text.Split('\n');
        dataPairs = new string[dataLines.Length][];
        int lineNum = 0;
        foreach (string line in dataLines)
        {
            dataPairs[lineNum++] = line.Split('/');
        }
        //Debug.Log(dataPairs[0][0]);
        //Debug.Log(dataPairs[1][1]);

        questionYValue = this.transform.position.y;

        foreach (string[] d in dataPairs)
        {
            GameObject q = Instantiate(questionPrefab, new Vector3(0, questionYValue, 0), new Quaternion (0,0,0,0), this.gameObject.transform) as GameObject;
            q.GetComponent<ExamMultipleChoiceQuestion>().questionList = d;
            listOfQuestionObjects.Add(q);
            questionYValue -= distanceBetweenQuestions;
        }

        submitExamObj.transform.position = new Vector3(submitExamObj.transform.position.x, questionYValue, submitExamObj.transform.position.z);
        submitExamObj.GetComponentInChildren<Button>().onClick.AddListener(SubmitExam);


        mainCamera.GetComponent<moveWithScrollWheel>().yMin = questionYValue;

    }

    void SubmitExam ()
    {
        correctAnswers = 0;
        incorrectAnswers = 0;
        //Debug.Log("SUBMIT EXAM");
        foreach (GameObject q in listOfQuestionObjects)
        {
            if (q.GetComponent<ExamMultipleChoiceQuestion>().correctAnswer)
            {
                correctAnswers += 1;
            }
            else
            {
                incorrectAnswers += 1;
            }
        }
        int totalAnswers = correctAnswers + incorrectAnswers;
        examScore = ((float)correctAnswers / (float)totalAnswers) * 100; //get percentage
        examScore = Mathf.Round(examScore * 100) / 100; //round to 2 decimal places

        string letterGrade = "";

        if (examScore >= 100)
        {
            letterGrade = "A+";
        }
        else if (examScore >= 93)
        {
            letterGrade = "A";
        }
        else if (examScore >= 90)
        {
            letterGrade = "A-";
        }
        else if (examScore >= 87)
        {
            letterGrade = "B+";
        }
        else if (examScore >= 83)
        {
            letterGrade = "B";
        }
        else if (examScore >= 80)
        {
            letterGrade = "B-";
        }
        else if (examScore >= 77)
        {
            letterGrade = "C+";
        }
        else if (examScore >= 73)
        {
            letterGrade = "C";
        }
        else if (examScore >= 70)
        {
            letterGrade = "C-";
        }
        else if (examScore >= 67)
        {
            letterGrade = "D+";
        }
        else if (examScore >= 63)
        {
            letterGrade = "D";
        }
        else if (examScore >= 60)
        {
            letterGrade = "D-";
        }
        else
        {
            letterGrade = "F";
        }


        mainCamera.transform.position = new Vector3(1000, 0, -10);
        mainCamera.GetComponent<moveWithScrollWheel>().yMax = 0;
        mainCamera.GetComponent<moveWithScrollWheel>().yMin = 0;

        resultsText.text = letterGrade +
            "\n\n" + "Score: " + examScore.ToString() +
            "\n\n" + "Correct: " + correctAnswers.ToString() +
            "\n" + "Incorrect: " + incorrectAnswers.ToString();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
