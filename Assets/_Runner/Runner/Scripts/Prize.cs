using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyperCasual.Runner
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
        int m_Value;

        enum PrizeType
        {
            Coin,
            MeleeNeko,
            RangeNeko
        }

        public int Value => m_Value;

        // write function to add prize to inventory.
    }
}