using UnityEngine;
using UnityEngine.UI;

public class LifeGaugeController : MonoBehaviour {

    private Slider m_Slider;                // 現在のライフを示すスライダー
	private Quaternion m_RelativeRotation;  // シーンの開始時のローカル空間のローテーション

    private void Awake()
    {
        m_Slider = GetComponent<Slider>();
    }

    private void Start()
    {
        m_RelativeRotation = transform.parent.localRotation;
    }

    private void Update()
    {   
        // スライダーがプレイヤーの方向に追従して回転してしまうのを防ぐため
        // 初期値を設定し直す
        transform.rotation = m_RelativeRotation;
    }

    public void OnGaugeUpdate(float life)
    {
        m_Slider.value = life;
    }
}