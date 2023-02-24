using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HyperCasual.Gameplay;
using HyperCasual.Core;

public class GameSceneLoad : Singleton<GameSceneLoad>
{
    [SerializeField] AbstractGameEvent m_NextLevelEvent;
    public List<GameObject> sceneRuns;
    public List<GameObject> sceneMerges;
    public FinishRunEvent evt;

    public Camera mainCam;
    public Transform camTarget;
    public GameObject objMerge;
    public bool isFinishRun = false;

    public void Action_FinishRunGame(int noOfPlayer)
    {
        SetSceneRuns(false);

        SetSceneMerges(true);


        mainCam.transform.DOMove(camTarget.position,2);
        mainCam.transform.DORotateQuaternion(camTarget.rotation,2);
        isFinishRun = true;

        evt.NumberCharacterAdd = noOfPlayer;
        evt.Raise();
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
            k.gameObject.SetActive(set);
    }

    public void SetPositionMergeGame(Vector3 _vt)
    {
        isFinishRun = false;
        objMerge.transform.position = new Vector3(0, 0, _vt.z + 10);
    }
}
