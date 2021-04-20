using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Leap.Unity;
using System;
using System.Linq;

public class MenuManager : MonoBehaviour
{
    public RectTransform fanRect;
    public GameObject m_ProviderObject;
    private CanvasGroup thisCanvasG;
    private LeapServiceProvider m_Provider;
    private Finger[] fingers;
    private bool[] isGripFingers;
    // Start is called before the first frame update
    void Start()
    {
        fanRect = GetComponent<RectTransform>();
        m_Provider = m_ProviderObject.GetComponent<LeapServiceProvider>();
        thisCanvasG = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = m_Provider.CurrentFrame;

        if (frame.Hands.Count != 0)
        {
            List<Hand> hands = frame.Hands;
            Arm arm = hands[0].Arm;
            fingers = hands[0].Fingers.ToArray();
            isGripFingers = Array.ConvertAll(fingers, new Converter<Finger, bool>(i => i.IsExtended));
            //UI追従・カメラを向く
            //fanRect.position = ConvertToUnityVector3(hands[0].PalmPosition);
            fanRect.position = ConvertToUnityVector3(arm.WristPosition);
            fanRect.position += new Vector3(0.12f, 0.2f, 0.1f);
            fanRect.LookAt(Camera.main.transform);



            if (isGripFingers[0] == true && isGripFingers[1] == true)
            {
                //UI表示
                thisCanvasG.alpha = 1;
                //Debug.Log("手のひら："+hands[0].PalmNormal);
                //Debug.Log("人差し指"+fingers[1].Direction);
            }
            else if (isGripFingers[0] == false && isGripFingers[1] == true)
            {
                //Debug.Log("Sumb down.");
                //決定
            }
            else if (isGripFingers[0] == true && isGripFingers[1] == false)
            {
                //Debug.Log("Index down.");
                //戻る
            }
            else if (isGripFingers[0] == false && isGripFingers[1] == false)
            {
                //UI非表示
                thisCanvasG.alpha = 0;
            }
        }
    }

    private Vector3 ConvertToUnityVector3(Vector v)
    {
        // Leap.VectorからUnityのVector3に変換
        return new Vector3(v.x, v.y, v.z);
    }

}
