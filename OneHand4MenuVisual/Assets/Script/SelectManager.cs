using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Timers;
using System.Linq;
using Leap;

public class SelectManager : MonoBehaviour
{
    public bool follow;                 //追従フラグ
    private int itemNum;                //指定項目(参照用)
    private Color originC;              //パネルカラー保存用
    private Controller controller;      //Leap専用座標取得用
    private bool[] isGripFingers;       //指開閉フラグ
    private Finger[] fingers;           //指情報取得用
    private float fingerDZ;             //人差し指Z方向ベクトル取得用
    private float fingerDX;             //人差し指X方向ベクトル取得用
    private float fingerDZMax;          //人差し指方向の推定MAX値
    private float armDZ;                //腕Z方向ベクトル取得用
    private bool fix;                   //選択項目固定フラグ
    private int t = 0;                  //timer回数
    private Timer timer;                //アニメーション時間
    private List<string> chooseItem;    //選択項目格納配列
    private int i = 0;                  //格納用
    private bool back;                  //戻るフラグ
    // Start is called before the first frame update
    void Start()
    {
        chooseItem = new List<string>();
        timer = new Timer(500);
        timer.Elapsed += (sender, e) =>
        {
            if (t < 1)
            {
                fix = false;
                back = false;
                t++;
            }
            else
            {
                timer.Stop();
                t = 0;
            }
        };
        controller = new Controller();
        fingers = new Finger[5];
        isGripFingers = new bool[5];
        fix = false;    //項目非固定状態
        back = false;
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
                    if (fingerDZ < fingerDZMax + 0.1675f)
                    {
                        itemNum = 1;
                    }
                    else if (fingerDZ < fingerDZMax + 0.335f && fingerDZ >= fingerDZMax + 0.1675f)
                    {
                        itemNum = 2;
                    }
                    else if (fingerDZ < fingerDZMax + 0.5025f && fingerDZ >= fingerDZMax + 0.335f)
                    {
                        itemNum = 3;
                    }
                    else
                    {
                        itemNum = 4;
                    }
                }
            }
            //親指を閉じた場合
            else if (isGripFingers[0] == false && isGripFingers[1] == true && fix == false)
            {
                //選択項目を格納・アニメーション
                chooseItem.Add(itemNum.ToString());
                i++;
                Debug.Log(string.Join(",", chooseItem));
                fix = true;
                timer.Start();
            }
            //人差し指を閉じた場合
            else if (isGripFingers[0] == true && isGripFingers[1] == false && back == false)
            {
                //選択解除および色変更
                if (i == 0)
                {

                }
                else
                {
                    i--;
                    chooseItem.RemoveAt(i);
                    Debug.Log("Back");
                    Debug.Log(string.Join(",", chooseItem));
                    back = true;
                    timer.Start();
                }
            }
            //親指・人差し指を閉じた場合
            else if (isGripFingers[0] == false && isGripFingers[1] == false)
            {
                //選択非固定状態にして色を保存
                if (fix == true)
                {
                    fix = false;
                }
            }
        }
    }

    public int GetItemNum()
    {
        return itemNum;
    }

    public bool GetFix()
    {
        return fix;
    }

    public bool GetBack()
    {
        return back;
    }

    public int GetBackItem()
    {
        return int.Parse(chooseItem[i - 1]);
    }

    public int GetI()
    {
        return i;
    }
}
