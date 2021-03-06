using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform[] Positions;
    [SerializeField] float objectSpeed = 1.0f;

    int NextPosIndex;
    Transform NextPos;
    bool gomove;
    void Start()
    {
        NextPos = Positions[0]; 
    }

    // Update is called once per frame
    void Update()
    {
        // if(this.gomove)
        //    MoveGameObject(); 
    }

    void FixedUpdate()
    {
        if(this.gomove)
           MoveGameObject();
    //    MoveGameObject(); 
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
            if(NextPosIndex >= Positions.Length)
            {
                NextPosIndex = 0;
            }
            NextPos = Positions[NextPosIndex];
        }
        transform.position = Vector3.MoveTowards(transform.position, NextPos.position, objectSpeed * Time.deltaTime);
    }

}
