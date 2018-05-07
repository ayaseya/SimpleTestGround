using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float m_Speed = 6f;	// 移動速度

	Animator m_Animator;
	Rigidbody m_Rigidbody;
	Vector3 m_Movement;
	Renderer m_Renderer;
    LifeGaugeController m_Life;

	int hp = 100;				// ライフ

	void Awake ()
	{
		m_Animator = GetComponent<Animator> ();
		m_Rigidbody = GetComponent<Rigidbody> ();
		m_Renderer = GetComponentInChildren<Renderer> ();
        m_Life = GetComponentInChildren<LifeGaugeController>();
	}

	void FixedUpdate ()
	{
		// キー入力の取得
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// キー入力からベクトルを設定する
		m_Movement.Set (h, 0f, v);

		Move ();
		Turn ();
		Animate ();
	}

	private void Move ()
	{
		// ベクトルから移動量を取得する
		m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
		m_Rigidbody.MovePosition (transform.position + m_Movement);
	}

	private void Turn ()
	{
		// 移動中でなければ回転処理は行わない
		if (!IsRunning ())
			// NOP.
			return;

		// ベクトルから回転方向を取得する
		Quaternion targetRotation = Quaternion.LookRotation (m_Movement);

		// 回転処理
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, 360.0f * Time.deltaTime);
	}

	private void Animate ()
	{
		// 走るアニメーションの有効、無効化
		m_Animator.SetBool ("Running", IsRunning ());
	}

	private bool IsRunning ()
	{
		// ベクトルの長さでキャラクターが移動中か判断する
		return System.Math.Abs(m_Movement.magnitude) > 0;
	}

	public void TakeDamage (int damage)
	{
		// ライフ減算
		hp -= damage;

        // ライフゲージに反映
        m_Life.OnGaugeUpdate(hp);

		// プレイヤーの進行方向とは逆に弾く処理
		// 仮の値なので反発係数はマジックナンバーとして許容する
		m_Rigidbody.AddForce (transform.forward * -60f, ForceMode.Impulse);

		// 点滅によるダメージ演出
		StartCoroutine (Blink ());
	}

	private IEnumerator Blink ()
	{
		//変更前のマテリアルのコピーを保存
		var original = new Material (m_Renderer.material);

		//キーワードの有効化する
		m_Renderer.material.EnableKeyword ("_EMISSION");

		//白色に光らせる
		m_Renderer.material.SetColor ("_EmissionColor", new Color (1, 1, 1));

		// 0.1秒待機して元の色に戻す
		yield return new WaitForSeconds (0.1f);
		m_Renderer.material = original;
	}
}
