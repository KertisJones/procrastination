using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateArrayFromText : MonoBehaviour {

    public TextAsset dataFile;

    public string[][] dataPairs;
    public List<GameObject> listOfQuestionObjects;

    public Object questionPrefab;

    private float questionYValue = 0;
    public float distanceBetweenQuestions = 200;

	// Use this for initialization
	void Start () {
        /*
           Format is: 
           Question 1? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
           Question 2? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
           Question 3? / Correct Answer/ False Answer 1 / False Answer 2 / False Answer 3
         */


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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
