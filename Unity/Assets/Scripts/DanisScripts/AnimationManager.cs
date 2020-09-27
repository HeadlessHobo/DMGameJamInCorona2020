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

    public bool HasMovementAnimation
    {
        get
        {
            return QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MoveAnimation");
        }
    }
    
    public bool HasIdleAnimation
    {
        get
        {
            return QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EntryIdleAni") ||
                   QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.StandardIdleAni") ||
                   QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.IdleSmokeAnimation");
        }
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
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.IdleSmokeAnimation"))
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
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MoveAnimation") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.IdleSmokeAnimation"))
        {
            QueenUIAnimator.Play("IdleSmokeAnimation");
        }
    }

    //IdleAnimation
    public void QUIIdleAni()
    {
        if (QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.EntryIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.StandardIdleAni") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.MoveAnimation") ||
            QueenUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.IdleSmokeAnimation"))
        {
            QueenUIAnimator.Play("StandardIdleAni");
        }
    }



}
