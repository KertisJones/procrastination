using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstGameObjFromListOfImg : MonoBehaviour
{
    public List<Sprite> listOfImages;
    //private List<GameObject> listOfObjects;

    public Object spritePrefab;

    public GameObject displayCamera;


    private float objYValue = 0;
    public float distanceBetweenQuestions = 200;

    // Use this for initialization
    void Start()
    {
        objYValue = this.transform.position.y;

        foreach (Sprite d in listOfImages)
        {
            GameObject q = Instantiate(spritePrefab, new Vector3(this.transform.position.x, objYValue, 0), new Quaternion(0, 0, 0, 0), this.gameObject.transform) as GameObject;
            q.GetComponent<SpriteRenderer>().sprite = d;
            //listOfObjects.Add(q);
            objYValue -= distanceBetweenQuestions;
        }

        displayCamera.GetComponent<moveWithScrollWheel>().yMin = objYValue + distanceBetweenQuestions;

    }
    

    // Update is called once per frame
    void Update()
    {

    }
}
