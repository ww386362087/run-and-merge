using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using HyperCasual.Runner;
using HyperCasual.Gameplay;
using System;
using MoreMountains.NiceVibrations;

public class UiManager : MonoBehaviour
{
    public int price_increase_per_buy;
    public int neko_receive_per_ad;
    public static UiManager instance;
    public GameObject new_character_monster_panel , new_character_warrior_panel,
                      card_panel , remove_ad , ingame , winpanel , lose_panel , game_control , btn_retr , btn_cadre , btn_setting , setting_panel;
    public GameObject btn_warrior_ads , btn_warrior_, btn_monster_ads , btn_monster_ , btn_add_mons_free, btn_add_wars_free;

    public TMPro.TextMeshProUGUI level_nbr_txt, txt_level_win_panel, txt_level_lose_panel, txt_mmoney , txt_coin_monster , txt_coin_warrior , txt_earning_lose , txt_earning_win;
    
    GameController gamecontroller_script;

    //parent cadre
    public MeshRenderer cadre_mesh;

    //------ cards
    public GameObject scroll_monster, scroll_warrior;
    public Image line_monster, line_warrior;
    public int total_coin_in_level_merge;
    public ParticleSystem confetti;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        gamecontroller_script = FindObjectOfType<GameController>();
        //Advertisements.Instance.Initialize();
        //Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);

        string level_txt = "LEVEL" + (GameManager.instance.getlevel() + 1);

        level_nbr_txt.text = level_txt;
        txt_level_win_panel.text = level_txt;
        txt_level_lose_panel.text = level_txt;

        // coins text
        manage_coins_start();

