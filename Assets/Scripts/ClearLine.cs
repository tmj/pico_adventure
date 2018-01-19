using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClearLine : MonoBehaviour {

	public Text clearText;

	void OnTriggerEnter2D(Collider2D collision) {

		if (collision.gameObject.tag == TagDefine.Player) {
			clearText.gameObject.SetActive(true);
		}
	}
}
