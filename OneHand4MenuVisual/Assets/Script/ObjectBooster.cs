using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBooster : MonoBehaviour
{
    Vector3 defaultPos;
    Vector3 throwPos = new Vector3(0.5f, -2.0f, 18.0f);

    Vector3 forceDirection = new Vector3(0f, 1.0f, -1.0f);   //45度の向きに射出するように定義
    float forceMagnitude = 10.0f;                           //力の大きさを定義

    //Vector3 forceDirection = new Vector3(0f, 0.5f, -1.0f);   //level2
    //float forceMagnitude = 15.0f;                           

    //Vector3 forceDirection = new Vector3(0f, 0.3f, -1.0f);   //level3
    //float forceMagnitude = 20.0f;                           

    // Start is called before the first frame update
    void Start()
    {
        Transform MyTrans = this.transform;
        defaultPos = MyTrans.position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform MyTransNow = this.transform;
        Vector3 worldPos = MyTransNow.position;

        //下に落ちたら初期位置に戻る
        if (worldPos.y < -5.0f)
        {
            MyTransNow.position = defaultPos;
            ObjectZero();
        }
    }

    //投擲メソッド
    public void ObjectImpulse()
    {
        //投擲位置へ移動
        Transform MyTransNow = this.transform;
        Vector3 worldPos = MyTransNow.position;
        MyTransNow.position = throwPos;

        Rigidbody rb = gameObject.GetComponent<Rigidbody>();    //Impulse(撃力)
        ObjectZero();

        //設定値で投擲
        Vector3 force = forceMagnitude * forceDirection;
        rb.AddForce(force, ForceMode.Impulse);
    }

    private void ObjectZero()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();    //Impulse(撃力)

        //運動をゼロに
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
