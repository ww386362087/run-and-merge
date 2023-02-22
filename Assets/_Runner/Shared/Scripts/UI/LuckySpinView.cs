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
        [SerializeField]
        HyperCasualButton m_ButtonClose;
        [SerializeField]
        HyperCasualButton m_ButtonSpinFree;
        [SerializeField]
        HyperCasualButton m_ButtonSpinAds;
        [SerializeField]
        GameObject m_Countdown;

        [SerializeField]
        AbstractGameEvent m_SpinEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;

        [SerializeField]
        GameObject m_PrizeHolder;
        [SerializeField]
        Prize[] m_Prizes;
        Prize m_SelectedPrize;

        //Quaternion m_WheelQuaternion;

        [SerializeField]
        float m_space = 100f;
        #endregion

        private void Awake()
        {
            m_Prizes = m_PrizeHolder.GetComponentsInChildren<Prize>();
            //m_WheelQuaternion = m_PrizeHolder.GetComponent<Quaternion>();
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
            m_SpinEvent.Raise();

            int PrizeIndex = UnityEngine.Random.Range(1, m_Prizes.Length);
            int percentage = 360 / m_Prizes.Length;
            int targetValue = 360 * 4 + percentage * PrizeIndex;

            m_PrizeHolder.transform.DORotate(new Vector3(0, 0, targetValue), 3f, RotateMode.FastBeyond360).SetEase(Ease.InOutCubic);

            m_SelectedPrize = m_Prizes[PrizeIndex];
        }

        void OnButtonCloseClicked()
        {
            m_CloseViewEvent.Raise();
            UIManager.Instance.GoBack();
        }

        [ContextMenu("Reorder")]
        void ReOrderItemReward()
        {
            var per = 360f / m_Prizes.Length;
            var startAngle = 90f;

            for(int i = 0; i < m_Prizes.Length; i++)
            {
                var pos = new Vector2(Mathf.Cos(startAngle*Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad))*m_space;
                m_Prizes[i].transform.localPosition = pos;
                //Debug.Log(startAngle);
                startAngle -= per;
            }
        }
    }
}
