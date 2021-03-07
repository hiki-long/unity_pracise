using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndChoose : MonoBehaviour
{
    public void ToBegin()
    {
        SceneManager.LoadScene("Begin");
        GlobalControl.Instance.TeamLeftHP = 100000;
        GlobalControl.Instance.BattleSceneindex = 0;
        GlobalControl.Instance.PlayerPosindex = 0;
        GlobalControl.Instance.playerpos = new Vector3(-2.12f,-1.02f,0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}