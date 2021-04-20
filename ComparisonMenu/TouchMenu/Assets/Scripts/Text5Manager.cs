using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using Leap;

public class Text5Manager : MonoBehaviour
{
    public Text text;                   //変更テキスト
    public UnityEngine.UI.Image panel;  //変更パネル
    private Color originC;              //パネルカラー保存用
    private Controller controller;      //Leap専用座標取得用
    private bool[] isGripFingers;       //指開閉フラグ
    private Finger[] fingers;           //指情報取得用
    private float fingerDZ;             //人差し指Z方向ベクトル取得用
    private float fingerDX;             //人差し指X方向ベクトル取得用
    private float fingerDZMax;          //人差し指方向の推定MAX値
    private float armDZ;                //腕Z方向ベクトル取得用
    private bool fix;                   //選択項目固定フラグ
    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        fingers = new Finger[5];
        isGripFingers = new bool[5];
        originC = panel.GetComponent<UnityEngine.UI.Image>().color;
        text.text = "N";
        fix = false;    //項目非固定状態
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = controller.Frame();
        if (frame.Hands.Count != 0)
        {
            List<Hand> hands = frame.Hands;
            Arm arm = hands[0].Arm;
            fingers = hands[0].Fingers.ToArray();
            isGripFingers = Array.ConvertAll(fingers, new Converter<Finger, bool>(i => i.IsExtended));
            //親指・人差し指を開いた場合
            if (isGripFingers[0] == true && isGripFingers[1] == true)
            {
                //方向ベクトルデータ取得
                fingerDX = fingers[1].Direction.x;
                armDZ = arm.Direction.z;
                fingerDZMax = armDZ - 0.28f;
                //人差し指方向ベクトルが垂直を超えた場合も反映
                if (fingerDX > 0f)
                {
                    fingerDZ = fingers[1].Direction.z - fingerDX;
                }
                else
                {
                    fingerDZ = fingers[1].Direction.z;
                }

                //方向ベクトルデータ監視用
                //Debug.Log(fingerDZ);
                //Debug.Log("Max:" + fingerDZMax);
                //Debug.Log("arm:" + armDZ);

                //方向選択(身体側に向けるほど項目大)
                if (fix == false)
                {
                    if (fingerDZ < fingerDZMax + 0.134f)
                    {
                        text.text = "1";
                    }
                    else if (fingerDZ < fingerDZMax + 0.268f && fingerDZ >= fingerDZMax + 0.134f)
                    {
                        text.text = "2";
                    }
                    else if (fingerDZ < fingerDZMax + 0.402f && fingerDZ >= fingerDZMax + 0.268f)
                    {
                        text.text = "3";
                    }
                    else if (fingerDZ < fingerDZMax + 0.536f && fingerDZ >= fingerDZMax + 0.402f)
                    {
                        text.text = "4";
                    }
                    else
                    {
                        text.text = "5";
                    }
                }
                originC = panel.GetComponent<UnityEngine.UI.Image>().color;
            }
            //非固定時、親指を閉じた場合
            else if (isGripFingers[0] == false && isGripFingers[1] == true && fix == false )
            {
                //選択固定および色変更
                panel.GetComponent<UnityEngine.UI.Image>().color = new Color(originC.b, originC.g, originC.r, originC.a);
                fix = true;
            }
            //固定時、人差し指を閉じた場合
            else　if(isGripFingers[0] == true && isGripFingers[1] == false && fix == true)
            {
                //選択解除および色変更
                panel.GetComponent<UnityEngine.UI.Image>().color = new Color(originC.b, originC.g, originC.r, originC.a);
                fix = false;
            }
            //親指・人差し指を閉じた場合
            else if (isGripFingers[0] == false && isGripFingers[1] == false)
            {
                //テキスト非表示
                text.text = "N";
                Debug.Log("N");
                //選択非固定状態にして色を保存
                if (fix == true)
                {
                    panel.GetComponent<UnityEngine.UI.Image>().color = new Color(originC.b, originC.g, originC.r, originC.a);
                    fix = false;
                }
            }
        }
    }
}