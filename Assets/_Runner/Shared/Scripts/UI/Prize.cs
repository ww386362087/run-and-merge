using System;
using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine.UI;
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
        PrizeData m_PrizeData;
        [SerializeField]
        PrizeType m_PrizeType = PrizeType.Coin;
        [SerializeField]
        Image m_PrizeImage;
        [SerializeField]
        int m_Quantity;
        [SerializeField]
        TextMeshProUGUI m_TextQuantity;

        public enum PrizeType
        {
            Coin,
            MeleeNeko,
            RangeNeko
        }

        public Image Sprite => m_PrizeImage; 
        public int Quantity => m_Quantity;

        public void IntitializeData()
        {
            m_PrizeImage.sprite = m_PrizeData.Sprite;
            m_Quantity = m_PrizeData.Quantity;

            m_TextQuantity.text = m_Quantity.ToString();
        }

        //TODO: write function to add prize to inventory.
        public void AddToInventory(int _quantity)
        {
            switch (m_PrizeType)
            {
                case PrizeType.Coin:
                    AddCoinToInventory(_quantity);
                    break;
                case PrizeType.MeleeNeko:
                    AddMeleeNekoToInventory(_quantity);
                    break;
                case PrizeType.RangeNeko:
                    AddRangeNekoToInventory(_quantity);
                    break;
            }
        }

        void AddCoinToInventory(int _quantity)
        {
            
        }

        void AddMeleeNekoToInventory(int _quantity)
        {

        }
        void AddRangeNekoToInventory(int _quantity)
        {

        }
    }
}