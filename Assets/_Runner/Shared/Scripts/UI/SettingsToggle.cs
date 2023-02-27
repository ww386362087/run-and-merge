using UnityEngine;
using UnityEngine.UI;

public class SettingsToggle : MonoBehaviour
{
    [SerializeField] RectTransform m_HandleRectTransform;
    [SerializeField] Toggle m_Toggle;
    Vector2 m_HandlePosition;

    private void Awake()
    {
        Defaults();

        //
        m_Toggle.onValueChanged.AddListener(OnToggle);
    }
    private void OnDestroy()
    {
        m_Toggle.onValueChanged.RemoveListener(OnToggle);
    }

    void Defaults()
    {
        m_HandlePosition = m_HandleRectTransform.anchoredPosition;

        if (m_Toggle.isOn)
            OnToggle(true);
        else
            OnToggle(false);
    }

    void OnToggle(bool _isOn)
    {
        m_HandleRectTransform.anchoredPosition = _isOn ? m_HandlePosition : m_HandlePosition * -1;
    }
}
