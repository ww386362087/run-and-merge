using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NekoIdleAnimRandomizer : MonoBehaviour
{
    [SerializeField] Warrior warriorCon;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetFloat("IdleParam", Random.Range(0f, .3f));

        //StartCoroutine(RandomizeIdleAnim());
    }

    IEnumerator RandomizeIdleAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            anim.SetFloat("IdleParam", Random.Range(0f, 1f));
        }
    }
    
    public void ActiveArrow()
    {
        //warriorCon.active_arrow();
    }

    public void FireArrow()
    {
        warriorCon.fight_arrow();
    }
}