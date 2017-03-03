using UnityEngine;
using System.Collections;

public class CharacterObserver : MonoBehaviour {

	public CharacterController character;


	public Stage stage;

	// Use this for initialization
	void Start () {
		character.eventDead += this.OnPlayerDead;
	}
	
	void FixedUpdate () {
	
		if(character.transform.position.y < stage.deadline.PosY) {
			RepositionPlayer();
		}
	}

	void RepositionPlayer() {
		character.transform.position = stage.SpawnPointPosition;
	}

	void OnPlayerDead() {
		RepositionPlayer();
	}
}
