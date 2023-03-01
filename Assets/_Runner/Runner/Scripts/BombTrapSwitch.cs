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

        // Switch action.
        public void Active()
        {
            //Debug.Log("active");
            SetState(true, false);
        }
    }
}
