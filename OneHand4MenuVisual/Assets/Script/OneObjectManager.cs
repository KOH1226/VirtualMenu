using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneObjectManager : MonoBehaviour
{
    Vector3 defaultPos = new Vector3(1.0f, -2.0f, 20.0f);
    public bool fall;
    // Start is called before the first frame update
    void Start()
    {
        fall = false;
    }

    // Update is called once per frame
    void Update()
    {
        Transform MyTransNow = this.transform;
        Vector3 worldPos = MyTransNow.position;

        //下に落ちたら初期位置に戻って投擲
        if (worldPos.y < -5.0f)
        {
            MyTransNow.position = defaultPos;
            fall = true;
        }
    }

    public bool GetFall()
    {
        return fall;
    }
}
