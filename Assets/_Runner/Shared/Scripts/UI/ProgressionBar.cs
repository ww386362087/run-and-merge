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
        [SerializeField] private Color m_DarkerGreen = new Color(76, 182, 22, 255);
        [SerializeField] private Color m_LighterGreen = new Color(129, 234, 34, 255);
        [SerializeField] private Color m_Origin = new Color(188, 197, 184, 255);

        private void Update()
        {
            //Debug.Log("value: " + Value + " max: " + MaxValue + " min: " + MinValue);

            UpdateProgressBar();
        }

        void UpdateProgressBar()
        {
            float tenthOfLength = MaxValue / 10;

            int progress = (int)(Mathf.Ceil(Value / tenthOfLength));

            for (int i = 0; i < m_ProgressBars.Length; i++)
            {
                if (i < progress - 1)
                {
                    m_ProgressBars[i].color = m_DarkerGreen;
                }
                else if (i == progress - 1)
                {
                    m_ProgressBars[i].color = m_LighterGreen;
                }
                else
                {
                    m_ProgressBars[i].color = m_Origin;
                }
            }
        }
    }
}