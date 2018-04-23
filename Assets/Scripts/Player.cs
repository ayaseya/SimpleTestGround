using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float m_Speed = 6f;	// 移動速度

	Animator m_Animator;
	Rigidbody m_Rigidbody;
	Vector3 m_Movement;

	void Awake ()
	{
		m_Animator = GetComponent<Animator> ();
		m_Rigidbody = GetComponent<Rigidbody> ();
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
		if (!isRunning ())
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
		m_Animator.SetBool ("Running", isRunning ());
	}

	private bool isRunning ()
	{
		// ベクトルの長さでキャラクターが移動中か判断する
		return m_Movement.magnitude != 0f;
	}
}
