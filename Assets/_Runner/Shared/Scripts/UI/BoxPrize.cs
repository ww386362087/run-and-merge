using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual.Runner;
using TMPro;
using UnityEngine.UI;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// A class used to hold prize data
    /// for Lucky Box minigame.
    /// </summary>
    public class BoxPrize : MonoBehaviour
    {
        [Header("Refference")]
        [SerializeField]
        HyperCasualButton m_ButtonBox;
        [SerializeField]
        Prize m_Prize;
        [SerializeField]
        GameObject m_ChestOverlay;
        [SerializeField]
        Image m_Background;

        public Prize PrizeBox => m_Prize;

        public void IntitializeData()
        {
            m_Prize.IntitializeData();
        }

        public void AddEvent(Action newEvent)
        {
            m_ButtonBox.AddListener(()=> 
            { 
                newEvent?.Invoke();
                RemoveEvent();
            });
        }

        public void RemoveEvent()
        {
            m_ButtonBox.RemoveAllListener();
        }

        public void OnButtonBoxClicked()
        {
            m_ChestOverlay.SetActive(false);
            m_Prize.AddToInventory(m_Prize.Quantity);
        }

        public void ResetBoxState()
        {
            RemoveEvent();

            m_Background.gameObject.SetActive(true);
            m_Prize.gameObject.SetActive(true);
            m_ChestOverlay.SetActive(true);
        }

        public void SetColorBG(Color _color)
        {
            m_Background.color = _color;
        }
    }
}
