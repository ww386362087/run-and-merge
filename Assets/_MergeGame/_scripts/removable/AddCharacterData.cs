using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AddCharacterData : MonoBehaviour
{
    [SerializeField] List<GameObject> monsterCardList;
    [SerializeField] List<GameObject> warriorCardList;

    [SerializeField] List<GameObject> newMonsterList;
    [SerializeField] List<GameObject> newWarriorList;
    
    [SerializeField] List<Monster> monsterDataList;
    [SerializeField] List<Warrior> warriorDataList;

    [ContextMenu("Set monster card list")]
    public void SetMonsterCardList()
    {
        for (int i = 0; i < monsterCardList.Count; i++)
        {
            monsterCardList[i].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetHp(i, true);
            monsterCardList[i].transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetDamage(i, true);
        }
    }

    [ContextMenu("Set warrior card list")]
    public void SetWarriorCardList()
    {
        for (int i = 0; i < warriorCardList.Count; i++)
        {
            warriorCardList[i].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetHp(i, false);
            warriorCardList[i].transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetDamage(i, false);
        }
    }

    [ContextMenu("Set new monster list")]
    public void SetNewMonsterList()
    {
        for (int i = 0; i < newMonsterList.Count; i++)
        {
            newMonsterList[i].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetHp(i, true);
            newMonsterList[i].transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetDamage(i, true);
        }
    }

    [ContextMenu("Set new warrior list")]
    public void SetNewWarriorList()
    {
        for (int i = 0; i < newWarriorList.Count; i++)
        {
            newWarriorList[i].transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetHp(i, false);
            newWarriorList[i].transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = GetDamage(i, false);
        }
    }

    public string GetDamage(int index, bool isMonster)
    {
        if (isMonster)
        {
            return monsterDataList[index].damage.ToString();
        }
        else
        {
            return warriorDataList[index].damage.ToString();
        }
    }

    public string GetHp(int index, bool isMonster)
    {
        if (isMonster)
        {
            return monsterDataList[index].health.ToString();
        }
        else
        {
            return warriorDataList[index].health.ToString();
        }
    }
}
