using System;
using System.Collections;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
        GameObject m_CountdownOverlay;
        [SerializeField]
        TextMeshProUGUI m_CountdownText;
        [SerializeField]
        GameObject m_ButtonOverlay;

        [Header("Event")]
        [SerializeField]
        AbstractGameEvent m_SpinEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;

        [Header("Countdown")]
        TimeSpan m_CountdownTime = new TimeSpan(0, 0, 10, 0);
        [SerializeField]
        bool m_CountingDown = false;

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

        #region Main
        private void Awake()
        {
            // Continue countdown if available.
            if (PlayerPrefs.HasKey("CountdownTime"))
            {
                m_CountingDown = true;

                var savedTime = PlayerPrefs.GetInt("CountdownTime");
                var timer = new TimeSpan(0, 0, (int)savedTime / 60, savedTime % 60);
                TimerCountdown.StartCountDown(timer);
            }

            m_Spinned = false; //To set initial spin values.
            length = m_Prizes.Length;
            percentage = 360f / length;
        }

        /*private void OnApplicationPause()
        {
            var countdownTime = (int)TimerCountdown.TimeLeft.TotalSeconds;
            if (countdownTime > 0)
            {
                PlayerPrefs.SetInt("CountdownTime", countdownTime);
            }
        }*/

#if UNITY_EDITOR
        private void OnApplicationQuit()
        {
            var countdownTime = TimerCountdown.TimeLeft.TotalSeconds;
            if (countdownTime > 0)
            {
                PlayerPrefs.SetInt("CountdownTime", (int)countdownTime);
            }
        }
#endif

        private void Update()
        {
            if (m_CountingDown)
            {
                var outputString = $"{(int)TimerCountdown.TimeLeft.TotalMinutes}:{TimerCountdown.TimeLeft.Seconds:00}";
                m_CountdownText.text = outputString;

                if (TimerCountdown.TimeLeft == TimeSpan.Zero)
                {
                    m_CountingDown = false;
                    m_CountdownOverlay.SetActive(m_CountingDown);
                    PlayerPrefs.DeleteKey("CountdownTime");
                }
            }
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

        //=========================================================================================================


        void ResetLuckySpin()
        {
            RandomizePrizePool();
            GetSpinPrizes();
            SetupSpinPrizes();
            SetButtonOverlayState(false);
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
                float targetValue = (percentage * m_PrizeIndexHistory) - offset - (percentage / 2);
                m_Disk.eulerAngles = new Vector3(0, 0, targetValue);

                m_Spinned = false;
            }
        }
        #endregion

        #region On Button Click
        void OnButtonSpinFreeClicked()
        {
            StartCoroutine(Spin());
            m_CountdownOverlay.SetActive(true);
            TimerCountdown.StartCountDown(m_CountdownTime);
            m_CountingDown = true;
        }

        void OnButtonSpinAdsClicked()
        {
            //StartCoroutine(ShowAds()); // Show ads here.
            AdsMAXManager.Instance.ShowRewardedAd(() => { StartCoroutine(Spin()); },"spin");
          
        }

        void OnButtonCloseClicked()
        {

            UIManager.Instance.ClearHistory();
            m_CloseViewEvent.Raise();
            UIManager.Instance.Show<Hud>();
        }

        IEnumerator Spin()
        {
            //Note: the wheel always spin counter-clockwise, figure out a way to spin it clockwise.
            SetButtonOverlayState(true);

            m_SpinEvent.Raise();

            // Rotate.
            int PrizeIndex = UnityEngine.Random.Range(0, length);
            var newPrize = percentage * PrizeIndex;
            var previousPrize = percentage * (length - m_PrizeIndexHistory);
            var fourRotations = 360 * 4; // for visual effect.
            float targetValue = fourRotations + previousPrize + newPrize;
            if (!m_Spinned)
            {
                targetValue += percentage / 2;
            }
            m_Disk.DORotate(new Vector3(0, 0, targetValue), 3f, RotateMode.LocalAxisAdd).SetRelative(true).SetEase(Ease.InOutCubic);

            // Save values.
            m_PrizeIndexHistory = PrizeIndex;
            m_SelectedPrize = m_Prizes[PrizeIndex];
            //Debug.Log("Coins won: " + m_SelectedPrize.Quantity); // Data for MAD <3
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + m_SelectedPrize.Quantity);
            if (!m_Spinned)
                m_Spinned = true;

            yield return new WaitForSeconds(3.5f);
            SetButtonOverlayState(false);
        }

        void SetButtonOverlayState(bool _state)
        {
            m_ButtonOverlay.SetActive(_state);
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
