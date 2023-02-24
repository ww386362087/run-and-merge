using System;
using System.Collections.Generic;
using HyperCasual.Core;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// This View contains Lucky Box minigame functionality.
    /// </summary>
    public class LuckyBoxView : View
    {
        #region Variable Declaration
        [Header("Refference")]
        [SerializeField]
        GameObject m_BoxPrizeHolder;
        [SerializeField]
        BoxPrize[] m_BoxPrizes;
        [SerializeField]
        TextMeshProUGUI m_MaxPrice;

        [SerializeField]
        HyperCasualButton m_ButtonGetKeys;
        [SerializeField]
        HyperCasualButton m_ButtonContinue;
        [SerializeField]
        HyperCasualButton m_ButtonNext;

        [SerializeField]
        GameObject[] m_ActiveKeys;
        [SerializeField]
        int m_KeyCount = 3;
        [SerializeField]
        int m_AdsWatchedTimes = 0;

        [Header("Event")]
        [SerializeField]
        AbstractGameEvent m_BoxEvent;
        [SerializeField]
        AbstractGameEvent m_CloseViewEvent;
        #endregion

        private void Awake()
        {
            // Check at end of a run, when the player wins, if the player has 3 keys in inventory then trigger lucky box window -> Player must use all 3 keys
            //before they can continue with the game.

            //TODO:
            // 1. Add overlay to prevent players from opening boxes while they have no keys left.
            // 2. Modify level win event to check for total keys in SaveManager, if equals to 3 -> activate LuckyBoxWindow.

            DefaultState();
        }

        private void OnEnable()
        {
            GetBoxPrizes();
            SetupBoxPrizes();
            LoadUITextData();

            ResetActiveKeys();
            ResetAdsWatchTime();

            //
            m_ButtonGetKeys.AddListener(OnButtonGetKeysClicked);
            m_ButtonNext.AddListener(OnButtonNextClicked);
            m_ButtonContinue.AddListener(OnButtonContinueClicked);
        }

        private void OnDisable()
        {
            m_ButtonGetKeys.RemoveListener(OnButtonGetKeysClicked);
            m_ButtonNext.RemoveListener(OnButtonNextClicked);
            m_ButtonContinue.RemoveListener(OnButtonContinueClicked);
        }

        void GetBoxPrizes()
        {
            m_BoxPrizes = m_BoxPrizeHolder.GetComponentsInChildren<BoxPrize>();
        }

        void SetupBoxPrizes()
        {
            for (int i = 0; i < m_BoxPrizes.Length; i++)
            {
                var box = m_BoxPrizes[i];
                box.AddEvent(() => 
                {
                    box.OnButtonBoxClicked();
                    InactivateKey();
                    
                    if (m_KeyCount <= 0)
                    {
                        m_KeyCount = 0;

                        NoKeysLeftState();
                    }

                    if (m_KeyCount <= 0 && m_AdsWatchedTimes >= 2)
                    {
                        m_AdsWatchedTimes = 2;
                        m_KeyCount = 0;

                        NoChestLeftState();
                    }
                });
            }
        }

        void InactivateKey()
        {
            m_ActiveKeys[m_KeyCount - 1].SetActive(false);
            m_KeyCount -= 1;
        }

        void LoadUITextData()
        {
            int sum = 0;
            for (int i = 0; i < m_BoxPrizes.Length; i++)
            {
                var box = m_BoxPrizes[i];
                sum += box.PrizeBox.Quantity;
            }
            m_MaxPrice.text = sum.ToString();
        }

        void ResetActiveKeys()
        {
            m_KeyCount = 3;
            
            foreach (var k in m_ActiveKeys)
            {
                k.SetActive(true);
            }
        }

        void ResetAdsWatchTime()
        {
            m_AdsWatchedTimes = 0;
        }

        #region Button Onclick events
        void OnButtonGetKeysClicked()
        {
            // Show ads here.

            m_AdsWatchedTimes += 1;
            ResetActiveKeys();
            DefaultState();
        }

        void OnButtonNextClicked()
        {
            CloseLuckyBox();
        }

        void OnButtonContinueClicked()
        {
            CloseLuckyBox();
        }

        void CloseLuckyBox()
        {
            m_CloseViewEvent.Raise();

            SaveManager.Instance.Keys = 0;
            UIManager.Instance.GoBack();
        }
        #endregion

        #region Button active states
        void NoChestLeftState()
        {
            SetButtonState(false, true);
        }

        void NoKeysLeftState()
        {
            SetButtonState(true, false);
        }

        void DefaultState()
        {
            SetButtonState(false, false);
        }

        void SetButtonState(bool _otherBtns, bool _continueBtn)
        {
            m_ButtonGetKeys.gameObject.SetActive(_otherBtns);
            m_ButtonNext.gameObject.SetActive(_otherBtns);

            m_ButtonContinue.gameObject.SetActive(_continueBtn);
        }
        #endregion
    }
}
