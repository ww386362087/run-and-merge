using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to count down and display time
    /// </summary>
    public class Timer : MonoBehaviour
    {
        [Header("Countdown time")]
        [SerializeField]
        int m_Minutes = 10;
        [SerializeField]
        int m_Seconds = 0;
        
        float m_CountdownTime;

        [Space]
        [SerializeField]
        TextMeshProUGUI m_CountdownText;
        [SerializeField]
        bool m_IsCountingDown = false;

        private void Start()
        {
            m_CountdownTime = m_Minutes * 60 + m_Seconds;
            m_IsCountingDown = true;
            gameObject.SetActive(m_IsCountingDown);
        }

        void Update()
        {
            if (m_IsCountingDown)
            {
                if (m_CountdownTime > 0)
                {
                    m_CountdownTime -= Time.deltaTime;
                    DisplayTime(m_CountdownTime);
                }
                else
                {
                    m_CountdownTime = 0;
                    m_IsCountingDown = false;
                    gameObject.SetActive(m_IsCountingDown);
                }
            }
        }

        void DisplayTime(float time)
        {
            if (time < 0)
            {
                time = 0;
            }
            else if (time > 0)
            {
                time += 1;
            }

            float minutes = Mathf.FloorToInt(time / 60);
            float seconds = Mathf.FloorToInt(time % 60);

            m_CountdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
