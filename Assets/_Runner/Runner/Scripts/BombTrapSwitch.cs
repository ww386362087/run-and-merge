using System.Collections;
using System.Collections.Generic;
using HyperCasual.Gameplay;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class representing an Explosive but
    /// is triggered by a button.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class BombTrapSwitch : Explosive, ISwitchable
    {
        protected override void DefaultState()
        {
            SetState(false, false);
        }

        protected override void ResetColliderState()
        {
            if (m_CacheRoutine != null)
                StopCoroutine(m_CacheRoutine);
            StartCoroutine(SetColliders(false));
        }

        // Switch action.
        public void Active()
        {
            // Play button activation sound.
            AudioManager.Instance.PlayEffect(SoundID.ButtonSound);

            // Set bomb state.
            SetState(true, false);
            
            // Activate bomb colliders.
            if (m_CacheRoutine != null)
                StopCoroutine(m_CacheRoutine);
            StartCoroutine(SetColliders());
        }
    }
}
