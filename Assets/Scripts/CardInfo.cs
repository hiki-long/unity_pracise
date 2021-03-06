using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Buff{
    public bool isbuff; //buff分为两种,false就是debuff
    public int bufftype;//bufftype和debufftype合并
    public int keeplive;//生效回合数
};

public struct SkillBaseInfo{
    public int cost;
    public int type;
    public bool costadd;
    public bool used;//被使用
    public string skillname;//技能名
    public float skillpower;//技能倍率
    public int hittimes;//攻击次数
    public Buff buffinfo;

    public SkillBaseInfo(string skillname, int cost, int type, float skillpower=0,int hittimes=1,bool costadd=false,int bufftype=0, int keeplive=1,bool isbuff=true,bool used=false )
    {
        this.skillname = skillname;
        this.cost = cost;
        this.type = type;
        this.skillpower = skillpower;
        this.costadd = costadd;
        this.used = used;
        this.hittimes = hittimes;
        Buff info;
        info.bufftype = bufftype;
        info.keeplive = keeplive;
        info.isbuff = isbuff;
        this.buffinfo = info;
    }
};

public class CardInfo 
{
    // Start is called before the first frame update
    private static Dictionary<string, SkillBaseInfo> dict = new Dictionary<string, SkillBaseInfo>(){
        {"Aidang1",new SkillBaseInfo("舞女",0,(int)SkillType.CHARGE)},
        {"Aidang2",new SkillBaseInfo("异常抗性1",0,(int)SkillType.InTeam)},
        {"Rance1", new SkillBaseInfo("突击-零-",0,(int)SkillType.NearAT,1.0f)},
        {"Rance2", new SkillBaseInfo("鬼畜重击",3,(int)SkillType.NearAT,3.0f)},
        {"Amituosi1", new SkillBaseInfo("古代种杀手",2,(int)SkillType.FarAT,2.0f)},
        {"Amituosi2", new SkillBaseInfo("钢铁意志",2,(int)SkillType.Guard,0)},
        {"Haniblack1", new SkillBaseInfo("哈尼冲击波",2,(int)SkillType.FarAT,1.5f)},
        {"Haniblack2", new SkillBaseInfo("连击发生3", 0,(int)SkillType.InTeam)},
        {"Henite1", new SkillBaseInfo("六色破坏光线",6,(int)SkillType.Magic,6.0f)},
        {"Henite2", new SkillBaseInfo("魔力球",1,(int)SkillType.FarAT,0.1f,6)},
        {"Jiahalasi1", new SkillBaseInfo("突击",1,(int)SkillType.NearAT,1.0f)},
        {"Jiahalasi2", new SkillBaseInfo("洛璐璐 荷鲁斯",4,(int)SkillType.BUFF,0,0,false,(int)BuffType.AUTOHEAL,5)},
        {"Qianxin1", new SkillBaseInfo("轮攻之剑", 2, (int)SkillType.NearAT,1.0f,4,true)},
        {"Qianxin2", new SkillBaseInfo("毗沙天门", 2,(int)SkillType.BUFF,0,0,false,(int)BuffType.ATTACKUP,4)},
        {"Ms1", new SkillBaseInfo("普通攻击", 0, (int)SkillType.NearAT,1.0f)},
        {"Ms2", new SkillBaseInfo("普通射击", 0, (int)SkillType.FarAT, 1.0f)},
        {"Ms3", new SkillBaseInfo("火魔法", 0, (int)SkillType.Magic, 1.0f,1,false,(int)DebuffType.ONFIRE,3,false)},
        {"Ms4", new SkillBaseInfo("冰魔法", 0, (int)SkillType.Magic, 1.0f,1,false,(int)DebuffType.ONCOLD,3,false)},
        {"Ms5", new SkillBaseInfo("重击", 0, (int)SkillType.NearAT, 3.0f)},
        {"Ms6", new SkillBaseInfo("防御屏障",0, (int)SkillType.Guard)},
        {"Ms7", new SkillBaseInfo("魔法屏障",0,(int)SkillType.Passive)},
        {"Ms8", new SkillBaseInfo("回复",0,(int)SkillType.Heal,2.0f)},
        {"Ms9", new SkillBaseInfo("军师效果", 0, (int)SkillType.BUFF,0,0,false,(int)BuffType.ATTACKUP,9)},
        {"Ms10", new SkillBaseInfo("魔法阵",0,(int)SkillType.BUFF,0,1,false,(int)BuffType.MAGICUP,9)}
    };
    
    public static SkillBaseInfo getSkill(string str)
    {
        if(dict.ContainsKey(str))
            return dict[str];
        else
            return new SkillBaseInfo("", 0, 0);
    }

    
}
