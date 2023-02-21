using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to control spawnable buttons 
    /// in a Runner game. Includes button functions
    /// and other logics.
    /// </summary>
    public class SpawnableButtonController : MonoBehaviour
    {
        /// <summary> Returns the SpawnableButtonController. </summary>
        #region Variable Declaration
        public static SpawnableButtonController Instance => s_Instance;
        static SpawnableButtonController s_Instance;

        #endregion

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;

            Initialize();
        }

        public void Initialize()
        {

        }

        public void SpawnBombTrap(float distance)
        {

        }

        public void SpawnBridge(float distance)
        {

        }
    }
}