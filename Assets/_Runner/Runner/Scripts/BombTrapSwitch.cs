using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing an Obstacle triggered
    /// when interacting with ...
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class BombTrapSwitch : Explosive, ISwitchable
    {
        [Header("___DaBomb___")]
        [SerializeField]
        GameObject[] m_MeshNMaterial;
        [SerializeField]
        GameObject[] m_VFX;

        // Switch action.
        public void Active()
        {
            SwitchTriggeredState();
        }

        // Changing bomb states
        public override void ResetSpawnable()
        {
            base.ResetSpawnable();

            DefaultState();
        }

        protected override void ChangeColliderSize()
        {
            BombTriggeredState();

            base.ChangeColliderSize();
        }

        // ---
        void DefaultState()
        {
            SetState(false, false);
        }

        void SwitchTriggeredState()
        {
            SetState(true, false);
        }

        void BombTriggeredState()
        {
            SetState(false, true);
        }

        /// <summary>
        /// Sets the state (active/inactive) of the object.
        /// </summary>
        /// <param name="_mat">
        /// The state of the object's material.
        /// </param>
        /// /// <param name="_vfx">
        /// The active state of the VFX.
        /// </param>
        protected virtual void SetState(bool _mat, bool _vfx)
        {
            for (var i = 0; i < m_MeshNMaterial.Length; i++)
            {
                m_MeshNMaterial[i].SetActive(_mat);
                m_VFX[i].SetActive(_vfx);
            }
        }
    }
}
