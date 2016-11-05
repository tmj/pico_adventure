using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public int speed = 3;

	Vector3 dir;

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", 1.0f);
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

	/// <summary>
	/// 弾の方向を設定する
	/// </summary>
	/// <param name="dir"></param>
	public void SetDirection(Vector3 dir) {
		this.dir = dir;
	}

    // Update is called once per frame
    void Update () {
		GetComponent<Rigidbody2D>().velocity = dir * speed;
	}
}
