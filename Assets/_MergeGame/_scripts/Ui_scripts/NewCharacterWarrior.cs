using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterWarrior : MonoBehaviour
{
    public List<GameObject> list_warriors;

    // Start is called before the first frame update
    void Start()
    {
        //show_unlock_warrior();
    }


    public void show_unlock_warrior()
    {
        int cnt = GameManager.instance.get_count_active_warrior();
        print(cnt);
        //active background
        list_warriors[cnt - 2].SetActive(true);
    }
}
