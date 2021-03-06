using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
enum SkillType {NearAT=1, FarAT=2, Heal=4, Magic=8, BUFF=16, DEBUFF=32, Passive=64, InTeam=128, Elimination=256, Guard=512, CHARGE=1024 };
enum DebuffType {ONFIRE=1, ONCOLD=2, ONELEC=4, ONDARK=8, HEALFORBID=16, STOP=32, SLEEP=64, FEAR=128, CURSE=256, POISION=512};
enum BuffType {ATTACKUP=1, GUARDUP=2, MAGICUP=4, AUTOHEAL=8, IMMUNE=16};//immune是免疫的意思
public class Skill : MonoBehaviour{
    public SkillBaseInfo skillinfo;
    public int num;
    public GameObject gameobject;
    private Transform[] allChild;
    public Transform costrecord;
    public Transform basecostrecord;
    private bool hasupdate = false;
    private bool hasinit = false;
    void Init()
    {
        //要记得处理(Clone)后缀
        string temp = gameobject.name.Replace("(Clone)","");
        skillinfo = CardInfo.getSkill(temp + num.ToString());
        allChild = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChild)
        {
            if(child.name == "SkillName")
            {
                child.GetComponent<Text>().text = this.skillinfo.skillname;
                // Debug.Log(this.skillinfo.skillname);
            }
            else if(child.name == "Cost")
            {
                child.GetComponent<Text>().text = this.skillinfo.cost.ToString();
            }
        }
        
    }

    public void UpdateCardcost(bool used)
    {
        string tmp;
        if(used)
        {
            tmp = (this.skillinfo.cost+1).ToString();
            hasupdate = true;
        }
        else
        {
            tmp = this.skillinfo.cost.ToString();
            Debug.Log(tmp);
        }
        basecostrecord.GetComponent<Text>().text = tmp;
        costrecord.GetComponent<Text>().text = tmp;
    }

    public void SkillUPdate()
    {
        if(!hasinit)
        {
            hasinit = true;
            Init();
        }
        if(!hasupdate)
        {
            UpdateCardcost(hasupdate);
        }
        hasupdate = false;
    }
}