using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Monster : MonoBehaviour
{
    private static Dictionary<int, int> dict = new Dictionary<int, int>(){
        {(int)SkillType.NearAT,1},
        {(int)SkillType.FarAT,2},
        {(int)SkillType.Magic,3},
        {(int)SkillType.Heal,4},
        {(int)SkillType.BUFF,5},
        {(int)SkillType.Elimination,6},
        {(int)SkillType.Guard,5},
        {(int)SkillType.DEBUFF,6},
        {(int)SkillType.Passive,5}
    };
    public int atkall;//攻击力
    public int nowhp;//当前血量
    public int hpall;//血量
    public int mydebuff;//自身负面效果
    public int mybuff;//自身正面效果
    public int atktime;
    public Hashtable keepbuff;//效果保持回合记
    public Hashtable keepdebuff;
    public Transform skillplace;
    public SkillBaseInfo[] list = new SkillBaseInfo[4];//记录生成的怪物信息 
    void Start()
    {
        
    }
    public void Attack()
    {
        System.Random rd = new System.Random();
        atktime = rd.Next(1,5);
        for(int i = 0; i < skillplace.childCount; i++)
        {
            Destroy(skillplace.GetChild(i).gameObject);
        }
        for(int i = 0; i < atktime; i++)
        {
            int skillnum = rd.Next(1,11);//随机行动次数,最多3次
            SkillBaseInfo info = CardInfo.getSkill("Ms" + skillnum.ToString());
            list[i] = info;
            string prefix = "pm06_1c_0";
            int img_index = 0;
            // .Debug.Log(info.type);
            img_index = dict[info.type]; 
            var sprite = Resources.Load<Sprite>(prefix + img_index.ToString());
            if(img_index != 0 && sprite)
            {
                var img = new GameObject("Image").AddComponent<Image>();
                img.rectTransform.SetParent(skillplace);
                img.sprite = sprite;
                img.SetNativeSize();
            }
        }
    }
}