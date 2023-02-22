using HyperCasual.Runner;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawnable : Spawnable
{
    const string k_PlayerTag = "Player";


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag(k_PlayerTag))
        {
            PlayerController.Instance.AddCharacter(gameObject);
        }
    }
}
