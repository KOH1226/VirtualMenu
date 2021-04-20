using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Leap;
using Leap.Unity;

public class PointerManager : MonoBehaviour
{
    public GameObject providerObj;
    private LeapServiceProvider provider;
    private Finger[] fingers;
    private bool[] isGripFingers;
    private LineRenderer pointer;
    float dY;
    // Start is called before the first frame update
    void Start()
    {
        provider = providerObj.GetComponent<LeapServiceProvider>();
        pointer = this.GetComponent<LineRenderer>();
        pointer.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = provider.CurrentFrame;
        
        if (frame.Hands.Count != 0)
        {
            List<Hand> hands = frame.Hands;
            fingers = hands[0].Fingers.ToArray();
            isGripFingers = Array.ConvertAll(fingers, new Converter<Finger, bool>(i => i.IsExtended));

            if (isGripFingers[1] == true)
            {
                pointer.enabled = true;

                pointer.SetPosition(0, ConvertToUnityVector(fingers[1].TipPosition));
                pointer.SetPosition(1, ConvertToUnityVector(fingers[1].Direction) * 100);

                RaycastHit hitInfo;
                if (Physics.Linecast(ConvertToUnityVector(fingers[1].TipPosition), ConvertToUnityVector(fingers[1].Direction) * 100, out hitInfo))
                {
                    Debug.Log("hit:"+hitInfo);
                }

            }
            else
            {
                pointer.enabled = false;
            }

        }
    }

    public Vector3 ConvertToUnityVector(Vector v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    public Quaternion ConvertQ(LeapQuaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}
