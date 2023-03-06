using DG.Tweening;

using HyperCasual.Runner;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEnemySpawnable : Spawnable, ISwitchable
{
    [SerializeField]
    SoundID m_Sound = SoundID.None;

    [SerializeField]
    GameObject enemy;

    bool m_active = false;

    public void Active()
    {
        if (m_active)
            return;
        m_active = true;

        var character = PlayerController.Instance.GetClosest(enemy.transform.position);
        character.transform.SetParent(null);
        character.transform.DOMove(enemy.transform.position, .25f).OnComplete(() =>
        {
            AudioManager.Instance.PlayEffect(m_Sound);
            Destroy(character);
            Destroy(gameObject);
        }).SetEase(Ease.Linear);
    }
}
