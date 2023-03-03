using System;
using System.Collections;
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
        [SerializeField]
        GameObject m_ButtonOverlay;

        [Header("Event")]
        [SerializeField]
        AbstractGameEvent m_SpinEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;

        [Header("Spin")]
        [SerializeField]
        Transform m_Disk;
        [SerializeField]
        Prize[] m_Prizes;
        [SerializeField]
        Prize m_SelectedPrize;

        [SerializeField]
        int m_PrizeIndexHistory = 0;
        [SerializeField]
        bool m_Spinned = false;

        //
        int length;
        float percentage;
        readonly float offset = -12f / 2; //because art team gave us shit asset.

        [Header("Context Menu")]
        [SerializeField]
        float m_DistanceFromCenter = 175f;
        #endregion

        private void Awake()
        {
            m_Spinned = false; //To set initial spin values.

            length = m_Prizes.Length;
            percentage = 360f / length;
        }

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
            m_ButtonOverlay.SetActive(false);
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
                m_Prizes = m_Disk.gameObject.GetComponentsInChildren<Prize>();
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
            if (m_Spinned)
            {
                var percentage = 360 / m_Prizes.Length;
                float targetValue = (percentage * m_PrizeIndexHistory) - offset - (percentage / 2);
                m_Disk.eulerAngles = new Vector3(0, 0, targetValue);

                m_Spinned = false;
            }
        }

        #region On Button Click
        void OnButtonSpinFreeClicked()
        {
            StartCoroutine(Spin());

            //ActivateTimerOverlay();
        }

        void OnButtonSpinAdsClicked()
        {
            //StartCoroutine(ShowAds()); // Show ads here.

            StartCoroutine(Spin());
        }

        IEnumerator Spin()
        {
            //Note: the wheel always spin counter-clockwise, figure out a way to spin it clockwise.
            m_ButtonOverlay.SetActive(true);

            m_SpinEvent.Raise();
            
            int PrizeIndex = UnityEngine.Random.Range(0, length);
            float targetValue = 360 * 4 + percentage * (length - m_PrizeIndexHistory) + percentage * PrizeIndex;
            if (!m_Spinned)
            {
                targetValue += percentage / 2;
            }
            //float targetValue = (m_Spinned ? (360 * 3 + percentage * (length - m_PrizeIndexHistory)) : (360 * 4)) + percentage * PrizeIndex + percentage*( m_Spinned ? 0 : 1 / 2);

            m_Disk.DORotate(new Vector3(0, 0, targetValue), 3f, RotateMode.LocalAxisAdd).SetRelative(true).SetEase(Ease.InOutCubic);

            m_PrizeIndexHistory = PrizeIndex;
            m_SelectedPrize = m_Prizes[PrizeIndex];

            var oldCurrency = PlayerPrefs.GetInt("Currency");
            var newCurrency = oldCurrency + m_SelectedPrize.Quantity;
            PlayerPrefs.SetInt("Currency", newCurrency);
            //Debug.LogError("vvvvv" + newCurrency);

            if (!m_Spinned)
                m_Spinned = true;

            //Debug.Log("Coins won: " + m_SelectedPrize.Quantity); // Data for MAD <3
            yield return new WaitForSeconds(3.5f);

            m_ButtonOverlay.SetActive(false);
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
            var startAngle = 90f + offset;

            for(int i = 0; i < m_Prizes.Length; i++)
            {
                var pos = new Vector2(Mathf.Cos(startAngle*Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad))*m_DistanceFromCenter;
                m_Prizes[i].transform.localPosition = pos;
                //Debug.Log(startAngle);
                startAngle -= percentage;

                var z_rotation = percentage * i + offset;
                m_Prizes[i].transform.localEulerAngles = new Vector3(0, 0, z_rotation);
            }
        }
        #endregion
    }
}
