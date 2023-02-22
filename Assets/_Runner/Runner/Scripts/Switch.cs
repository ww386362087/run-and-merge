using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    /*[SerializeField]
    SoundID m_Sound = SoundID.ButtonSound;*/
    [SerializeField]
    ISwitchable target;

    bool m_Triggered;

    readonly float m_ObjectHeight = 2; // button prefab is created from cylinder mesh with height 2.
    protected Transform m_Transform;
    Vector3 m_OriginalPosition;
    Vector3 m_TargetPosition;
    Vector3 m_OriginalScale;
    Vector3 m_TargetScale;

    const string k_PlayerTag = "Player";

    private void Awake()
    {
        m_Transform = transform;
        m_OriginalPosition = m_Transform.position;
        m_OriginalScale = m_Transform.localScale;
    }

    private void Start()
    {
        target = gameObject.GetComponentInParent<ISwitchable>();

        ResetSwitch();
    }

    void ResetSwitch()
    {
        m_Triggered = false;

        SetPosition(m_OriginalPosition);
        SetScale(m_OriginalScale);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(k_PlayerTag) && !m_Triggered)
        {
            ButtonTriggered();
            target?.Active();
        }
    }

    void ButtonTriggered()
    {
        // Change switch position.
        float offset = m_ObjectHeight * (m_OriginalScale.y - m_TargetScale.y) / 2;
        float newPos_Y = m_OriginalPosition.y - offset;

        m_TargetPosition = new Vector3(m_OriginalPosition.x, newPos_Y, m_OriginalPosition.z);
        SetPosition(m_TargetPosition);

        // Change switch scale.
        m_TargetScale = new Vector3(m_OriginalScale.x, m_OriginalScale.y / 10, m_OriginalScale.z);
        SetScale(m_TargetScale);

        //
        m_Triggered = true;
        //AudioManager.Instance.PlayEffect(m_Sound);
    }

    void SetPosition(Vector3 position)
    {
        m_Transform.position = position;
    }

    void SetScale(Vector3 scale)
    {
        m_Transform.localScale = scale;
    }
}
