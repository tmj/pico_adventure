using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {

	/// <summary>
	/// スポーン座標
	/// </summary>
	public Vector3 SpawnPointPosition {
		get { return spawnPoint.transform.position; }
	}

	public DeadLine deadline;
	public GameObject spawnPoint;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
