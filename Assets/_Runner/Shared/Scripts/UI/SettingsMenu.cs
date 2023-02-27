using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasual.Runner
{
    /// <summary>
    /// This View contains settings menu functionalities
    /// </summary>
    public class SettingsMenu : View
    {
        [SerializeField]
        HyperCasualButton m_ButtonClose;
        [SerializeField]
        Toggle m_MusicToggle;
        [SerializeField]
        Toggle m_SfxToggle;
        /*[SerializeField]
        Slider m_AudioVolumeSlider;
        [SerializeField]
        Slider m_QualitySlider;*/
        
        void OnEnable()
        {
            m_MusicToggle.isOn = AudioManager.Instance.EnableMusic;
            m_SfxToggle.isOn = AudioManager.Instance.EnableSfx;
            /*m_AudioVolumeSlider.value = AudioManager.Instance.MasterVolume;
            m_QualitySlider.value = QualityManager.Instance.QualityLevel;*/
            
            m_ButtonClose.AddListener(OnButtonCloseClick);
            m_MusicToggle.onValueChanged.AddListener(MusicToggleChanged);
            m_SfxToggle.onValueChanged.AddListener(SfxToggleChanged);
            //m_AudioVolumeSlider.onValueChanged.AddListener(VolumeSliderChanged);
            //m_QualitySlider.onValueChanged.AddListener(QualitySliderChanged);
        }
        
        void OnDisable()
        {
            m_ButtonClose.RemoveListener(OnButtonCloseClick);
            m_MusicToggle.onValueChanged.RemoveListener(MusicToggleChanged);
            m_SfxToggle.onValueChanged.RemoveListener(SfxToggleChanged);
            //m_AudioVolumeSlider.onValueChanged.RemoveListener(VolumeSliderChanged);
            //m_QualitySlider.onValueChanged.RemoveListener(QualitySliderChanged);
        }

        void MusicToggleChanged(bool value)
        {
            AudioManager.Instance.EnableMusic = value;
        }

        void SfxToggleChanged(bool value)
        {
            AudioManager.Instance.EnableSfx = value;
        }

        /*void VolumeSliderChanged(float value)
        {
            AudioManager.Instance.MasterVolume = value;
        }*/
        
        /*void QualitySliderChanged(float value)
        {
            QualityManager.Instance.QualityLevel = (int)value;
        }*/

        void OnButtonCloseClick()
        {
            UIManager.Instance.GoBack();
        }
    }
}
