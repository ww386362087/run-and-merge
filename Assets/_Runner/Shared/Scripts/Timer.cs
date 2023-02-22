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
        [SerializeField]
        float m_CountdownTimeInMinutes = 10;
        float m_CountdownTimeInSeconds;
        [SerializeField]
        TextMeshProUGUI m_CountdownText;
        [SerializeField]
        bool m_IsCountingDown = false;

        private void Start()
        {
            m_CountdownTimeInSeconds = m_CountdownTimeInMinutes * 60;
            m_IsCountingDown = true;
            gameObject.SetActive(m_IsCountingDown);
        }

        void Update()
        {
            if (m_IsCountingDown)
            {
                if (m_CountdownTimeInSeconds > 0)
                {
                    m_CountdownTimeInSeconds -= Time.deltaTime;
                    DisplayTime(m_CountdownTimeInSeconds);
                }
                else
                {
                    m_CountdownTimeInSeconds = 0;
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
