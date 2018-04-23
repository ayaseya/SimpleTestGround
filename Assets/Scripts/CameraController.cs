using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform m_Target;		// カメラの追尾対象（プレイヤー）の位置
	public float m_Smoothing = 5f;	// カメラの追尾速度

	private Vector3 offset;			// カメラからm_Targetまでの距離、
									// カメラは常にプレイヤーからoffset分の距離を保つことになる

	void Start ()
	{
		// プレイヤーの初期位置からoffsetを初期化する
		offset = transform.position - m_Target.position;
	}


	void FixedUpdate ()
	{
		// プレイヤーの位置から追尾後のカメラ位置を算出する
		Vector3 targetCamPos = m_Target.position + offset;

		// プレイヤーの位置を元にカメラを移動させる
		transform.position = Vector3.Lerp (transform.position, targetCamPos, m_Smoothing * Time.deltaTime);
	}
}