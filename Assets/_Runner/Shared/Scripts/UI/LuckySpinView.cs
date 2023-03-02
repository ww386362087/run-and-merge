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
    public class LuckySpinView : View
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
        AbstractGameEvent m_SpinEvent;
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

        private void OnEnable()
        {
            ResetLuckySpin();

            //
            m_ButtonClose.AddListener(OnButtonCloseClicked);
            m_ButtonSpinFree.AddListener(OnButtonSpinFreeClicked);
            m_ButtonSpinAds.AddListener(OnButtonSpinAdsClicked);
        }

        private void OnDisable()
        {
            ResetPointerPosition();

            //
            m_ButtonClose.RemoveListener(OnButtonCloseClicked);
            m_ButtonSpinFree.RemoveListener(OnButtonSpinFreeClicked);
            m_ButtonSpinAds.RemoveListener(OnButtonSpinAdsClicked);
        }

        void ResetLuckySpin()
        {
            RandomizePrizePool();
            GetSpinPrizes();
            SetupSpinPrizes();
        }

        void RandomizePrizePool()
        {
            // Get folder.
            // randomize order.
            // add to array.
            //m_PrizeDatas += ;
        }

        void GetSpinPrizes()
        {
            if (m_Prizes == null || m_Prizes.Length == 0)
            {
                m_Prizes = m_PrizeHolder.GetComponentsInChildren<Prize>();
            }
        }

        void SetupSpinPrizes()
        {
            for (int i = 0; i < m_Prizes.Length; i++)
            {
                var prize = m_Prizes[i];
                prize.IntitializeData();
            }
        }

        void ResetPointerPosition()
        {
            var disk = m_PrizeHolder.transform;
            disk.eulerAngles = new Vector3(0, 0, -11.3f);
        }

        #region On Button Click
        void OnButtonSpinFreeClicked()
        {
            ResetPointerPosition();
            Spin();

            //ActivateTimerOverlay();
        }

        void OnButtonSpinAdsClicked()
        {
            //StartCoroutine(ShowAds()); // Show ads here.

            ResetPointerPosition();
            Spin();
        }

        void Spin()
        {
            //Note: the wheel always spin counter-clockwise, figure out a way to spin it clockwise.
            m_SpinEvent.Raise();

            int PrizeIndex = UnityEngine.Random.Range(0, m_Prizes.Length);
            int percentage = 360 / m_Prizes.Length;
            int targetValue = 360 * 4 + percentage * PrizeIndex + percentage / 2;

            m_PrizeHolder.transform.DORotate(new Vector3(0, 0, targetValue), 3f, RotateMode.LocalAxisAdd).SetRelative(true).SetEase(Ease.InOutCubic);

            m_SelectedPrize = m_Prizes[PrizeIndex];

            Debug.Log("Coins won: " + m_SelectedPrize.Quantity); // Data for MAD <3
        }

        void OnButtonCloseClicked()
        {

            UIManager.Instance.ClearHistory();
            m_CloseViewEvent.Raise();
            UIManager.Instance.Show<Hud>();
        }
        #endregion

        #region Context Menu
        [ContextMenu("Reorder")]
        void ReOrderItemReward()
        {
            var offset = (float) -11.3 / 2;
            var per = 360f / m_Prizes.Length;
            var startAngle = 90f + offset;

            for(int i = 0; i < m_Prizes.Length; i++)
            {
                var pos = new Vector2(Mathf.Cos(startAngle*Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad))*m_DistanceFromCenter;
                m_Prizes[i].transform.localPosition = pos;
                //Debug.Log(startAngle);
                startAngle -= per;

                var z_rotation = per * (m_ClockwiseValue * i) + offset;
                m_Prizes[i].transform.localEulerAngles = new Vector3(0, 0, z_rotation);
            }
        }
        #endregion
    }
}
