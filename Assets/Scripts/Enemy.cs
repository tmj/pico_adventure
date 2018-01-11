using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	Vector3 defaultPos;

	// Use this for initialization
	void Start () {
		CharacterObserver.Instance.onReset += this.SetPositionDefault;
		defaultPos = transform.position;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		
		if(collision.gameObject.tag == TagDefine.Player) {
			collision.gameObject.SendMessage("Die");
		}
	}

	public void SetPositionDefault() {
		transform.position = defaultPos;
		transform.rotation = Quaternion.identity;
	}
}
