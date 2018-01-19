using UnityEngine;
using System.Collections;

public class CharacterController: MonoBehaviour {

	public float maxVelocity = 50;
	public float speed = 0.1f;
	public float JUMP_POW = 1000f;
	public GameObject bullet;
	public GameObject deviceInput;

	Rigidbody2D rigidbody2d;
	int groundLayer;
	bool is_grounded = false;	// 足地面についてるか
	bool is_controllable = true;		// 操作可能か

	SpriteRenderer sprite;
	Object prefab_eff_dead;

	float moveVect = 0;
	float accelerate = 0;

	// 死亡イベント
	public event System.Action eventDead;
	float velocity;

	void Awake() {
#if UNITY_ANDROID || UNITY_IOS
		deviceInput.gameObject.SetActive(true);
#endif
	}

	void Start() {
		this.prefab_eff_dead = Resources.Load("Effect/eff_dead");


		this.sprite = GetComponent<SpriteRenderer>();
		this.rigidbody2d = GetComponent<Rigidbody2D>();
		this.groundLayer = LayerMask.NameToLayer("Ground");
	}

	void Update() {
		float x = 0;
		if (is_controllable) {
			x = Input.GetAxisRaw("Horizontal");
			if(x < 0) {
				this.sprite.flipX = true;
			} else if(0 < x) {
				this.sprite.flipX = false;
			}

#if UNITY_ANDROID || UNITY_IOS
			x = moveVect;
#endif
			if (Input.GetButtonDown("Jump")) {
				// ジャンプ
				Jump();
			}
#if UNITY_EDITOR
			if (Input.GetButtonDown("Fire1")) {
				// 弾発射
				Fire();
			}
#endif
		}

		MoveX( x );

		if (this.rigidbody2d.velocity.magnitude > maxVelocity) {
			this.rigidbody2d.velocity = this.rigidbody2d.velocity.normalized * maxVelocity;
		}

		// 地面あたり判定可視化
		Debug.DrawRay(transform.position, Vector3.down, Color.green, -1);
	}

	public void MoveRight(bool key_down) {
		if(key_down) moveVect = 1;
		else moveVect = 0;

		this.sprite.flipX = false;
	}

	public void MoveLeft(bool key_down) {
		if(key_down) moveVect = -1;
		else moveVect = 0;

		this.sprite.flipX = true;
	}

	public void MoveX(float val) {

		float dir = 1;
		if(this.sprite.flipX) {
			dir = -1;
		}

		if (0 < Mathf.Abs(val)) {
			accelerate += speed;
		} else {
			accelerate -= speed;
		}

		velocity += accelerate;
		accelerate = Mathf.Clamp(accelerate, 0, 10);
		velocity = Mathf.Clamp(velocity, 0, maxVelocity);
		if (Mathf.Abs(val) > 0)
		{
			// 移動する向きとスピードを代入する
			this.rigidbody2d.AddForce(Vector2.right * val * speed);
			//this.transform.position += Vector3.right * dir * velocity * Time.deltaTime;
		}
	}

	public void Jump() {
		if (this.is_grounded) {
			this.rigidbody2d.AddForce(Vector2.up * JUMP_POW);
			this.is_grounded = false;
		}
	}

	public void Fire() {
		GameObject go = (GameObject)Instantiate(this.bullet);
		go.transform.position = this.transform.position;
		go.transform.position += new Vector3( (this.sprite.flipX ? -1.0f : 1.0f), 0.0f, 0.0f);
		go.transform.localScale = Vector3.one;

		go.GetComponent<Bullet>().SetDirection(this.sprite.flipX ? Vector3.left : Vector3.right);
	}

	void FixedUpdate() {

		this.is_grounded = false;

		// 足が地面に触れているか
		this.is_grounded |= Physics2D.Linecast(transform.position + Vector3.right, transform.position + Vector3.right + Vector3.down, 1 << this.groundLayer);
		this.is_grounded |= Physics2D.Linecast(transform.position - Vector3.right, transform.position - Vector3.right + Vector3.down, 1 << this.groundLayer);
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
