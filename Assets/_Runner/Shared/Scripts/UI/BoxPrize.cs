using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual.Runner;
using TMPro;

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

        public Prize PrizeBox => m_Prize;

        public void AddEvent(Action newEvent)
        {
            m_ButtonBox.AddListener(newEvent);
        }

        public void RemoveEvent(Action newEvent)
        {
            m_ButtonBox.RemoveListener(newEvent);
        }

        public void OnButtonBoxClicked()
        {
            m_ChestOverlay.SetActive(false);
            m_Prize.AddToInventory();
        }
    }
}
