using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    // carPrefabを入れる
    public GameObject carPrefab;
    // coinPrefabを入れる
    public GameObject coinPrefab;
    // conePrefabを入れる
    public GameObject conePrefab;
    // アイテムを出すX方向の範囲
    private float posRange = 3.4f;
    // ゴール地点
    public int goalPos = 360;

    // Unityちゃんのオブジェクト
    private GameObject unitychan;
    // UnityちゃんのZ座標
    private float unitychanPosZ;
    // 次回アイテムを生成するUnityちゃんの位置
    private int nextItemGenPos = 0;

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

        // ゴールから先にはアイテムを生成しない（ゴールから45m手前でアイテム生成をストップ）
        if (((int)this.unitychanPosZ + 45) < goalPos)
        {
            // UnityちゃんがZ方向に15m進むごとに45m先にアイテムを生成
            if (((int)this.unitychanPosZ) >= this.nextItemGenPos)
            {
                // Unityちゃんの45m先にアイテムを生成、どのアイテムを出すのかはランダムに設定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    // コーンをX軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, (this.unitychanPosZ + 45));

                        // 生成後3.5秒経過でコーンを破棄するようにすると上手く動作しました
                        // しかし画面スクロール速度が変わると画面から消える時間も変わりますので、採用しませんでした
                        // Destroy(cone, 3.5f);
                    }
                }
                else
                {
                    // レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        // アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        // アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        // 60%コイン配置：30％車配置：10％なにもなし
                        if (1 <= item && item <= 6)
                        {
                            // コインを生成
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, (this.unitychanPosZ + 45) + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            // 車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, (this.unitychanPosZ + 45) + offsetZ);
                        }
                    }
                }
                // 次回アイテムを生成するUnityちゃんの位置を更新
                this.nextItemGenPos += 15;
            }
        }
    }
}
