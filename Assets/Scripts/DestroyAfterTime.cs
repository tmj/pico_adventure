using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float time;

	// Use this for initialization
	void Start() {
		Invoke("DestroySelf", time);
	}

	void DestroySelf() {
		Destroy(this.gameObject);
	}
}
