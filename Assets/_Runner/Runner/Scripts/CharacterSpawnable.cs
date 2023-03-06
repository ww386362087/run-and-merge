using HyperCasual.Runner;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnable : Spawnable
{
    const string k_PlayerTag = "Player";

    [SerializeField]
    SoundID m_Sound = SoundID.None;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(k_PlayerTag))
        {
            AudioManager.Instance.PlayEffect(m_Sound);
            PlayerController.Instance.AddCharacter(gameObject);
        }
    }
}
