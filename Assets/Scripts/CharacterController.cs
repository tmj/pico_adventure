using UnityEngine;
using System.Collections;

public class CharacterController: MonoBehaviour {

	public float speed = 15;
	public float JUMP_POW = 1000f;
	public GameObject bullet;

	Rigidbody2D rigidbody2d;
	int groundLayer;
	bool is_grounded = false;

	SpriteRenderer sprite;

	void Start() {
		this.sprite = this.GetComponent<SpriteRenderer>();
		this.rigidbody2d = this.GetComponent<Rigidbody2D>();
		this.groundLayer = LayerMask.NameToLayer("Ground");
	}


	// Update is called once per frame
	void Update() {
		float x = Input.GetAxisRaw("Horizontal");

		if(sprite.flipX && 0 < x) {
			// flipX==true は右向き
			sprite.flipX = false;
		} else if(!sprite.flipX && x < 0) {
			sprite.flipX = true;
		}

		if (Mathf.Abs(x) > 0) {
			// 移動する向きとスピードを代入する
			this.rigidbody2d.AddForce( Vector2.right * x * speed);
		}

		if (Input.GetButtonDown("Jump") && is_grounded) {
			// ジャンプ
			this.rigidbody2d.AddForce( Vector2.up * JUMP_POW);
			is_grounded = false;
		}

		if(Input.GetButtonDown("Fire1")) {
			// 弾発射
			Fire();
		}

		// 地面あたり判定可視化
		Debug.DrawRay(this.transform.position, Vector3.down, Color.green, -1);
	}

	void Fire() {
		GameObject go = (GameObject)Instantiate(bullet);
		go.transform.position = this.transform.position;
		go.transform.localScale = Vector3.one;

		go.GetComponent<Bullet>().SetDirection(sprite.flipX ? Vector3.left : Vector3.right);
	}

	void FixedUpdate() {
		
		// 足が地面に触れているか
		is_grounded = Physics2D.Linecast(this.transform.position, this.transform.position + Vector3.down, 1 << this.groundLayer);
	}
}
