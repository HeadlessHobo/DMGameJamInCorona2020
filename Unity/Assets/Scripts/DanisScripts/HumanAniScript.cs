using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAniScript : MonoBehaviour
{
    public int rndNum;
    public Sprite[] HumanSprites = new Sprite[4];
    public Sprite QueenSprite;
    public GameObject This;
    public bool isQueen = false;

    public Animator HumanAnimator;
    // Start is called before the first frame update
    void Start()
    {
        if(isQueen == false)
        {
            rndNum = Random.Range(1, 5);
           This.GetComponent<SpriteRenderer>().sprite = HumanSprites[rndNum];
        }
        else
        {
            This.GetComponent<SpriteRenderer>().sprite = QueenSprite;
        }



        HumanIdleAni();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Play when human is Idle
    public void HumanIdleAni()
    {
        HumanAnimator.Play("HumanIdle");
    }

    //Play when human is moving. 
    public void HumanMove()
    {
        HumanAnimator.Play("HumanMove"); 
    }

    //Play when human is happy and excited
    public void HumanExcitedAni()
    {
        HumanAnimator.Play("HumanExcited");
    }

    //When queen puts down dynamite
    public void QueenGiveDynamiteAni()
    {
        HumanAnimator.Play("GiveDynamiteAni");
    }


}
