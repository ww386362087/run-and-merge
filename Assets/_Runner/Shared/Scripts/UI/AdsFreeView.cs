using HyperCasual.Core;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains asdgfasdg functionalities
    /// </summary>
    public class AdsFreeView : View
    {
        [SerializeField]
        HyperCasualButton m_ButtonClose;
        [SerializeField]
        HyperCasualButton m_ButtonRemoveAds;

        private void OnEnable()
        {
            m_ButtonClose.AddListener(OnButtonCloseClick);
            m_ButtonRemoveAds.AddListener(OnButtonRemoveAdsClick);
        }

        private void OnDisable()
        {
            m_ButtonClose.RemoveListener(OnButtonCloseClick);
            m_ButtonRemoveAds.RemoveListener(OnButtonRemoveAdsClick);
        }

        void OnButtonCloseClick()
        {
            UIManager.Instance.ClearHistory();
            m_CloseViewEvent.Raise();
            UIManager.Instance.Show<Hud>();
        }

        void OnButtonRemoveAdsClick()
        {
            // FOR MAD. DO STH HERE
        }
    }
}
