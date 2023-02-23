using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing an Obstacle triggered
    /// when interacting with ...
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class BombTrap : Explosive
    {
        [Header("___DaBomb___")]
        [SerializeField]
        GameObject m_VFX;

        public override void ResetSpawnable()
        {
            base.ResetSpawnable();

            gameObject.SetActive(true);
            m_VFX.SetActive(false);
        }

        protected override void ChangeColliderSize()
        {
            base.ChangeColliderSize();

            m_VFX.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
