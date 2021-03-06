using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
enum Attr {Fire=1, Ice, Sun, Dark, Lightling, None};
enum Place {Rance=1, LISASI, HEERMAN, SAISI, FREECITY, JAPAN, OTHER, NOMAN, MONSTER, GOD};
public struct CardAndSkillinfo{
    //释放技能看个人面版
    public SkillBaseInfo info;
    public int cardatk;
};

public class Card : MonoBehaviour // Start is called before the first frame update
{
    GraphicRaycaster raycaster;
    public GameObject gameobject;
    public int attack;
    public int hp;
    public int attr;
    public int level;
    public int place;
    private float growth = 1.5f;
    private int hp_base = 1000;
    private int atk_base = 2000;
    private bool isDragging = false;
    private Vector2 startPosition;
    private bool isOverDropZone = false;
    private GameObject dropZone;
    public bool inEdit = false;
    public GameObject ShowUsed;
    public GameObject Team;
    private GameObject buttonobj;
    public GameObject skill1;
    public GameObject skill2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }
    public void GenerateHPandAT()
    {
        this.hp = (int)(this.level * growth)*1000 + hp_base; 
        this.attack = (int)(this.level * growth)*1000 + atk_base;
    }
    void Awake()
    {
        this.inEdit = false;
        this.raycaster = GetComponent<GraphicRaycaster>();
        // this.GenerateHPandAT();
    }

    void Update()
    {
        if (!inEdit && Input.GetKeyDown(KeyCode.Mouse0))
         {
             //Set up the new Pointer Event
             PointerEventData pointerData = new PointerEventData(EventSystem.current);
             List<RaycastResult> results = new List<RaycastResult>();
 
             //Raycast using the Graphics Raycaster and mouse click position
             pointerData.position = Input.mousePosition;
             this.raycaster.Raycast(pointerData, results);
 
             //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
             //每次都不显示，直到被点击的卡牌才会显示详情技能
             gameobject.SetActive(false);
            //  Debug.Log(gameobject.name);
             foreach (RaycastResult result in results)
             {
                //  Debug.Log("Hit " + result.gameObject.name);
                 if(!ShowUsed.activeSelf && result.gameObject.name == "CardBody")
                 {
                    gameobject.SetActive(true);
                 }
             }
             
        }
        else
        {
            //在编辑部队的时候可以进行拖拽
            if(isDragging)
                transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void StartDrag()
    {
        if(inEdit)
        {
            startPosition = transform.position;
            isDragging = true;
        }
    }

    public void EndDrag()
    {
        if(inEdit)
        {

            isDragging = false;
            if(isOverDropZone)
            {
                transform.parent.SetParent(dropZone.transform,false);
                // Debug.Log(dropZone.transform.position);
            }
            else
            {
                transform.position = startPosition;
            }
        }
    }

    public void UseEffect(GameObject skillbutton)
    {
        //gameobject和gameObject有本质区别,gameObject代表使用的哪个对象的函数,这里是Canvas(Card)对象
        //gameobject是自己指定的类型
        buttonobj = skillbutton;
        Team team = Team.GetComponent<Team>();
        int skillcost = skillbutton.GetComponent<Skill>().skillinfo.cost + (skillbutton.GetComponent<Skill>().skillinfo.used ? 1 : 0);
        if(skillcost  <= team.power)
        {
            SkillBaseInfo info = skillbutton.GetComponent<Skill>().skillinfo;
            CardAndSkillinfo cardskill;
            cardskill.info = info;
            skillbutton.GetComponent<Skill>().UpdateCardcost(true);
            cardskill.cardatk = this.attack;
            team.power -= skillcost;
            SendMessageUpwards("ReDraw",SendMessageOptions.DontRequireReceiver);
            SendMessageUpwards("TeamFirst",cardskill, SendMessageOptions.DontRequireReceiver);
            gameobject.SetActive(false);
            ShowUsed.SetActive(true);
        }
    }

    public void EnterNextTurn()
    {
        ShowUsed.SetActive(false);
        skill1.GetComponent<Skill>().SkillUPdate();
        skill2.GetComponent<Skill>().SkillUPdate();
    }
    
}
