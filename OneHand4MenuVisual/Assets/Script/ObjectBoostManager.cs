using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class ObjectBoostManager : MonoBehaviour
{
    List<GameObject> childList = new List<GameObject>();
    List<Rigidbody> childRigidbodies = new List<Rigidbody>();

    private ObjectBooster ob0 = new ObjectBooster();
    private ObjectBooster ob1 = new ObjectBooster();
    private ObjectBooster ob2 = new ObjectBooster();
    private ObjectBooster ob3 = new ObjectBooster();
    //public GameObject OOMGameObject1;
    private int[] ExperimentNum1;
    private int[] ExperimentNum2;
    private int en = 0;

    private Timer timer;                
    private int t = 0;                  //timer回数
    private bool time;                  //計測中
    // Start is called before the first frame update
    void Start()
    {

        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
        }

        ob0 = childList[0].GetComponent<ObjectBooster>();
        ob1 = childList[1].GetComponent<ObjectBooster>();
        ob2 = childList[2].GetComponent<ObjectBooster>();
        ob3 = childList[3].GetComponent<ObjectBooster>();

        ExperimentNum1 = new int[20] { 2, 3, 1, 4, 0, 4, 1, 3, 2, 1, 2, 3, 1, 4, 0, 4, 1, 3, 2, 1};

        time = false;
        timer = new Timer(500);
        timer.Elapsed += (sender, e) =>
        {
            if (t < 1)
            {
                t++;
            }
            else
            {
                time = false;
                timer.Stop();
                t = 0;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if(time == false)
        {
            if (en > 29)
            {

            }
            else if(en  < 10)
            {
                time = true;
                timer.Start();
                en++;
            }
            else
            {
                time = true;
                timer.Start();
                
                ObjectSwitch(ExperimentNum1[en]);
                en++;

                Debug.Log("throw:" + en);
            }
        }
    }

    void ObjectSwitch(int num)
    {
        switch (num)
        {
            case 0:
                ob0.ObjectImpulse();
                break;
            case 1:
                ob1.ObjectImpulse();
                break;
            case 2:
                ob2.ObjectImpulse();
                break;
            case 3:
                ob3.ObjectImpulse();
                break;
        }
    }
}
