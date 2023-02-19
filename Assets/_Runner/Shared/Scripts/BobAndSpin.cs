using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Core
{
    public class BobAndSpin : MonoBehaviour
    {
        public bool UsePositionBasedOffset = true;
        public float PositionBasedScale = 2.0f;

        [Header("--- Bob")]
        public bool Bob = true;
        public float BobSpeed = 5.0f;
        public float BobHeight = 0.2f;

        [Header("--- Spin")]
        public bool Spin = true;
        [SerializeField] Axis m_Axis;
        public float SpinSpeed = 180.0f;

        enum Axis
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        // internal.
        Transform m_Transform;
        Vector3 m_StartPosition;
        Quaternion m_StartRotation;

        void Awake()
        {
            SetDefault();
        }

        void Update()
        {
            float offset = (UsePositionBasedOffset) ? m_StartPosition.z * PositionBasedScale + Time.time : Time.time;

            if (Bob)
            {
                m_Transform.position = m_StartPosition + Vector3.up * Mathf.Sin(offset * BobSpeed) * BobHeight;
            }

            if (Spin)
            {
                switch (m_Axis)
                {
                    case Axis.X:
                        Rotate(offset, Vector3.right);
                        break;
                    case Axis.Y:
                        Rotate(offset, Vector3.up);
                        break;
                    case Axis.Z:
                        Rotate(offset, Vector3.forward);
                        break;
                }
            }
        }

        void SetDefault()
        {
            m_Transform = transform;
            m_StartPosition = m_Transform.position;
            m_StartRotation = m_Transform.rotation;
        }

        void Rotate(float _offset, Vector3 _axis)
        {
            m_Transform.rotation = m_StartRotation * Quaternion.AngleAxis(_offset * SpinSpeed, _axis);
        }
    }
}