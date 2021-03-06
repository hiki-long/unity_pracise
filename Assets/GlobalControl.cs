using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour
{
    //单例模式,控制全局
    public static GlobalControl Instance;
    public int BattleSceneindex;
    public int PlayerPosindex;
    public int TeamLeftHP;
    public Vector3 playerpos;

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            Instance.BattleSceneindex = 0;
            Instance.playerpos = new Vector3(-2.12f,-1.02f,0);
            Instance.TeamLeftHP = 100000;
            Instance.PlayerPosindex = 0;
        }
        else if(Instance != null)
        {
            Destroy(gameObject);
        }
    }
}
