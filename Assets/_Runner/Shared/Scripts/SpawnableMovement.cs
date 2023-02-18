using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Core
{
    public class SpawnableMovement : MonoBehaviour
    {
        public bool UsePositionBasedOffset = true;
        public float PositionBasedScale = 2.0f;
        [Space(5)]

        [Header("--- Bob")]
        public bool Bob = true;
        public float BobSpeed = 5.0f;
        public float BobHeight = 0.2f;
        [Space(5)]

        [Header("--- Spin")]
        public bool Spin = true;
        [SerializeField] Axis m_Axis;
        [SerializeField] Pivot m_Pivot;
        public float SpinSpeed = 180.0f;

        enum Axis
        {
            X = 0,
            Y = 1,
            Z = 2
        }

        enum Pivot
        {
            Center,
            Edge,
        }

        // internal.
        Transform m_Transform;
        Vector3 m_StartPosition;
        Quaternion m_StartRotation;
        bool isCenter;

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

                switch (m_Pivot)
                {
                    case Pivot.Center:
                        SelectPivot(isCenter);
                        break;
                    case Pivot.Edge:
                        SelectPivot(!isCenter);
                        break;
                }
            }
        }

        void SetDefault()
        {
            m_Transform = transform;
            m_StartPosition = m_Transform.position;
            m_StartRotation = m_Transform.rotation;
            isCenter = true;
        }

        void Rotate(float _offset, Vector3 _axis)
        {
            m_Transform.rotation = m_StartRotation * Quaternion.AngleAxis(_offset * SpinSpeed, _axis);
        }

        void SelectPivot(bool _isCenter)
        {
            if (!_isCenter)
            {

            }
        }
    }
}