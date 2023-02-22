using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing an Obstacle (which
    /// inherits from Spawnable) object. If a 
    /// GameObject tagged "Player" collides with
    /// this object, a larger collider will be
    /// activated and the "Player" GameObject
    /// will be destroyed.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class Explosive : Obstacle, ISwitchable
    {
        #region Variable Declaration
        [Header("___DaBomb___")]
        [SerializeField]
        GameObject m_MeshNMaterial;
        [SerializeField]
        GameObject m_VFX;

        BoxCollider m_BoxCollider;
        CapsuleCollider m_CapsuleCollider;
        SphereCollider m_SphereCollider;

        Vector3 m_OriginalBoxColliderSize;
        float m_OriginalSphereColliderRadius;

        [SerializeField]
        ColliderType m_ColliderType = ColliderType.BoxCollider;
        [SerializeField]
        Vector3 m_TargetBoxColliderSize = new Vector3(2.5f, 2.5f, 2.5f);
        [SerializeField]
        float m_TargetSphereColliderRadius = 2.5f;

        enum ColliderType
        {
            BoxCollider,
            CapsuleCollider,
            SphereCollider
        }
        #endregion

        public override void ResetSpawnable()
        {
            base.ResetSpawnable();

            DefaultState();
        }

        protected override void Awake()
        {
            base.Awake();

            switch (m_ColliderType)
            {
                case ColliderType.BoxCollider:
                    m_BoxCollider = gameObject.GetComponent<BoxCollider>();
                    m_OriginalBoxColliderSize = m_BoxCollider.size;
                    break;
                case ColliderType.CapsuleCollider:
                    m_CapsuleCollider = gameObject.GetComponent<CapsuleCollider>();
                    // stuff
                    break;
                case ColliderType.SphereCollider:
                    m_SphereCollider = gameObject.GetComponent<SphereCollider>();
                    m_OriginalSphereColliderRadius = m_SphereCollider.radius;
                    break;
            }
        }

        protected override void ChangeColliderSize()
        {
            BombTriggeredState();

            switch (m_ColliderType)
            {
                case ColliderType.BoxCollider:
                    SetBoxColliderSize(m_TargetBoxColliderSize);
                    break;
                case ColliderType.CapsuleCollider:
                    SetCapsuleColliderSize();
                    break;
                case ColliderType.SphereCollider:
                    SetSphereColliderSize(m_TargetSphereColliderRadius);
                    break;
            }
        }

        protected override void ResetColliderSize()
        {
            switch (m_ColliderType)
            {
                case ColliderType.BoxCollider:
                    SetBoxColliderSize(m_OriginalBoxColliderSize);
                    break;
                case ColliderType.CapsuleCollider:
                    SetCapsuleColliderSize();
                    break;
                case ColliderType.SphereCollider:
                    SetSphereColliderSize(m_OriginalSphereColliderRadius);
                    break;
            }
        }

        public void Active()
        {
            SwitchTriggeredState();
        }

        void DefaultState()
        {
            m_MeshNMaterial.SetActive(false);
            m_VFX.SetActive(false);
        }

        void SwitchTriggeredState()
        {
            m_MeshNMaterial.SetActive(true);
            m_VFX.SetActive(false);
        }

        void BombTriggeredState()
        {
            m_MeshNMaterial.SetActive(false);
            m_VFX.SetActive(true);
        }

        /// <summary>
        /// Sets the size of the box collider.
        /// </summary>
        /// <param name="size">
        /// The size you want to set for the box collider.
        /// </param>
        protected virtual void SetBoxColliderSize(Vector3 size)
        {
            m_BoxCollider.size = size;
        }

        /// <summary>
        /// 
        /// </param>
        protected virtual void SetCapsuleColliderSize()
        {
            // Do this later.
        }

        /// <summary>
        /// Sets the size of the sphere collider.
        /// </summary>
        /// <param name="radius">
        /// The radius you want to set for the sphere collider.
        /// </param>
        protected virtual void SetSphereColliderSize(float radius)
        {
            m_SphereCollider.radius = radius;
        }
    }
}