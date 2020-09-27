using System.Collections;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

public class AnimationManager : Singleton<AnimationManager>
{

    public Animator QueenUIAnimator;

    void Start()
    {
        QUIIdleAni();
    }

    ////////////////--- Explosion animations --- ////////////////
    
    //When the queen speaks
    public void QUIQuoteAni()
    {
        QueenUIAnimator.Play("MajestyQuote");
    }

    ////////////////--- Queen UI animations --- ////////////////

    //When the queen is hit by an explosion
    public void QUIBlownAwayAni()
    {
        QueenUIAnimator.Play("BlownAway");
    }

    //When player moves
    public void QUIMoveAni()
    {
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleSmokeAnimation"))
        {
            QueenUIAnimator.Play("MoveAnimation");
        }
    }

    //When player throws dynamite
    public void QUIThrowDynAni()
    {
        QueenUIAnimator.Play("ThrowDynamite");
        
    }

    //Randomly triggers when on idle
    public void QUISmokeIdleAni()
    {
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("MoveAnimation") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleSmokeAnimation"))
        {
            QueenUIAnimator.Play("IdleSmokeAnimation");
        }
    }

    //IdleAnimation
    public void QUIIdleAni()
    {
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("MoveAnimation") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("IdleSmokeAnimation"))
        {
            QueenUIAnimator.Play("StandardIdleAni");
        }
    }



}
