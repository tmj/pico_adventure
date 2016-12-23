using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DeadLine : MonoBehaviour {

	public float PosY {
		get { return this.transform.position.y; }
	}

	public float CamStopPosY {
		get { return camStopPosY.y; }
	}

	Vector3 camStopPosY;
	const float LINE_LENGTH = 5000;

	void Start() {
		camStopPosY = transform.position + Vector3.up * 20;
	}

	void Update () {

		Debug.DrawLine(camStopPosY - Vector3.left * LINE_LENGTH, camStopPosY - Vector3.right * LINE_LENGTH, Color.green);

		Debug.DrawLine(this.transform.position - Vector3.left * LINE_LENGTH, this.transform.position - Vector3.right * LINE_LENGTH, Color.red);
	}
}
