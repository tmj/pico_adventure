using UnityEngine;
using System.Collections;

public class CharacterObserver : MonoBehaviour {

	public static CharacterObserver Instance {
		get { return instance; }
	}
	static CharacterObserver instance;

	public CharacterController character;
	public Stage stage;
	public System.Action onReset;

	void Awake() {
		if(instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	// Use this for initialization
	void Start () {
		character.eventDead += this.OnPlayerDead;
	}
	
	void FixedUpdate () {
	
		if(character.transform.position.y < stage.deadline.PosY) {
			RepositionPlayer();
		}
	}

	public void RepositionPlayer() {
		character.transform.position = stage.SpawnPointPosition;
		stage.clearLine.clearText.gameObject.SetActive(false);

		if(onReset != null) {
			onReset();
		}
	}

	void OnPlayerDead() {
		RepositionPlayer();
	}
}
