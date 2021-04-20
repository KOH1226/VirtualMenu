using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemManager : MonoBehaviour
{
    List<GameObject> childList = new List<GameObject>();
    List<Image> childImageList = new List<Image>();
    List<List<Text>> UITexts = new List<List<Text>>();
    public List<Text> originText = new List<Text>();
    public GameObject smGameObject;
    private SelectManager sm = new SelectManager();
    private Color originC;
    private List<string> chooseItem = new List<string>();
    private string[] text1 = new string[4] { "①", "②", "③", "④" };
    private string[] text2 = new string[4] { "①", "②", "③", "④" };
    private string[] text3 = new string[4] { "①", "②", "③", "④" };
    private string[] text4 = new string[4] { "①", "②", "③", "④" };
    private string[] text5 = new string[4] { "①", "②", "③", "④" };
    // Start is called before the first frame update
    void Start()
    {
        sm = smGameObject.GetComponent<SelectManager>();

        foreach(Transform child in transform)
        {
            childList.Add(child.gameObject);
            childImageList.Add(child.Find("Image").GetComponent<Image>());
            originText.Add(child.Find("Image/Text").GetComponent<Text>());
        }
        //UITexts.Add(originText);
        originC = childImageList[0].color;
        Debug.Log(originText);
    }

    // Update is called once per frame
    void Update()
    {

        switch (sm.GetItemNum())
        {
            case 1:
                ColorChange(0);
                break;
            case 2:
                ColorChange(1);
                break;
            case 3:
                ColorChange(2);
                break;
            case 4:
                ColorChange(3);
                break;
        }

        if (sm.GetFix() == true)
        {
            TextSwitch(sm.GetItemNum());
            //chooseItem.Add(originText[sm.GetItemNum()-1].text);
            //Debug.Log(string.Join(",", chooseItem));
        }else if (sm.GetBack() == true)
        {
            TextSwitch(sm.GetBackItem());
            //chooseItem.RemoveAt(sm.GetI());
            //Debug.Log(string.Join(",", chooseItem));
        }
    }

    private void ColorChange(int itemNum)
    {
        int i = 0;
        //色初期化
        foreach (Image image in childImageList)
        {
            childImageList[i].color = originC;
            i++;
        }
        //指定項目色変更
        childImageList[itemNum].color = new Color(originC.b, originC.g, originC.r, originC.a);
    }

    private void TextChange(string[] s)
    {
        for(int i = 0; i < 4; i++)
        {
            originText[i].text = s[i];
        }
    }

    private void TextSwitch(int num)
    {
        switch (num)
        {
            case 1:
                TextChange(text1);
                break;
            case 2:
                TextChange(text2);
                break;
            case 3:
                TextChange(text3);
                break;
            case 4:
                TextChange(text4);
                break;
        }
    }
}
