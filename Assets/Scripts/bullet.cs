using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

  public int speed = 10;

	// Use this for initialization
	void Start () {
        Invoke("DestroySelf", 1.0f);
    }

    void DestroySelf()
    {
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
		GetComponent<Rigidbody2D>().velocity = transform.right.normalized * speed;
	}
}
