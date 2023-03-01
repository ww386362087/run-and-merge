using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This View contains head-up-display functionalities
    /// </summary>
    public class Hud : View
    {
        [SerializeField]
        TextMeshProUGUI m_GoldText;
        [SerializeField]
        ProgressionBar m_ProgressionBar;
        [SerializeField]
        HyperCasualButton m_SettingsButton;
        [SerializeField]
        HyperCasualButton m_CardsButton;
        [SerializeField]
        HyperCasualButton m_AdsFreeButton;
        [SerializeField]
        HyperCasualButton m_DragToMoveOverlay;
        [SerializeField]
        HyperCasualButton m_ButtonLuckySpin;
        [SerializeField]
        AbstractGameEvent m_PauseEvent;

        public ProgressionBar Progression => m_ProgressionBar;

        int m_GoldValue;
        
        /// <summary>
        /// The amount of gold to display on the hud.
        /// The setter method also sets the hud text.
        /// </summary>
        public int GoldValue
        {
            get => m_GoldValue;
            set
            {
                if (m_GoldValue != value)
                {
                    m_GoldValue = value;
                    m_GoldText.text = GoldValue.ToString();
                }
            }
        }

        float m_Progress;
        
        /// <summary>
        /// The amount of XP to display on the hud.
        /// The setter method also sets the hud slider value.
        /// </summary>
        public float Progress
        {
            get => m_Progress;
            set
            {
                if (!Mathf.Approximately(m_Progress, value))
                {
                    m_Progress = value;
                    m_ProgressionBar.Value = m_Progress;
                }
            }
        }

        void OnEnable()
        {
            m_ButtonLuckySpin.AddListener(OnLuckySpinButtonClick);
        }

        void OnDisable()
        {
            m_ButtonLuckySpin.RemoveListener(OnLuckySpinButtonClick);
        }

        void OnLuckySpinButtonClick()
        {
            UIManager.Instance.Show<LuckySpinView>();
        }
    }
}
