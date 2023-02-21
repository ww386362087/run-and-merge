using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class GameSceneLoad : Singleton<GameSceneLoad>
{
    public List<GameObject> sceneRuns;
    public List<GameObject> sceneMerges;

    public Camera mainCam;
    public Transform camTarget;
    public GameObject objMerge;

    public void Action_FinishRunGame()
    {
        foreach (var k in sceneRuns)
            k.gameObject.SetActive(false);

        foreach (var k in sceneMerges)
            k.gameObject.SetActive(true);


        mainCam.transform.DOMove(camTarget.position,2);
        mainCam.transform.DORotateQuaternion(camTarget.rotation,2);

    }

    public void SetPositionMergeGame(Vector3 _vt)
    {
        objMerge.transform.position = new Vector3(0, 0, _vt.z + 10);
    }



}
