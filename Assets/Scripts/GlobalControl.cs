using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    //单例模式,控制全局
    private static GlobalControl _instance;//控制的唯一单例
    public static GlobalControl Instance { get { return _instance; } }
    public int BattleSceneindex;//怪物类型场景
    public int PlayerPosindex;//玩家地图位置
    public int TeamLeftHP;//联合部队剩余HP
    public Vector3 playerpos;//玩家进入地图一开始的位置

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Instance.BattleSceneindex = 0;
            Instance.playerpos = new Vector3(-2.12f,-1.02f,0);
            Instance.TeamLeftHP = 100000;
            Instance.PlayerPosindex = 0;
        }
    }
}
