using UnityEngine;
using System.Collections;

public class CharacterObserver : MonoBehaviour {

	public GameObject character;

	public Stage stage;

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
	
		if(character.transform.position.y < stage.deadline.PosY) {
			character.transform.position = stage.SpawnPointPosition;
		}
	}
}
