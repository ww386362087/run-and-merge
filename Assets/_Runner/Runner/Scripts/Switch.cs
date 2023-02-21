using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] ISwitchable target;

    const string k_PlayerTag = "Player";

    private void Start()
    {
        target = gameObject.GetComponentInParent<ISwitchable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(k_PlayerTag))
        {
            target?.Active();
        }
    }
}
