### Unity demo简介

#### 本Unity项目是仿制兰斯10RPG战略游戏制作的，完成了大致以下几个场景：

1. 游戏开始场景

   ![开始场景](https://github.com/hiki-long/unity_pracise/img/1.png)

2.  游戏移动场景

   ![移动场景](https://github.com/hiki-long/unity_practise/img/2.png)

3. 游戏战斗场景

   ![战斗场景](https://github.com/hiki-long/unity_practise/img/3.png)

##### 待实现的场景

* 联合部队
* 存档机制
* Save/Load场景

##### 战斗场景设计思路

大体的设计思路如下：

从上到下：

* 游戏的全局变量由GlobalControl.cs来控制，使用单例模式记录一些需要在场景切换中保存的全局变量。
* 游戏的卡牌对战逻辑由BattleManager来进行编写，负责怪兽和联合部队作战时候的血量UI调整，伤害效果显示，以及每回合AP进行刷新。
* 怪兽本身有一个Monster类，用于编写怪兽出技能的逻辑和本身数值的存储
* 联合部队同样有一个Team类，主要是用于卡牌布局的动态生成
* 每个卡牌都是一个Card类，里面包含了一张卡牌所有需要用到的属性值。
* 由于每个角色卡有两个技能，所以一个Card类管理两个Skill类的刷新机制。卡牌在技能使用过后的一回合费用加1.
* Skill类主要负责提供刷新费用接口以及卡牌上技能信息的获取并显示出来。
* CardInfo类主要提供角色和怪兽技能种类的信息查询，内容放在一个静态字典中，并通过getSkill(string name)接口调用

其他:

* 个人坐标移动是通过PlayerMove来进行逻辑管理的
* EndChoose类，游戏结束画面管理退出
* DamagePopUp类，显示伤害数字特效的管理的类，在一定时间后自动销毁
* GradientColor.cs类，网上找来的进行颜色过渡动态显示的类
* FollowPlayer类，用来进行相机跟随角色移动的类



