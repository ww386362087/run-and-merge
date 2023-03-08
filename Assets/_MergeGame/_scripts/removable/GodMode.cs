using HyperCasual.Core;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    [SerializeField]
    HyperCasualButton m_QuickPlayButton;
    [SerializeField]
    HyperCasualButton m_BackButton;
    [Space]
    [SerializeField]
    LevelSelectButton m_LevelButtonPrefab;
    [SerializeField]
    RectTransform m_LevelButtonsRoot;
    [SerializeField]
    AbstractGameEvent m_NextLevelEvent;
    [SerializeField]
    AbstractGameEvent m_BackEvent;

    [SerializeField] Button btn_addCoin;
    [SerializeField] Button btn_addMonster;
    [SerializeField] Button btn_addWarrior;
    [SerializeField] InputField monsterLevel;
    [SerializeField] InputField warriorLevel;
    [SerializeField] InputField coin;

    readonly List<LevelSelectButton> m_Buttons = new();

    void Start()
    {
        var levels = SequenceManager.Instance.Levels;
        for (int i = 0; i < levels.Length; i++)
        {
            m_Buttons.Add(Instantiate(m_LevelButtonPrefab, m_LevelButtonsRoot));
        }

        ResetButtonData();
    }

    void OnEnable()
    {
        ResetButtonData();

        //m_QuickPlayButton.AddListener(OnQuickPlayButtonClicked);
        m_BackButton.AddListener(Close);
        btn_addCoin.onClick.AddListener(AddCoin);
        btn_addMonster.onClick.AddListener(AddMonsterTest);
        btn_addWarrior.onClick.AddListener(AddWarriorTest);

        //OnQuickPlayButtonClicked();
    }

    void OnDisable()
    {
        //m_QuickPlayButton.RemoveListener(OnQuickPlayButtonClicked);
        m_BackButton.RemoveListener(Close);
        btn_addCoin.onClick.RemoveListener(AddCoin);
        btn_addMonster.onClick.RemoveListener(AddMonsterTest);
        btn_addWarrior.onClick.RemoveListener(AddWarriorTest);
    }

    void ResetButtonData()
    {
        var levelProgress = SaveManager.Instance.LevelProgress;
        for (int i = 0; i < m_Buttons.Count; i++)
        {
            var button = m_Buttons[i];
            button.SetData(i, true, OnClickBtn);
        }
    }

    void OnClickBtn(int startingIndex)
    {
        if (startingIndex < 0)
            throw new Exception("Button is not initialized");

        SequenceManager.Instance.SetStartingLevel(startingIndex);
        ProgressionManager.Instance.SetLevel(startingIndex);
        m_NextLevelEvent.Raise();

        Close();
        SceneManager.LoadScene("RunnerLevel");
    }

    void OnClickQuickplay(int startingIndex)
    {
        if (startingIndex < 0)
            throw new Exception("Button is not initialized");

        SequenceManager.Instance.SetStartingLevel(startingIndex);
        ProgressionManager.Instance.SetLevel(startingIndex);
        m_NextLevelEvent.Raise();
    }

    void OnQuickPlayButtonClicked()
    {
        m_BackEvent.Raise();
        OnClickQuickplay(SaveManager.Instance.LevelProgress);
    }

    void Close()
    {
        //m_BackEvent.Raise();
        transform.parent.gameObject.SetActive(false);
    }

    void AddMonsterTest()
    {
        GameController.Instance.AddMonsterToTest(Int32.Parse(monsterLevel.text));
    }

    void AddWarriorTest()
    {
        GameController.Instance.AddWarriorToTest(Int32.Parse(warriorLevel.text));
    }

    void AddCoin()
    {
        UiManager.instance.increase_money(Int32.Parse(coin.text));
    }
}
