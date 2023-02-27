using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;

namespace HyperCasual.Runner
{
    /// <summary>
    /// Ends the game on collision, forcing a win state.
    /// </summary>
    [ExecuteInEditMode]
    [RequireComponent(typeof(Collider))]
    public class FinishLine : Spawnable
    {
        const string k_PlayerTag = "Player";
        

        void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag(k_PlayerTag))
            {
                //GameManager.Instance.Win();
                if (!GameSceneLoad.Instance.isFinishRun)
                {
                    GameSceneLoad.Instance.isFinishRun = true;
                    Debug.LogError(PlayerController.Instance.Characters.Count);
                    GameSceneLoad.Instance.Action_FinishRunGame(PlayerController.Instance.Characters.Count);
                }
               
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            if (GameSceneLoad.Instance != null)
            {
                GameSceneLoad.Instance.SetPositionMergeGame(this.transform.position);
            }
        }
    }
}