using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace HyperCasual.Gameplay
{
    /// <summary>
    /// A class used to hold prize data
    /// for minigames.
    /// </summary>
    public class Prize : MonoBehaviour
    {
        [SerializeField]
        PrizeType m_PrizeType = PrizeType.Coin;
        [SerializeField]
        int m_Quantity;
        TextMeshProUGUI m_TextQuantity;

        enum PrizeType
        {
            Coin,
            MeleeNeko,
            RangeNeko
        }

        public int Quantity => m_Quantity;

        private void Awake()
        {
            m_TextQuantity = GetComponentInChildren<TextMeshProUGUI>();
            m_TextQuantity.text = m_Quantity.ToString();
        }

        //TODO: write function to add prize to inventory.
        public void AddToInventory()
        {

        }
    }
}