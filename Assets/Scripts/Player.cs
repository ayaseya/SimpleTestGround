using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	public float m_Speed = 6f;

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
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		m_Movement.Set (h, 0f, v);

		Move ();
		Turn ();
		Animate ();
	}

	private void Move ()
	{
		m_Movement = m_Movement.normalized * m_Speed * Time.deltaTime;
		m_Rigidbody.MovePosition (transform.position + m_Movement);
	}

	private void Turn ()
	{
		if (!isRunning ())
			return;
		Quaternion targetRotation = Quaternion.LookRotation (m_Movement);
		transform.rotation = Quaternion.RotateTowards (transform.rotation, targetRotation, 360.0f * Time.deltaTime);
	}

	private void Animate ()
	{
		m_Animator.SetBool ("Running", isRunning ());
	}

	private bool isRunning ()
	{
		return m_Movement.magnitude != 0f;
	}
}
