using System;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;
using DG.Tweening;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This View contains Lucky Spin minigame functionality.
    /// </summary>
    public class LuckyBoxView : View
    {
        #region Variable Declaration
        [Header("Refference")]
        [SerializeField]
        HyperCasualButton[] m_ButtonPrizeBox;

        [SerializeField]
        HyperCasualButton m_ButtonGetKeys;
        [SerializeField]
        HyperCasualButton m_ButtonContinue;
        [SerializeField]
        HyperCasualButton m_ButtonNext;

        [SerializeField]
        GameObject[] m_Keys;

        [Header("Event")]
        [SerializeField]
        AbstractGameEvent m_BoxEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;

        /*[Header("Stuff")]
        [SerializeField]
        GameObject m_PrizeHolder;
        [SerializeField]
        Prize[] m_Prizes;
        Prize[] m_SelectedPrizes;*/

        /*[Header("Context Menu")]
        [SerializeField]
        float m_DistanceFromCenter = 175f;*/
        #endregion

        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            for (int i = 0; i < m_ButtonPrizeBox.Length; i++)
            {
                m_ButtonPrizeBox[i].AddListener(OnButtonPrizeBoxClicked);
            }

            m_ButtonGetKeys.AddListener(OnButtonGetKeysClicked);
            m_ButtonNext.AddListener(OnButtonNoThanksClicked);
            m_ButtonContinue.AddListener(OnButtonContinueClicked);
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_ButtonPrizeBox.Length; i++)
            {
                m_ButtonPrizeBox[i].RemoveListener(OnButtonPrizeBoxClicked);
            }

            m_ButtonGetKeys.RemoveListener(OnButtonGetKeysClicked);
            m_ButtonGetKeys.RemoveListener(OnButtonNoThanksClicked);
            m_ButtonContinue.RemoveListener(OnButtonContinueClicked);
        }

        void OnButtonPrizeBoxClicked()
        {
            
        }

        void OnButtonGetKeysClicked()
        {
            // Show ads.
            
            SelectBox();
        }

        void OnButtonNoThanksClicked()
        {

        }

        void OnButtonContinueClicked()
        {
            m_CloseViewEvent.Raise();
            UIManager.Instance.GoBack();
        }

        void SelectBox()
        {

        }
    }
}
