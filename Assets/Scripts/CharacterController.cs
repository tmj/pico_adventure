using UnityEngine;
using System.Collections;

public class CharacterController: MonoBehaviour {

	public float speed = 15;
	public float JUMP_POW = 1000f;
	public GameObject bullet;

	Rigidbody2D rigidbody2d;
	int groundLayer;
	bool is_grounded = false;	// 足地面についてるか
	bool is_controllable = true;		// 操作可能か

	SpriteRenderer sprite;
	Object prefab_eff_dead;


	// 死亡イベント
	public event System.Action eventDead;

	void Start() {
		this.prefab_eff_dead = Resources.Load("Effect/eff_dead");


		this.sprite = GetComponent<SpriteRenderer>();
		this.rigidbody2d = GetComponent<Rigidbody2D>();
		this.groundLayer = LayerMask.NameToLayer("Ground");
	}

	void Update() {

		if (is_controllable) { 
			Controll();
		}

		// 地面あたり判定可視化
		Debug.DrawRay(transform.position, Vector3.down, Color.green, -1);
	}

	void Controll() {
		float x = Input.GetAxisRaw("Horizontal");

		if (sprite.flipX && 0 < x) {
			// flipX==true は右向き
			this.sprite.flipX = false;
		}
		else if (!sprite.flipX && x < 0) {
			this.sprite.flipX = true;
		}

		if (Mathf.Abs(x) > 0) {
			// 移動する向きとスピードを代入する
			this.rigidbody2d.AddForce(Vector2.right * x * speed);
		}

		if (Input.GetButtonDown("Jump") && this.is_grounded) {
			// ジャンプ
			this.rigidbody2d.AddForce(Vector2.up * JUMP_POW);
			this.is_grounded = false;
		}

		if (Input.GetButtonDown("Fire1")) {
			// 弾発射
			Fire();
		}
	}

	void Fire() {
		GameObject go = (GameObject)Instantiate(this.bullet);
		go.transform.position = this.transform.position;
		go.transform.position += new Vector3( (this.sprite.flipX ? -1.0f : 1.0f), 0.0f, 0.0f);
		go.transform.localScale = Vector3.one;

		go.GetComponent<Bullet>().SetDirection(this.sprite.flipX ? Vector3.left : Vector3.right);
	}

	void FixedUpdate() {

		// 足が地面に触れているか
		this.is_grounded = Physics2D.Linecast(transform.position, transform.position + Vector3.down, 1 << this.groundLayer);
	}


	Coroutine die;
	/// <summary>
	/// キャラ殺す
	/// </summary>
	public void Die() {
		if (this.die != null) return;

		this.die = StartCoroutine(this.Coroutine_Die());
	}

	IEnumerator Coroutine_Die() {
		this.is_controllable = false;

		Instantiate(this.prefab_eff_dead, transform.position, transform.rotation);

		yield return new WaitForSeconds(0.8f);

		this.is_controllable = true;
		this.die = null;

		// 死亡を通知
		if (eventDead != null) {
			eventDead();
		}
	}
}
