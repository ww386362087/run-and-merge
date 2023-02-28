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
    /// This View contains Lucky Box minigame functionality. Check at 
    /// end of a run, when the player wins, if the player has 3 keys in
    /// inventory then trigger lucky box window -> Player must use all 
    /// 3 keys before they can continue with the game.
    /// 
    /// TODO: Modify level win event to check for total keys in SaveManager, if equals to 3 -> activate LuckyBoxWindow.
    /// </summary>
    public class LuckyBoxView : View
    {
        #region Variable Declaration
        [Header("Refference")]
        [SerializeField] private TextMeshProUGUI m_MaxPrice;
        [SerializeField] private GameObject m_BoxPrizeHolder;
        [SerializeField] private BoxPrize[] m_BoxPrizes;
        [SerializeField] private GameObject m_Overlay;
        [Space]

        [SerializeField] private HyperCasualButton m_ButtonGetKeys;
        [SerializeField] private HyperCasualButton m_ButtonContinue;
        [SerializeField] private HyperCasualButton m_ButtonNext;
        [Space]

        [SerializeField] private GameObject[] m_ActiveKeys;
        [SerializeField] private int m_KeyCount = 3;
        [SerializeField] private bool m_AdsWatched = false;
        int m_PrizePoolSum = 0;
        PrizeData[] m_PrizeDatas;
        [Space]

        [Header("Event")]
        [SerializeField] private AbstractGameEvent m_BoxEvent;
        [SerializeField] private AbstractGameEvent m_CloseViewEvent;
        [Space]

        [Header("Color")]
        [SerializeField] private Color m_DarkerGreen = new Color(158, 202, 142, 255);
        [SerializeField] private Color m_LighterGreen = new Color(185, 224, 165, 255);
        #endregion

        private void OnEnable()
        {
            m_AdsWatched = false;
            ResetLuckyBox();

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

        void ResetLuckyBox()
        {
            m_PrizePoolSum = 0;
            RandomizePrizePool();
            GetBoxPrizes();
            SetupBoxPrizes();
            LoadUITextData();
            SetOverlay(false);
            DefaultState();
            ResetActiveKeys();
        }

        void RandomizePrizePool()
        {
            // Get folder.
            // randomize order.
            // add to array.
            //m_PrizeDatas += ;
        }

        void GetBoxPrizes()
        {
            if (m_BoxPrizes == null || m_BoxPrizes.Length == 0)
            {
                m_BoxPrizes = m_BoxPrizeHolder.GetComponentsInChildren<BoxPrize>();
            }
        }

        void SetupBoxPrizes()
        {
            for (int i = 0; i < m_BoxPrizes.Length; i++)
            {
                var box = m_BoxPrizes[i];
                box.IntitializeData();
                box.ResetBoxState();
                box.AddEvent(() => 
                {
                    box.OnButtonBoxClicked();
                    InactivateKey();

                    if (m_KeyCount == 0)
                    {
                        SetOverlay(true);
                        NoKeysLeftState();
                    }
                    else if (m_KeyCount < 0)
                        Debug.LogError("Key Count can't go below 0!");

                    if (m_KeyCount == 0 && m_AdsWatched)
                    {
                        SetOverlay(true);
                        AdsWatchedState();
                    }
                });

                //
                m_PrizePoolSum += box.PrizeBox.Quantity;
            }
        }

        void InactivateKey()
        {
            m_ActiveKeys[m_KeyCount - 1].SetActive(false);
            m_KeyCount -= 1;
        }

        void SetOverlay(bool _state)
        {
            m_Overlay.SetActive(_state);
        }

        void LoadUITextData()
        {
            m_MaxPrice.text = m_PrizePoolSum.ToString();
        }

        void ResetActiveKeys()
        {
            m_KeyCount = 3;
            
            foreach (var k in m_ActiveKeys)
            {
                k.SetActive(true);
            }
        }

        #region Button Onclick events
        void OnButtonGetKeysClicked()
        {
            //StartCoroutine(ShowAds()); // Show ads here.

            m_AdsWatched = true;

            ResetLuckyBox();
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
        void AdsWatchedState()
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

        #region Context Menu
        [ContextMenu("Change bg so le")]
        void ChangeColorBG()
        {
            for (int i = 0; i < m_BoxPrizes.Length; i++)
            {
                m_BoxPrizes[i].SetColorBG(i % 2 == 0 ? m_DarkerGreen : m_LighterGreen);
            }
        }
        #endregion
    }
}
