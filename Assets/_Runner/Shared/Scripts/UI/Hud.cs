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
        TextMeshProUGUI m_KeysText;
        [SerializeField]
        Slider m_XpSlider;
        [SerializeField]
        HyperCasualButton m_ButtonPause;
        [SerializeField]
        HyperCasualButton m_ButtonLuckySpin;
        [SerializeField]
        AbstractGameEvent m_PauseEvent;

        /// <summary>
        /// The slider that displays the XP value 
        /// </summary>
        public Slider XpSlider => m_XpSlider;

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

        int m_KeysValue;

        /// <summary>
        /// The amount of gold to display on the hud.
        /// The setter method also sets the hud text.
        /// </summary>
        public int KeysValue
        {
            get => m_KeysValue;
            set
            {
                if (m_KeysValue != value)
                {
                    m_KeysValue = value;
                    m_KeysText.text = KeysValue.ToString();
                }
            }
        }

        float m_XpValue;
        
        /// <summary>
        /// The amount of XP to display on the hud.
        /// The setter method also sets the hud slider value.
        /// </summary>
        public float XpValue
        {
            get => m_XpValue;
            set
            {
                if (!Mathf.Approximately(m_XpValue, value))
                {
                    m_XpValue = value;
                    m_XpSlider.value = m_XpValue;
                }
            }
        }

        void OnEnable()
        {
            m_ButtonPause.AddListener(OnPauseButtonClick);
            m_ButtonLuckySpin.AddListener(OnLuckySpinButtonClick);
        }

        void OnDisable()
        {
            m_ButtonPause.RemoveListener(OnPauseButtonClick);
            m_ButtonLuckySpin.RemoveListener(OnLuckySpinButtonClick);
        }

        void OnPauseButtonClick()
        {
            m_PauseEvent.Raise();
        }

        void OnLuckySpinButtonClick()
        {
            UIManager.Instance.Show<LuckySpinView>();
        }
    }
}
