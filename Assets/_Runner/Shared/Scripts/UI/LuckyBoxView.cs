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
        HyperCasualButton m_ButtonClose;
        [SerializeField]
        HyperCasualButton m_ButtonSpinFree;
        [SerializeField]
        HyperCasualButton m_ButtonSpinAds;
        [SerializeField]
        GameObject m_Countdown;

        [Header("Event")]
        [SerializeField]
        AbstractGameEvent m_BoxEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;

        [Header("Stuff")]
        [SerializeField]
        GameObject m_PrizeHolder;
        [SerializeField]
        Prize[] m_Prizes;
        Prize m_SelectedPrize;

        [SerializeField]
        bool m_SpinClockwise = true;
        const int m_ClockwiseValue = -1;

        [Header("Context Menu")]
        [SerializeField]
        float m_DistanceFromCenter = 175f;
        #endregion

        private void Awake()
        {
            m_Prizes = m_PrizeHolder.GetComponentsInChildren<Prize>();
        }

        private void OnEnable()
        {
            m_ButtonClose.AddListener(OnButtonCloseClicked);
            m_ButtonSpinFree.AddListener(OnButtonSpinFreeClicked);
            m_ButtonSpinAds.AddListener(OnButtonSpinAdsClicked);
        }

        private void OnDisable()
        {
            m_ButtonClose.AddListener(OnButtonCloseClicked);
            m_ButtonSpinFree.RemoveListener(OnButtonSpinFreeClicked);
            m_ButtonSpinAds.RemoveListener(OnButtonSpinAdsClicked);
        }

        void OnButtonSpinFreeClicked()
        {
            Spin();
        }

        void OnButtonSpinAdsClicked()
        {
            // Show ads.
            
            Spin();
        }

        void Spin()
        {
            m_BoxEvent.Raise();

            int PrizeIndex = UnityEngine.Random.Range(0, m_Prizes.Length);
            int percentage = 360 / m_Prizes.Length;
            int targetValue = 360 * 4 + percentage * PrizeIndex;

            m_PrizeHolder.transform.DORotate(new Vector3(0, 0, targetValue), 3f, RotateMode.LocalAxisAdd).SetRelative(true).SetEase(Ease.InOutCubic);

            m_SelectedPrize = m_Prizes[PrizeIndex];
        }

        void OnButtonCloseClicked()
        {
            m_CloseViewEvent.Raise();
            UIManager.Instance.GoBack();
        }
    }
}
