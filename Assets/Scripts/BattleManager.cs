using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
public class BattleManager : MonoBehaviour { //这里是进行回合管理的代码
    public int thisindex;
    private AudioSource audiosr;
    private AudioClip audiohit;
    private AudioClip audiobuff;
    public int turn;
    public int MaxTurn;
    public GameObject TurnText;
    public GameObject StartButton;
    public GameObject EndButton;
    public GameObject StartText;
    [SerializeField] Image[] APs;
    [SerializeField] GameObject[] Battlescene;
    public GameObject TeamHP;
    public GameObject MonsterHP; 
    public GameObject Cover;
    public GameObject cardplace;
    private Team team;
    private Monster monster;
    private int apmax = 6;
    public GameObject showmshp;
    public GameObject showushp;
    public GameObject buffshowmshp;
    public GameObject buffshowushp;
    public Transform DamageEffect;
    private Vector3 MsDamage = new Vector3(0,-200,0);
    private Vector3 MyDamage = new Vector3(0,140,0);
    public GameObject GuardBuff;
    public GameObject GuardTimes;
    public AudioSource bgm;
    private void Awake()
    {
        int tmp = GlobalControl.Instance.BattleSceneindex;
        if(tmp != thisindex)
        {
            this.gameObject.SetActive(false);
            this.bgm.Pause();
            Battlescene[tmp].SetActive(true);
        }
        team = gameObject.GetComponent<Team>();
        team.nowhp = GlobalControl.Instance.TeamLeftHP;
        float temp = (float)team.nowhp / (float)team.hpall;
        showushp.GetComponent<Text>().text = team.nowhp.ToString();
        TeamHP.GetComponent<Image>().fillAmount = temp;
        buffshowushp.GetComponent<Image>().fillAmount = temp;
    }
    public void TurnIncrease()
    {
        this.turn += 1;
    }

    public void StartBattle()
    {
        audiosr = this.gameObject.AddComponent<AudioSource>();
        audiosr.playOnAwake = false;
        audiohit = Resources.Load<AudioClip>("hitcut");
        audiobuff = Resources.Load<AudioClip>("buffcut");
        this.StartButton.SetActive(false);
        this.StartText.SetActive(false);
        this.Cover.SetActive(false);
        this.turn = 1;
        team.PowerIncrease(this.turn);
        APs[0].sprite = Resources.Load<Sprite>("yellowap");
        APs[1].sprite = Resources.Load<Sprite>("yellowap");
        monster = gameObject.GetComponent<Monster>();
        monster.Attack();
    }
    public void EndOneTurn()
    {
        this.turn += 1;
        cardplace.BroadcastMessage("EnterNextTurn",SendMessageOptions.DontRequireReceiver);
        team.PowerIncrease(this.turn);
        //更新ap贴图
        for(int i = 0; i < team.power; i++)
        {
            APs[i].sprite = Resources.Load<Sprite>("yellowap");
        }
        for(int j = team.power; j < apmax; j++)
        {
            APs[j].sprite = Resources.Load<Sprite>("noap");
        }
        MonsterSecond();
        team.guardlevel = 0;
        GuardBuff.SetActive(false);
    }
    public void ReDraw()
    {
        for(int i = 0; i < team.power; i++)
        {
            APs[i].sprite = Resources.Load<Sprite>("yellowap");
        }
        for(int j = team.power; j < apmax; j++)
        {
            APs[j].sprite = Resources.Load<Sprite>("noap");
        }
    }

    public void Update()
    {

        if(this.turn <= this.MaxTurn)
        {
            TurnText.GetComponent<Text>().text = this.turn.ToString() + "/" + this.MaxTurn.ToString();
        }
        BloodEffect();

    }
    public void BloodEffect()
    {
        float a = buffshowmshp.GetComponent<Image>().fillAmount;
        float b = MonsterHP.GetComponent<Image>().fillAmount;
        if(a > b)
        {
            a -= 0.003f;
            buffshowmshp.GetComponent<Image>().fillAmount = a;
        }
        else
        {
            buffshowmshp.GetComponent<Image>().fillAmount = b;
        }
        a = buffshowushp.GetComponent<Image>().fillAmount;
        b = TeamHP.GetComponent<Image>().fillAmount;
        // Debug.Log(a);
        // Debug.Log(b);
        if(a > b)
        {
            a -= 0.003f;
            buffshowushp.GetComponent<Image>().fillAmount = a;
        }
        else
        {
            buffshowushp.GetComponent<Image>().fillAmount = b;
        }
    }
    
