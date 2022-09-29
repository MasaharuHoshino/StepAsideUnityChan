using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    // Unityちゃんのオブジェクト
    private GameObject unitychan;
    // UnityちゃんのZ座標
    private float unitychanPosZ;

    // Start is called before the first frame update
    void Start()
    {
        // Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        // UnityちゃんのZ座標を取得
        this.unitychanPosZ = this.unitychan.transform.position.z;

        // Unityちゃんから5m離れた後にオブジェクトを破棄する
        if (this.gameObject.transform.position.z < (this.unitychanPosZ - 5))
        {
            Destroy(this.gameObject);
        }
    }

    // このOnBecameInvisible()関数使うのが簡単そうでしたが、Coinだけ消えませんでした
    // 原因はCoinPrefabだけRendererが割り当てられていなかったから
    // CoinPrefabにRenderer割り当てるときちんと消えました

    //private void OnBecameInvisible()
    //{
    //    // 画面の外に消えたオブジェクトの破棄
    //    Destroy(this.gameObject);
    //}
}
