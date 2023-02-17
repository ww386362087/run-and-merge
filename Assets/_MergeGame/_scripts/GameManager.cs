
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        onstartfirsttime();


    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            setcoin(9999999);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            resetall();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            set_count_active_warrior(get_count_active_warrior() + 1);
            print(get_count_active_warrior());
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            //setLevel(getlevel() + 1);
            //if (levels.Length <= getlevel() + 1)
            //    return;
            setLevel(getlevel() + 1);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void onstartfirsttime()
    {
        if (!PlayerPrefs.HasKey("firsttime_genaral"))
        {
            PlayerPrefs.SetInt("level_general", 0);
            PlayerPrefs.SetInt("firsttime_genaral", 0);
            PlayerPrefs.SetInt("coin", 0);
            PlayerPrefs.SetInt("count_warrior", 1);
            PlayerPrefs.SetInt("count_monster", 1);
            PlayerPrefs.SetInt("coin_warrior", 4);
            PlayerPrefs.SetInt("coin_monster", 4);
            PlayerPrefs.SetInt("sound", 1);
            PlayerPrefs.SetInt("vibration", 1);

            save_monster_details();
            save_warrior_details();

            //set cadre 12 monster
            PlayerPrefs.SetInt("monster_type2", 1);
            //set cadre 13 warrior
            PlayerPrefs.SetInt("warrior_type12", 1);
        }
    }

    //level
    public void setLevel(int lv)
    {
        PlayerPrefs.SetInt("level_general", lv);
    }
    public int getlevel()
    {
        return PlayerPrefs.GetInt("level_general");
    }

    //sound
    public void set_sound(int sd)
    {
        PlayerPrefs.SetInt("sound", sd);
    }
    public int get_sound()
    {
        return PlayerPrefs.GetInt("sound");
    }

    //vibration
    public void set_vibration(int vb)
    {
        PlayerPrefs.SetInt("vibration", vb);
    }
    public int get_vibration()
    {
        return PlayerPrefs.GetInt("vibration");
    }

    // reset
    public void resetall()
    {
        PlayerPrefs.DeleteKey("firsttime_genaral");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



    // coin
    public int getcoin()
    {
        return PlayerPrefs.GetInt("coin");
    }
    public void setcoin(int nbr)
    {
        PlayerPrefs.SetInt("coin", nbr);
    }

    // actual coin to buy monster
    public int get_actual_coin_monster()
    {
        return PlayerPrefs.GetInt("coin_monster");
    }
    public void set_actual_coin_monster(int nbr)
    {
        PlayerPrefs.SetInt("coin_monster", nbr);
    }

    // actual coin to buy warrior
    public int get_actual_coin_warrior()
    {
        return PlayerPrefs.GetInt("coin_warrior");
    }
    public void set_actual_coin_warrior(int nbr)
    {
        PlayerPrefs.SetInt("coin_warrior", nbr);
    }


    // monster character
    public int get_count_active_monster()
    {
        return PlayerPrefs.GetInt("count_monster");
    }

    public void set_count_active_monster(int nbr)
    {
        PlayerPrefs.SetInt("count_monster", nbr);
    }

    // warrior character
    public int get_count_active_warrior()
    {
        return PlayerPrefs.GetInt("count_warrior");
    }

    public void set_count_active_warrior(int nbr)
    {
        PlayerPrefs.SetInt("count_warrior", nbr);
    }

    public void save_monster_details()
    {
        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetInt("monster_type" + i, 0);
        }
    }

    public void save_warrior_details()
    {
        for (int i = 0; i < 15; i++)
        {
            PlayerPrefs.SetInt("warrior_type" +i, 0);
        }
    }

    // save warrior
    public int get_save_warrior(int index)
    {
        return PlayerPrefs.GetInt("warrior_type" + index);
    }

    public void set_save_warrior(int index , int type)
    {
        PlayerPrefs.SetInt("warrior_type" + index, type);
    }

    // save monster
    public int get_save_monster(int index)
    {
        return PlayerPrefs.GetInt("monster_type" + index);
    }

    public void set_save_monster(int index, int type)
    {
        PlayerPrefs.SetInt("monster_type" + index, type);
    }
}
