using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains Game-over Screen functionalities
    /// </summary>
    public class GameoverScreen : View
    {
        [SerializeField]
        HyperCasualButton m_PlayAgainButton;
        [SerializeField]
        AbstractGameEvent m_PlayAgainEvent;
        [SerializeField]
        AbstractGameEvent m_GoToMainMenuEvent;
        [SerializeField] TextMeshProUGUI m_txt_level;

        void OnEnable()
        {
            m_PlayAgainButton.AddListener(OnPlayAgainButtonClick);

            m_txt_level.text = "Level " + (SaveManager.Instance.LevelProgress + 1);

            GameSceneLoad.Instance.SetGameIsPlaying(false);
        }

        void OnDisable()
        {
            m_PlayAgainButton.RemoveListener(OnPlayAgainButtonClick);
        }

        void OnPlayAgainButtonClick()
        {
            m_PlayAgainEvent.Raise();
        }

        void OnGoToMainMenuButtonClick()
        {
            m_GoToMainMenuEvent.Raise();
        }
    }
}