        SetAddFreeStatus();
    }

    private void OnEnable()
    {
        txt_mmoney.text = GameManager.instance.getcoin().ToString();
    }

    public void SetAddFreeStatus()
    {
        btn_add_mons_free.SetActive(PlayerPrefs.GetInt(GameManager.instance.Num_Free_Mons) > 0);
        btn_add_wars_free.SetActive(PlayerPrefs.GetInt(GameManager.instance.Num_Free_Wars) > 0);

        btn_add_mons_free.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("FREE " + PlayerPrefs.GetInt(GameManager.instance.Num_Free_Mons));
        btn_add_wars_free.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("FREE " + PlayerPrefs.GetInt(GameManager.instance.Num_Free_Wars));
    }

    public void btn_add_monster_free()
    {
        gamecontroller_script.add_monster_to_scene();

        //save data
        gamecontroller_script.save_details_cadres();

        PlayerPrefs.SetInt(GameManager.instance.Num_Free_Mons,
                           PlayerPrefs.GetInt(GameManager.instance.Num_Free_Mons) - 1);

        SetAddFreeStatus();
    }

    public void btn_add_warrior_free()
    {
        gamecontroller_script.add_warrior_to_scene();

        //save data
        gamecontroller_script.save_details_cadres();

        PlayerPrefs.SetInt(GameManager.instance.Num_Free_Wars,
                           PlayerPrefs.GetInt(GameManager.instance.Num_Free_Wars) - 1);

        SetAddFreeStatus();
    }

    public void show_remove_ad_panel()
    {
        remove_ad.SetActive(true);
    }

    public void show_win()
    {
        // effect 
        confetti.Play();

        // sound
        SoundManager.instance.Play("win");

        StartCoroutine(show_win_panel());

    }
    //public void show_lose_direct()
    //{
    //    losepanel.SetActive(true);
    //    ingame.SetActive(false);

    //    //Advertisements.Instance.ShowInterstitial();
    //}
    public void show_lose()
    {
        // sound
        SoundManager.instance.Play("lose");

        StartCoroutine(show_lose_panel());
    }
    IEnumerator show_lose_panel()
    {
        txt_earning_lose.text = "+" + total_coin_in_level_merge + "M";

        yield return new WaitForSeconds(1.5f);
        lose_panel.SetActive(true);
        ingame.SetActive(false);

        //Advertisements.Instance.ShowInterstitial();

        EventTracking.Instance.str_End = DateTime.Now.ToString();
        FirebaseManager.Instance.LogEvent_FailLevel();
    }

    IEnumerator show_win_panel()
    {
        //GameManager.instance.setLevel(GameManager.instance.getlevel() + 1);
        ProgressionManager.Instance.SetLevel(PlayerPrefs.GetInt(ProgressionManager.Instance.MERGE_LEVEL_PROGRESSION) + 1);
        SequenceManager.Instance.SetStartingLevel(SaveManager.Instance.LevelProgress);

        
        yield return new WaitForSeconds(2.5f);
        winpanel.SetActive(true);

        //HyperCasual.Runner.GameManager.Instance.Win();
        //GameSceneLoad.Instance.Action_PrepareNextRunGame();
        ingame.SetActive(false);
        txt_earning_win.text = "+" + (total_coin_in_level_merge + Inventory.Instance.TempGold);
        //Advertisements.Instance.ShowInterstitial();

        EventTracking.Instance.str_End = DateTime.Now.ToString();

        FirebaseManager.Instance.LogEvent_FinishLevel();
    }



    public void btn_retry()
    {
        //Advertisements.Instance.ShowInterstitial();

        // sound
        SoundManager.instance.Play("button");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void btn_next()
    {
        //Advertisements.Instance.ShowInterstitial();

        // sound
        //SoundManager.instance.Play("click");

        //GameManager.instance.setLevel(GameManager.instance.getlevel() + 1);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void calcul_total_coin_in_level(int nbr)
    {
        total_coin_in_level_merge += nbr;
    }
    public void increase_money(int nbr)
    {
        GameManager.instance.setcoin(GameManager.instance.getcoin() + nbr);
        txt_mmoney.text = GameManager.instance.getcoin().ToString();
    }



    public void _vibrate()
    {
        if (GameManager.instance.get_vibration() == 1)
        {
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact, true, this);
        }

    }

    // MONSTERS BUTTON
    public void btn_show_monsters()
    {
        _vibrate();

        //scroll
        scroll_monster.SetActive(true);
        scroll_warrior.SetActive(false);

        // lines
        line_monster.enabled = true;
        line_warrior.enabled = false;

        // sound
        SoundManager.instance.Play("button");
    }

    // WARRIORS BUTTON
    public void btn_show_warriors()
    {
        _vibrate();

        //scroll
        scroll_monster.SetActive(false);
        scroll_warrior.SetActive(true);

        // lines
        line_monster.enabled = false;
        line_warrior.enabled = true;

        // sound
        SoundManager.instance.Play("button");

    }

    // close card button
    public void btn_close_card_panel()
    {
        _vibrate();

        // continue game
        gamecontroller_script.game_run = true;

        card_panel.SetActive(false);

        // sound
        SoundManager.instance.Play("button");
    }

    // open card button
    public void btn_open_card_panel()
    {
        _vibrate();

        // stop game
        gamecontroller_script.game_run = false;

        card_panel.SetActive(true);

        // sound
        SoundManager.instance.Play("button");
    }

    // open setting button
    public void btn_open_setting()
    {
        _vibrate();

        // stop game
        gamecontroller_script.game_run = false;

        setting_panel.SetActive(true);

        // sound
        SoundManager.instance.Play("button");
    }

    // close new character monster panel
    public void btn_close_new_character_monster_panel()
    {
        _vibrate();

        // continue game
        gamecontroller_script.game_run = true;

        new_character_monster_panel.SetActive(false);

        // sound
        SoundManager.instance.Play("button");
    }

    // close new character warrior panel
    public void btn_close_new_character_warrior_panel()
    {
        _vibrate();

        // continue game
        gamecontroller_script.game_run = true;

        // sound
        SoundManager.instance.Play("button");

        new_character_warrior_panel.SetActive(false);
    }

    // show new character warrior panel
    public void show_panel_new_character_warrior()
    {
        _vibrate();

        // stop game
        gamecontroller_script.game_run = false;

        // sound
        SoundManager.instance.Play("upgrade");

        new_character_warrior_panel.SetActive(true);
        new_character_warrior_panel.GetComponent<NewCharacterWarrior>().show_unlock_warrior();
    }

    // show new character monster panel
    public void show_panel_new_character_monster()
    {
        _vibrate();

        // stop game
        gamecontroller_script.game_run = false;

        // sound
        SoundManager.instance.Play("upgrade");

        new_character_monster_panel.SetActive(true);
        new_character_monster_panel.GetComponent<NewCharacterMonster>().show_unlock_monster();
    }

    // button add monster
    public void btn_add_monster()
    {
        _vibrate();

        // sound
        SoundManager.instance.Play("deploy");
        if (gamecontroller_script.list_empty_cadres.Count == 0) return;

        if(GameManager.instance.getcoin() >= GameManager.instance.get_actual_coin_monster())
        {
            gamecontroller_script.add_monster_to_scene();

            //save data
            gamecontroller_script.save_details_cadres();

            GameManager.instance.setcoin(GameManager.instance.getcoin() - GameManager.instance.get_actual_coin_monster());

            // show text coins total
            txt_mmoney.text = GameManager.instance.getcoin().ToString();

            // increase actual coin warrior
            GameManager.instance.set_actual_coin_monster(GameManager.instance.get_actual_coin_monster() + price_increase_per_buy);
            Debug.Log("Coin mua moi: " + GameManager.instance.get_actual_coin_monster());
            // show text coin monster
            txt_coin_monster.text = GameManager.instance.get_actual_coin_monster().ToString();

            // check if coins not enough monster
            if (GameManager.instance.getcoin() < GameManager.instance.get_actual_coin_monster())
            {
                // active button ads video
                btn_monster_ads.SetActive(true);
                //btn_monster_.SetActive(false);
            }

            // check if coins not enough warrior
            if (GameManager.instance.getcoin() < GameManager.instance.get_actual_coin_warrior())
            {
                // active button ads video
                btn_warrior_ads.SetActive(true);
               // btn_warrior_.SetActive(false);
            }

        }
        
    }

    // button add monster with ads video
    public void btn_add_monster_ads()
    {
        _vibrate();
        if (gamecontroller_script.list_empty_cadres.Count == 0) return;

        // sound
        SoundManager.instance.Play("deploy");


        // ads video
        //Advertisements.Instance.ShowRewardedVideo(Complete_ads_video_mosnter);
        AdsMAXManager.Instance.ShowRewardedAd(() => Complete_ads_video_mosnter(true, ""),"reward_moster");
    }


    // button add warrior
    public void btn_add_warrior()
    {
        _vibrate();
        if (gamecontroller_script.list_empty_cadres.Count == 0) return;
        // sound
        SoundManager.instance.Play("deploy");

        if (GameManager.instance.getcoin() >= GameManager.instance.get_actual_coin_warrior())
        {
            gamecontroller_script.add_warrior_to_scene();

            //save data
            gamecontroller_script.save_details_cadres();

            // set coins coins total
            GameManager.instance.setcoin(GameManager.instance.getcoin() - GameManager.instance.get_actual_coin_warrior());

            // show text 
            txt_mmoney.text = GameManager.instance.getcoin().ToString();

            // increase actual coin warrior
            GameManager.instance.set_actual_coin_warrior(GameManager.instance.get_actual_coin_warrior() + price_increase_per_buy);

            // show text coin warrior
            txt_coin_warrior.text = GameManager.instance.get_actual_coin_warrior().ToString();

            // check if coins not enough warrior
            if (GameManager.instance.getcoin() < GameManager.instance.get_actual_coin_warrior())
            {
                // active button ads video
                btn_warrior_ads.SetActive(true);
                //btn_warrior_.SetActive(false);
            }

            // check if coins not enough monster
            if (GameManager.instance.getcoin() < GameManager.instance.get_actual_coin_monster())
            {
                // active button ads video
                btn_monster_ads.SetActive(true);
                //btn_monster_.SetActive(false);
            }
        }
        
    }

    // button add warrior with ads video
    public void btn_add_warrior_ads()
    {
        _vibrate();
        if (gamecontroller_script.list_empty_cadres.Count == 0) return;

        // sound
        SoundManager.instance.Play("deploy");


        // ads video
        //Advertisements.Instance.ShowRewardedVideo(Complete_ads_video_warrior);
        AdsMAXManager.Instance.ShowRewardedAd(()=>Complete_ads_video_warrior(true,""),"reward_warrior");
    }

    // button fight
    public void btn_fight()
    {
        _vibrate();

        game_control.SetActive(false);
        btn_retr.SetActive(true);
        btn_cadre.SetActive(false);
        btn_setting.SetActive(false);

        cadre_mesh.enabled = false;

        gamecontroller_script.players_scripts.choose_enemy_for_fight_start();

        gamecontroller_script.enemies_script.choose_player_for_fight_start();
        GameController.Instance.game_play = true;
    }

    
    public void manage_coins_start()
    {
        // coins total
        txt_mmoney.text = GameManager.instance.getcoin().ToString();

        //monster
        if (GameManager.instance.getcoin() >= GameManager.instance.get_actual_coin_monster())
        {
            // show text coin monster
            txt_coin_monster.text = GameManager.instance.get_actual_coin_monster().ToString();
        }
        else
        {
            // active button ads video
            btn_monster_ads.SetActive(true);
            //btn_monster_.SetActive(false);
        }

        //warrior
        if (GameManager.instance.getcoin() >= GameManager.instance.get_actual_coin_warrior())
        {
            // show text coin warrior
            txt_coin_warrior.text = GameManager.instance.get_actual_coin_warrior().ToString();
        }
        else
        {
            // active button ads video
            btn_warrior_ads.SetActive(true);
            //btn_warrior_.SetActive(false);
        }
    }

    private void Complete_ads_video_mosnter(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            for (int i = 0; i < neko_receive_per_ad; i++)
            {
                gamecontroller_script.add_monster_to_scene();
            }

            //save data
            gamecontroller_script.save_details_cadres();
        }
        else
        {
            // no reward
        }

        SetAddFreeStatus();
    }

    private void Complete_ads_video_warrior(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);

        if (completed == true)
        {
            for (int i = 0; i < neko_receive_per_ad; i++)
            {
                gamecontroller_script.add_warrior_to_scene();
            }

            //save data
            gamecontroller_script.save_details_cadres();
        }
        else
        {
            // no reward
        }
        SetAddFreeStatus();
    }

    //IEnumerator save_data_wait()
    //{
    //    yield return new WaitForSeconds(1f);

    //    //save data
    //    gamecontroller_script.save_details_cadres();
    //}
}
