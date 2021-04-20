using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;

public class CircleManager : MonoBehaviour
{
    List<GameObject> childList = new List<GameObject>();
    List<Image> childImageList = new List<Image>();
    public GameObject smGameObject;
    public GameObject fanGameObject;
    private SelectManager sm = new SelectManager();
    private FanDeployer fd = new FanDeployer();
    private Color originC;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        fanGameObject = this.gameObject;
        animator = GetComponent<Animator>();
        sm = smGameObject.GetComponent<SelectManager>();
        fd = fanGameObject.GetComponent<FanDeployer>();
        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
            childImageList.Add(child.GetComponent<Image>());
        }
        originC = childImageList[0].color;
        ChildAlphaChange(originC.a - fd._radius / 0.2f * originC.a);
    }

    // Update is called once per frame
    void Update()
    {
        ChildAlphaChange(originC.a - fd._radius / 0.2f * originC.a);
        /*
        if (sm.GetFix() == true)
        {
            fd.Deploy();

            if (fd._radius > 0.1f)
            {
                ColorSwitch();
                animator.SetBool("Selected0.2", true);
            }
            else
            {
                animator.SetBool("Selected0.1", true);
            }
        }
        else
        {
            if (animator.GetBool("Selected0.2") == true)
            {
                fd._radius = 0.1f;
                fd.Deploy();
                animator.SetBool("Selected0.2", false);
            }else if (animator.GetBool("Selected0.1") == true)
            {
                fd._radius = 0.2f;
                fd.Deploy();
                animator.SetBool("Selected0.1", false);
                ColorSwitch();
            }
        }
        */

        if (sm.GetFix() == true)
        {
            ColorSwitch();
            ChildAlphaChange(originC.a - fd._radius / 0.2f * originC.a);
        }
        else if (sm.GetBack() == true)
        {
            ColorChange(sm.GetBackItem()-1);
            ChildAlphaChange(originC.a - fd._radius / 0.2f * originC.a);
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

    private void ColorSwitch()
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
    }

    private void ChildAlphaChange(float a)
    {
        int i = 0;
        //透明度変更
        foreach (Image image in childImageList)
        {
           
            childImageList[i].color = new Color(childImageList[i].color.r, childImageList[i].color.g, childImageList[i].color.b, a);
            i++;
        }
    }
}
