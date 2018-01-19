using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRamp : MonoBehaviour {

	public int power = 10;
	bool entered = false;


	void OnTriggerEnter2D(Collider2D collision) {
		if (!entered) {
			if (collision.gameObject.tag == TagDefine.Player) {
				Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
				rigid.AddForce(this.transform.up * power, ForceMode2D.Impulse);
				entered = true;
			
				StartCoroutine(EnableEnter());
			}
		}
	}

	void OnTriggerStay2D(Collider2D collision) {
		if (!entered) {
			OnTriggerEnter2D(collision);
		}
	}

	IEnumerator EnableEnter() {

		yield return new WaitForSeconds(0.13333f);

		entered = false;
	}
}
