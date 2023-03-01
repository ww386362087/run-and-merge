using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HyperCasual.Gameplay;
using HyperCasual.Core;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;

public class GameSceneLoad : Singleton<GameSceneLoad>
{
    [SerializeField] AbstractGameEvent m_NextLevelEvent;
    public List<GameObject> sceneRuns;
    public List<GameObject> sceneMerges;
    public GameObject currentMergeGameObj;
    public GameObject MergeGamePref;
    public FinishRunEvent evt;

    public Camera mainCam;
    public Transform camTarget;
    public bool isFinishRun = false;
    public bool isPlaying = false;

    Vector3 posToSet;

    public void Action_FinishRunGame(int noOfPlayer)
    {
        SetSceneRuns(false);

        SetSceneMerges(true);
        RemoveFog();

        mainCam.transform.DOMove(camTarget.position,2);
        mainCam.transform.DORotateQuaternion(camTarget.rotation,2);
        isFinishRun = true;

        evt.NumberCharacterAdd = noOfPlayer;
        evt.Raise();
    }

    private void RemoveFog()
    {
        DOVirtual.Float(RenderSettings.fogDensity, 0f, 2f, value =>
        {
            RenderSettings.fogDensity = value;
        }).OnComplete(() => {
            RenderSettings.fogDensity = 0f;
            DynamicGI.UpdateEnvironment();
        });
    }

    public void SetSceneRuns(bool set)
    {
        foreach (var k in sceneRuns)
        {
            if (k != null)
            {
                k.gameObject.SetActive(set);
            }
        }
    }

    public void SetSceneMerges(bool set)
    {
        foreach (var k in sceneMerges)
        {
            if (k != null)
            {
                k.gameObject.SetActive(set);
            }
        }
    }

    public void SetPositionMergeGame(Vector3 _vt)
    {
        isFinishRun = false;
        if (currentMergeGameObj != null)
        {
            currentMergeGameObj.transform.position = new Vector3(0, .61f, _vt.z + 20f);
        }

        posToSet = _vt;
    }

    public void RestartMergeGameObj()
    {
        Destroy(currentMergeGameObj);
        isPlaying = false;
        DOVirtual.DelayedCall(0.1f, () =>
        {
            currentMergeGameObj = Instantiate(MergeGamePref, transform);
            SetPositionMergeGame(new Vector3(0, 0, posToSet.z));
        });
    }

    public void SetCamTarget(Camera cam)
    {
        camTarget = cam.transform;
    }

    public void SetMissingRefOnRestartMergeGame(List<GameObject> objs)
    {
        sceneMerges = objs;
    }

    public void SetGameIsPlaying(bool playing)
    {
        isPlaying = playing;
    }
}
