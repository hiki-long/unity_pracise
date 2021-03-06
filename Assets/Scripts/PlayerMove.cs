using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] float objectSpeed = 1.0f;
    public GameObject button_left;//左侧按钮
    public GameObject button_right;//右侧按钮
    public GameObject button_next;
    bool select;

    int NextPosIndex;
    Transform NextPos;
    bool gomove;
    void Start()
    {
        NextPosIndex = GlobalControl.Instance.PlayerPosindex;
        NextPos = Positions[GlobalControl.Instance.PlayerPosindex]; 
        this.transform.position = GlobalControl.Instance.playerpos;
    }
    void FixedUpdate()
    {
        if(this.gomove)
           MoveGameObject();
    }
    public void setMove(bool gomove)
    {
        this.gomove = gomove;
    } 
    public void MoveGameObject()
    {
        if(transform.position == NextPos.position)
        {
            this.gomove = false;
            NextPosIndex++;
            // Debug.Log(NextPosIndex);
            if(NextPosIndex < Positions.Length)
                NextPos = Positions[NextPosIndex];
            if(NextPosIndex == 8 || NextPosIndex == 13)
            {
                // Debug.Log("Go to end");
                GoToEnd();
            }
            else if(NextPosIndex == 2  && !select)
            {
                button_left.SetActive(true);
                button_right.SetActive(true);
                button_next.SetActive(false);
                this.gomove = false;
            }
            else
                GoInBattle();
        }

        transform.position = Vector3.MoveTowards(transform.position, NextPos.position, objectSpeed * Time.deltaTime);
    }
    public void GoInBattle()
    {
        // Debug.Log(NextPosIndex);
        GlobalControl.Instance.PlayerPosindex = NextPosIndex;
        System.Random rd = new System.Random();
        int res = rd.Next(0,5);
        GlobalControl.Instance.BattleSceneindex = res;
        GlobalControl.Instance.playerpos = transform.position;
        SceneManager.LoadScene("Battle1");
    }
    public void GoToEnd()
    {
        SceneManager.LoadScene("EndGame");
    }
    public void AfterChoose(bool right)
    {
        button_next.SetActive(true);
        button_left.SetActive(false);
        button_right.SetActive(false);
        select = true;
        if(right)
        {
            NextPosIndex += 6;
            NextPos = Positions[NextPosIndex];
            this.setMove(true);
            MoveGameObject();
        }
        else
        {
            NextPosIndex = 2;
            NextPos = Positions[NextPosIndex];
            this.setMove(true);
            MoveGameObject();
        }
    }

}
