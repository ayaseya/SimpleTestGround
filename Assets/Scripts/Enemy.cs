using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	void Update ()
	{
		// 親オブジェクトを取得してY軸回転させる
		GameObject parent = transform.root.gameObject;
		parent.transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other)
	{
		// プレイヤーだった場合、ダメージ処理を呼び出す
		if (other.gameObject.CompareTag ("Player"))
		{
			Player player = other.gameObject.GetComponent<Player> ();
			player.TakeDamage (10);
		}
	}
}
