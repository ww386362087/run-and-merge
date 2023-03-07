using HyperCasual.Core;
using HyperCasual.Gameplay;
using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpinPrize : MonoBehaviour
{
    [SerializeField] AbstractGameEvent m_NextLevelEvent;
    public RectTransform arrow;
    public GameObject no_thanks_btn;
    public float speed_arrow;
    public float min_dist_arrow, max_dist_arrow;
    public TMPro.TextMeshProUGUI coin_text;
    Vector2 direction;
    public bool active;
    public int total_earning;
    public int multi;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.right;
       
        // active button no thanks
        StartCoroutine(no_thanks_wait());
    }

    public void OnEnable()
    {
        AdsMAXManager.Instance.ShowInterstitial();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            arrow_spin();
            multi_number_spin();
        }
    }

    public void arrow_spin()
    {
        float inc = speed_arrow * Time.deltaTime;

        Vector3 tmp = arrow.localPosition;
        if(direction == Vector2.right)
        {
            tmp.x += inc;
        }
        else
        {
            tmp.x -= inc;
        }
        if(tmp.x >= max_dist_arrow)
        {
            direction = Vector2.left;
        }
        else if (tmp.x <= min_dist_arrow)
        {
            direction = Vector2.right;
        }


        tmp.x = Mathf.Clamp(tmp.x, min_dist_arrow, max_dist_arrow);

        arrow.localPosition = tmp;
    }

    public void multi_number_spin()
    {
        total_earning = UiManager.instance.total_coin_in_level_merge + Inventory.Instance.TempGold;

        if (arrow.localPosition.x >= -450f && arrow.localPosition.x < -290f)
        {
            coin_text.text = (total_earning * 2).ToString() ;
            multi = 2;
        }
        else if (arrow.localPosition.x >= -290f && arrow.localPosition.x < -95f)
        {
            coin_text.text = (total_earning * 3).ToString();
            multi = 3;
        }
        else if (arrow.localPosition.x >= -95f && arrow.localPosition.x < 105f)
        {
            coin_text.text = (total_earning * 5).ToString();
            multi = 5;
        }
        else if (arrow.localPosition.x >= 105f && arrow.localPosition.x < 303f)
        {
            coin_text.text = (total_earning * 2).ToString();
            multi = 2;
        }
        else if (arrow.localPosition.x >= 303f && arrow.localPosition.x < 450f)
        {
            coin_text.text = (total_earning * 3).ToString();
            multi = 3;
        }
    }

    public void button_Claim()
    {
        //if (!active) return;

        active = false;
        // ads video

        AdsMAXManager.Instance.ShowRewardedAd(() => Complete_ads_video(true,""),"spin_prize");
    }

    public void button_NoThanks(bool isWon)
    {
        /*//if (!active) return;

        active = false;
        // ads inter

        //Advertisements.Instance.ShowInterstitial();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //////////////
        GameSceneLoad.Instance.RestartMergeGameObj();

        SequenceManager.Instance.SetStartingLevel(SaveManager.Instance.LevelProgress);
        m_NextLevelEvent.Raise();*/
        ///
        StartCoroutine(btn_NoThanksOnClick(isWon));
    }

    IEnumerator btn_NoThanksOnClick(bool isWon,bool _isAds=true)
    {
        if (isWon)
        {
            HyperCasual.Runner.GameManager.Instance.Win();
        }
        else
        {
            HyperCasual.Runner.GameManager.Instance.Lose();
        }

        yield return null;
        yield return null;
        yield return null;

        //if(_isAds)
            

        GameSceneLoad.Instance.RestartMergeGameObj();
        

        //SequenceManager.Instance.SetStartingLevel(SaveManager.Instance.LevelProgress);

        m_NextLevelEvent.Raise();
    }

    IEnumerator no_thanks_wait()
    {
        yield return new WaitForSeconds(1.5f);
        if (no_thanks_btn != null)
        {
            no_thanks_btn.SetActive(true);
        }
        
        //HyperCasual.Runner.GameManager.Instance.Win();
        //GameSceneLoad.Instance.SetSceneRuns(true);
    }

    private void Complete_ads_video(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            int tt = total_earning * (multi - 1);

            UiManager.instance.increase_money(tt);

          
            //// next level
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            // no reward
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        StartCoroutine(btn_NoThanksOnClick(true,false));
    }

    IEnumerator wait_ads_video()
    {
        yield return new WaitForSeconds(1f);
        Complete_ads_video(true, "");
        //Advertisements.Instance.ShowRewardedVideo(Complete_ads_video);
    }
}
