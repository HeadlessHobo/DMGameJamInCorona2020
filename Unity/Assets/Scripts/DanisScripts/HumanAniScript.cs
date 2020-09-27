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
            rndNum = Random.Range(0, 4);
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


    //
    public void HumanDeath()
    {
        if(rndNum == 0)
        {
            HumanAnimator.Play("Dane0Death");
        }
        else if(rndNum == 1)
        {
            HumanAnimator.Play("Dane1Death");
        }
        else if(rndNum == 2)
        {
            HumanAnimator.Play("Dane2Death");
        }
        else if (rndNum == 3)
        {
            HumanAnimator.Play("Dane3Death");
        }
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
