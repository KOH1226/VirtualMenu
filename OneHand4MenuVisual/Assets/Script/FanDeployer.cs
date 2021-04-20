using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FanDeployer : MonoBehaviour
{
    //半径
    [SerializeField]
    public float _radius;

    //子データ取得用
    List<GameObject> childList = new List<GameObject>();
    //private GameObject CircleCenter;

    //=================================================================================
    //初期化
    //=================================================================================

    private void Awake()
    {
        Deploy();
    }

    //Inspectorの内容(半径)が変更された時に実行
    private void OnValidate()
    {
        //CircleCenter = this.gameObject.GetComponent<GameObject>();
        Deploy();
    }

    //子を円状に配置する(ContextMenuで鍵マークの所にメニュー追加)
    [ContextMenu("Deploy")]
    public void Deploy()
    {

        //子を取得
        foreach (Transform child in transform)
        {
            childList.Add(child.gameObject);
        }

        //数値、アルファベット順にソート
        childList.Sort(
          (a, b) =>
          {
              return string.Compare(a.name, b.name);
          }
        );

        //オブジェクト間の角度差
        float angleDiff = 80f / (float)childList.Count;

        //各オブジェクトを円状に配置
        for (int i = 0; i < childList.Count; i++)
        {
            Vector3 childPostion = transform.position;
            
            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            childPostion.x += _radius * Mathf.Cos(angle);
            childPostion.y += _radius * Mathf.Sin(angle);

            childList[i].transform.position = childPostion;
        }
        //CircleCenter.transform.Rotate(0.0f, 0.0f, -10);
    }
}