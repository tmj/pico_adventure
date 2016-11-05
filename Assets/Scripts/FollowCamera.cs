using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = target.transform.position;
		pos.y += 2;
		pos.z = this.transform.position.z;
		this.transform.position = pos;

	}
}
