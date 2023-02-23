using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using HyperCasual.Gameplay;

public class GameSceneLoad : Singleton<GameSceneLoad>
{
    public List<GameObject> sceneRuns;
    public List<GameObject> sceneMerges;
    public FinishRunEvent evt;

    public Camera mainCam;
    public Transform camTarget;
    public GameObject objMerge;
    public bool isFinishRun = false;

    public void Action_FinishRunGame(int noOfPlayer)
    {
        foreach (var k in sceneRuns)
        {
            if (k != null)
            {
                k.gameObject.SetActive(false);
            }
        }

        foreach (var k in sceneMerges)
            k.gameObject.SetActive(true);


        mainCam.transform.DOMove(camTarget.position,2);
        mainCam.transform.DORotateQuaternion(camTarget.rotation,2);
        isFinishRun = true;

        Debug.Log("number of monster to add: " + noOfPlayer);
        evt.NumberCharacterAdd = noOfPlayer;
        evt.Raise();
    }

    public void SetPositionMergeGame(Vector3 _vt)
    {
        isFinishRun = false;
        objMerge.transform.position = new Vector3(0, 0, _vt.z + 10);
    }
}
