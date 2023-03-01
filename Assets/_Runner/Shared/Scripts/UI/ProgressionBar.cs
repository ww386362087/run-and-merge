using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Gameplay
{
    public class ProgressionBar : MonoBehaviour
    {
        [SerializeField]
        Image[] m_ProgressBars;
        public float Value;
        public float MinValue;
        public float MaxValue;

        [Header("Color")]
        [SerializeField] private Color m_DarkerGreen = new Color(77, 183, 22, 255);
        [SerializeField] private Color m_LighterGreen = new Color(129, 235, 35, 255);
    }
}