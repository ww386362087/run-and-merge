using HyperCasual.Core;
using HyperCasual.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour, IGameEventListener
{
    public LevelCompletedEvent evt;

    private void OnEnable()
    {
        evt.AddListener(this);
    }

    private void OnDisable()
    {
        evt.RemoveListener(this);
    }

    public void OnEventRaised()
    {
        StartCoroutine(WaitBeforeStart());
    }

    IEnumerator WaitBeforeStart()
    {
        yield return null;
        yield return null;
        yield return null;

        gameObject.SetActive(false);
    }
}
