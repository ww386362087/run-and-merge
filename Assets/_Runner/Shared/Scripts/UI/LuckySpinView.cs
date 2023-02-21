using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This View contains Lucky Spin minigame functionality.
    /// </summary>
    public class LuckySpinView : View
    {
        #region Variable Declaration
        [SerializeField] HyperCasualButton m_ButtonClose;
        [SerializeField] HyperCasualButton m_ButtonSpinFree;
        [SerializeField] HyperCasualButton m_ButtonSpinAds;

        [SerializeField] AbstractGameEvent m_BackEvent;
        #endregion

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

            Spin();
        }

        void Spin(/*bool _spinable*/)
        {

        }

        void OnButtonCloseClicked()
        {
            m_BackEvent.Raise();
        }
    }
}
