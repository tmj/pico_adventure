using UnityEngine;
using System.Collections;

public class charactor : MonoBehaviour {

  public float speed = 15;

  public GameObject bullet;
  IEnumerator Start ()
  {
      while (true) {
          // 弾をプレイヤーと同じ位置/角度で作成
          Instantiate (bullet, transform.position, transform.rotation);
          // 0.05秒待つ
          yield return new WaitForSeconds (1.0f);
      }
  }


	// Update is called once per frame
	void Update () {
	     float x = Input.GetAxisRaw ("Horizontal");

        // 上・下
        float y = Input.GetAxisRaw ("Vertical");

        // 移動する向きを求める
        Vector2 direction = new Vector2 (x, y).normalized;

        // 移動する向きとスピードを代入する
        GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
}
