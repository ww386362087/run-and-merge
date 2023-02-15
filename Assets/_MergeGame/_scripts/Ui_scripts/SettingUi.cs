using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUi : MonoBehaviour
{
    public GameObject btn_vibra, btn_snd;
    GameController gamecontroller_script;
    // Start is called before the first frame update
    void Start()
    {
        gamecontroller_script = FindObjectOfType<GameController>();
        load_details();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_close()
    {
        // continue game
        gamecontroller_script.game_run = true;

        gameObject.SetActive(false);
    }

    public void btn_vibration()
    {
        UiManager.instance._vibrate();

        // sound
        SoundManager.instance.Play("button");

        if (GameManager.instance.get_vibration() == 1)
        {
            btn_vibra.transform.GetChild(0).gameObject.SetActive(false);
            btn_vibra.transform.GetChild(1).gameObject.SetActive(true);
            GameManager.instance.set_vibration(0);
        }
        else
        {
            btn_vibra.transform.GetChild(1).gameObject.SetActive(false);
            btn_vibra.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.instance.set_vibration(1);
        }
    }

    public void btn_sound()
    {
        UiManager.instance._vibrate();

        // sound
        SoundManager.instance.Play("button");

        if (GameManager.instance.get_sound() == 1)
        {
            btn_snd.transform.GetChild(0).gameObject.SetActive(false);
            btn_snd.transform.GetChild(1).gameObject.SetActive(true);
            GameManager.instance.set_sound(0);
        }
        else
        {
            btn_snd.transform.GetChild(1).gameObject.SetActive(false);
            btn_snd.transform.GetChild(0).gameObject.SetActive(true);
            GameManager.instance.set_sound(1);
        }
    }

    public void load_details()
    {

        // sound

        if (GameManager.instance.get_sound() == 0)
        {
            btn_snd.transform.GetChild(0).gameObject.SetActive(false);
            btn_snd.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            btn_snd.transform.GetChild(1).gameObject.SetActive(false);
            btn_snd.transform.GetChild(0).gameObject.SetActive(true);
        }

        // vibration
        if (GameManager.instance.get_vibration() == 0)
        {
            btn_vibra.transform.GetChild(0).gameObject.SetActive(false);
            btn_vibra.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            btn_vibra.transform.GetChild(1).gameObject.SetActive(false);
            btn_vibra.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
