using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace HyperCasual.Core
{
    public class BobAndSpin : MonoBehaviour
    {
        public bool UsePositionBasedOffset = true;
        public float PositionBasedScale = 2.0f;

        [Header("--- Bob")]
        public bool Bob = true;
        [SerializeField] Direction m_Direction;
        public float BobSpeed = 5.0f;
        public float BobHeight = 0.2f;

        [Header("--- Spin")]
        public bool Spin = true;
        [SerializeField] Axis m_Axis;
        public float SpinSpeed = 180.0f;

        enum Direction
        {
            Sideways   = 0,
            UpNDown    = 1,
            BackNForth = 2
        }

        enum Axis
        {
            X          = 3,
            Y          = 4,
            Z          = 5
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
                switch (m_Direction)
                {
                    case Direction.Sideways:
                        Move(offset, Vector3.right);
                        break;
                    case Direction.UpNDown:
                        Move(offset, Vector3.up);
                        break;
                    case Direction.BackNForth:
                        Move(offset, Vector3.forward);
                        break;
                }
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

        void Move(float _offset, Vector3 _axis)
        {
            m_Transform.position = m_StartPosition + _axis * Mathf.Sin(_offset * BobSpeed) * BobHeight;
        }
    }
}