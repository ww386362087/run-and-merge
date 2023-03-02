using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyperCasual.Runner;

public class ProgressionManager : Singleton<ProgressionManager>
{
    public readonly string MERGE_LEVEL_PROGRESSION = "level_general";
    [SerializeField] int levelSet = 13;

    [ContextMenu("progress")]
    public void Progress()
    {
        Debug.Log("runner progress: " + SaveManager.Instance.LevelProgress);
        Debug.Log("merge progress: " + PlayerPrefs.GetInt(MERGE_LEVEL_PROGRESSION));
    }

    int GetMergeLevel()
    {
        return PlayerPrefs.GetInt(MERGE_LEVEL_PROGRESSION);
    }

    int GetRunnerLevel()
    {
        return SaveManager.Instance.LevelProgress;
    }

    void SetMergeLevel(int lv)
    {
        PlayerPrefs.SetInt(MERGE_LEVEL_PROGRESSION, lv);
    }

    void SetRunnerLevel(int lv)
    {
        SaveManager.Instance.LevelProgress = lv;
    }

    public int GetLevel()
    {
        return SaveManager.Instance.LevelProgress;
    }

    public void SetLevel(int lv)
    {
        PlayerPrefs.SetInt(MERGE_LEVEL_PROGRESSION, lv);
        SaveManager.Instance.LevelProgress = lv;
    }

    [ContextMenu("set")]
    public void Set()
    {
        SetLevel(levelSet);
    }
}
