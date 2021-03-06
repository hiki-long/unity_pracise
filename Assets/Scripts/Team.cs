using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Team : MonoBehaviour
{
    [SerializeField] GameObject[] cards;
    private bool[] isvalid;//记录联合部队的空位置
    private Hashtable inTeam;//观察是否有同一阵营在联合部队里
    public int power;//每回合费用
    public int atkall;//攻击力
    public int hpall;//血量
    public int nowhp;//当前血量
    public int guardlevel;//防御层数
    public int mydebuff;//自身负面效果
    public int mybuff;//自身正面效果
    public Hashtable keepbuff;//效果保持回合记录
    public Hashtable keepdebuff;//异常效果保持
    private int origin_x = 16;
    private int origin_y = 230;
    private int space = 142;//生成间距
    // private int begin_x = 520;
    private int begin_x = -412;
    private int begin_y = 10;
    // private int begin_y = 230;
    void Awake()
    {
        
    }
    void Start()
    {
      foreach(var element in cards)
        {
            if(element != null)
            {
                GameObject card = Instantiate(element,transform.position, Quaternion.identity);
                card.GetComponentInChildren<Card>().Team = this.gameObject;
                // card.transform.parent = GameObject.Find("CardHold").transform;
                card.transform.SetParent(GameObject.Find("CardHold").transform);
                card.transform.position += new Vector3(begin_x, begin_y, 0) - new Vector3(origin_x, origin_y, 0);
                begin_x += space;
            }
        }  
    }
    // public Monster monster;
    public void PowerIncrease(int turn)
    {
        if ((turn % 2) == 1)
            this.power += 2;
        else
            this.power += 3;
        this.power = this.power > 6 ? 6 : this.power;
    }
    
}
