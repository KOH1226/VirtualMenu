using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Text;
using System.Timers;

public class PanelManager : MonoBehaviour
{
    public GameObject smGameObject;
    public Text PanelText;
    private SelectManager sm = new SelectManager();
    private string s;
    private int i = 0;
    private int[] preExperimentNum1;
    private int[] preExperimentNum2;
    private int[] preExperimentNum3;
    private Timer timer;                //アニメーション時間
    private int t = 0;                  //timer回数
    private bool time;                  //計測中
    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();    //計測時間測定
    // Start is called before the first frame update
    void Start()
    {
        sm = smGameObject.GetComponent<SelectManager>();
        preExperimentNum1 = new int[15] { 2, 3, 1, 4, 2, 4, 1, 3, 2, 1, 4, 3, 4, 2, 3 };
        preExperimentNum2 = new int[15] { 1, 4, 1, 3, 2, 1, 3, 2, 4, 3, 1, 4, 2, 3, 2 };
        preExperimentNum3 = new int[15] { 4, 1, 2, 3, 1, 4, 2, 3, 1, 4, 3, 4, 2, 1, 3 };
        TextSwitch(preExperimentNum3[i]);
        time = false;

        timer = new Timer(200);
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
        if (time == false)
        {
            if (i > 14)
            {
                PanelText.text = "終了";
            }
            else if (sm.GetFix() == true)
            {
                time = true;
                timer.Start();
                i++;
                TextSwitch(preExperimentNum3[i]);
                Debug.Log("pre:" + i);
            }
            /*
            else if (sm.GetBack() == true)
            {
                time = true;
                timer.Start();
                i--;
                Debug.Log("pre:" + i);
                TextSwitch(preExperimentNum3[i]);
            }
            */
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("計測開始");
            sw.Start();
        }

        if (i == 15)
        {
            sw.Stop();
            TimeSpan ts = sw.Elapsed;

            string dt = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");

            //ファイル書き込み
            StreamWriter writer = new StreamWriter(@"C:\Users\KH\Desktop\予備実験用\4menu\" + dt + ".txt", false);
            writer.WriteLine("回答：" + sm.GetChooseItem());
            writer.WriteLine("見本：" + string.Join(",", preExperimentNum3));
            writer.WriteLine($"　{ts.Hours}時間 {ts.Minutes}分 {ts.Seconds}秒 {ts.Milliseconds}ミリ秒");
            writer.WriteLine($"　{sw.ElapsedMilliseconds}ミリ秒");
            writer.Close();
            Debug.Log("書き込み完了");
            i++;
        }

    }

    private void TextSwitch(int num)
    {
        switch (num)
        {
            case 1:
                PanelText.text = "1";
                break;
            case 2:
                PanelText.text = "2";
                break;
            case 3:
                PanelText.text = "3";
                break;
            case 4:
                PanelText.text = "4";
                break;
        }
    }
}