    public void UpdateUsHp()
    {
        if(team.nowhp == 0)
        {
            EndGame();
        }
        else
        {
            float temphp =  (float)team.nowhp  / (float)team.hpall;
            TeamHP.GetComponent<Image>().fillAmount = temphp;
            showushp.GetComponent<Text>().text = team.nowhp.ToString();
        }
    }
    public void UpdateMsHp()
    {
        if(monster.nowhp == 0)
        {
            EndGame();
        }
        else
        {
            float temphp =  (float)monster.nowhp  / (float)monster.hpall;
            MonsterHP.GetComponent<Image>().fillAmount = temphp;
            showmshp.GetComponent<Text>().text = monster.nowhp.ToString();
        }
    }

    public void TeamFirst(CardAndSkillinfo info)
    {//Card传递伤害信息
        // Debug.Log(info.info.skillname);
        int skilltype = info.info.type;
        int atktimes = info.info.hittimes;
        float skillpower = info.info.skillpower;
        System.Random rd = new System.Random();
        if(skilltype == (int)SkillType.NearAT || skilltype == (int)SkillType.FarAT || skilltype == (int)SkillType.Magic)
        {
            for(int i = 0; i < info.info.hittimes; i++)
            {
                //暂时没找到好的延迟效果实现
                double extra = rd.Next(90,110) / 100.0;//技能攻击浮动在0.9~1.1
                int damage = (int)(skillpower * info.cardatk * extra);
                monster.nowhp -= damage;
                DamagePopUp.Create(DamageEffect, MyDamage, damage, transform);
                monster.nowhp = Math.Max(0, monster.nowhp);
                audiosr.PlayOneShot(audiohit); 
                UpdateMsHp();
            }
        }
        else if(skilltype == (int)SkillType.Heal)
        {
            double extra = rd.Next(90,110) / 100.0;//技能攻击浮动在0.9~1.1
            team.nowhp += (int)(skillpower * info.cardatk * extra);
            team.nowhp = (team.nowhp > team.hpall) ? team.hpall : team.nowhp;
            UpdateUsHp();
        }
        else if(skilltype == (int)SkillType.CHARGE)
        {
            team.power += 1;
            team.power = Math.Min(6, team.power);
            ReDraw();
        }
        else if(skilltype == (int)SkillType.BUFF)
        {
            //BUFF是比较复杂的类，稍后分析
        }
    }
    public void MonsterSecond()
    {
        System.Random rd = new System.Random();
        for(int i = 0; i < monster.atktime; i++)
        {
            if(monster.list[i].type == (int)SkillType.NearAT || monster.list[i].type == (int)SkillType.FarAT || monster.list[i].type == (int) SkillType.Magic)
            {
                double extra = rd.Next(90,110) / 100.0;
                int damage = (int)(monster.list[i].skillpower * monster.atkall * extra * (1-0.2f*team.guardlevel));
                team.nowhp -= damage;
                team.nowhp = Math.Max(0,team.nowhp);
                DamagePopUp.Create(DamageEffect, MsDamage, damage, transform);
                audiosr.PlayOneShot(audiohit);
                UpdateUsHp();
            }
            else if(monster.list[i].type == (int)SkillType.Heal)
            {
                double extra = rd.Next(90,110) / 100.0;
                monster.nowhp += (int)(monster.list[i].skillpower * monster.atkall * extra);
                UpdateMsHp();
            }
        }
        monster.Attack();
    }
    public void GuardButton()
    {
        team.guardlevel += 1;
        audiosr.PlayOneShot(audiobuff);
        team.guardlevel = Math.Min(4, team.guardlevel);
        GuardTimes.GetComponent<Text>().text = "防御(" + team.guardlevel.ToString() + "/4)";
        GuardBuff.SetActive(true);
    }
    public void EndGame()
    {
        //这里应该结束游戏，切换场景
        // Debug.Log(team.nowhp);
        GlobalControl.Instance.TeamLeftHP = team.nowhp;
        SceneManager.LoadScene("Second");
    }

    public void GoShied()
    {
        if(team.power > 0)
        {
            team.power -= 1;
            GuardButton();
            ReDraw();
        }
    }

}
