using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing a Spawnable object.
    /// If a GameObject tagged "Player" collides
    /// with this object, it will trigger a Button
    /// function with SpawnableButtonController.
    /// </summary>
    public class SpawnableButton : Spawnable
    {
        [SerializeField]
        SoundID m_Sound = SoundID.ButtonSound;

        const string k_PlayerTag = "Player";

        [SerializeField]
        ButtonType m_ButtonType = ButtonType.BombTrapSpawner;
        [SerializeField]
        float m_SpawnDistance;

        bool m_Triggered;

        readonly float m_ObjectHeight = 2; // button prefab is created from cylinder mesh with hight 2.
        Vector3 m_OriginalPosition;
        Vector3 m_TargetPosition;
        Vector3 m_OriginalScale;
        Vector3 m_TargetScale;

        enum ButtonType
        {
            None,
            BombTrapSpawner,
            BridgeSpawner
        }

        protected override void Awake()
        {
            base.Awake();

            m_OriginalPosition = m_Transform.position;
            m_OriginalScale = m_Transform.localScale;
        }

        /// <summary>
        /// Reset the button to its initial state. Called 
        /// when a level is restarted by the GameManager.
        /// </summary>
        public override void ResetSpawnable()
        {
            m_Triggered = false;

            SetPosition(m_OriginalPosition);
            SetScale(m_OriginalScale);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag) && !m_Triggered)
            {
                TriggerButton();
            }
        }

        void TriggerButton()
        {
            ButtonTriggered();

            switch (m_ButtonType)
            {
                case ButtonType.None:
                    break;
                case ButtonType.BombTrapSpawner:
                    SpawnableButtonController.Instance.SpawnBombTrap(m_SpawnDistance);
                    break;
                case ButtonType.BridgeSpawner:
                    SpawnableButtonController.Instance.SpawnBridge(m_SpawnDistance);
                    break;
            }
        }

        void ButtonTriggered()
        {
            // Change position.
            float offset = m_ObjectHeight * (m_OriginalScale.y - m_TargetScale.y) / 2;
            float newPos_Y = m_OriginalPosition.y - offset;

            m_TargetPosition = new Vector3(m_OriginalPosition.x, newPos_Y, m_OriginalPosition.z);
            SetPosition(m_TargetPosition);

            // Change scale.
            m_TargetScale = new Vector3(m_OriginalScale.x, m_OriginalScale.y / 10, m_OriginalScale.z);
            SetScale(m_TargetScale);

            //
            m_Triggered = true;
            AudioManager.Instance.PlayEffect(m_Sound);
        }
    }
}