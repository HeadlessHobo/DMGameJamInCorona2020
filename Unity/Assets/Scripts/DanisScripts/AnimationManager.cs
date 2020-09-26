using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public Animator ExplosionAnimator;

    public Animator QueenUIAnimator;

    void Start()
    {
        QUIIdleAni();
    }

    public void ExplosionAni()
    {
        ExplosionAnimator.Play("Explosion");
    }

    ////////////////--- Explosion animations --- ////////////////
    
    //When the queen speaks
    public void QUIQuoteAni()
    {
        QueenUIAnimator.Play("MajestyQuote");
    }

    ////////////////--- Queen UI animations --- ////////////////

    //When the qeen is hit by an explosion
    public void QUIBlownAwayAni()
    {
        QueenUIAnimator.Play("BlownAway");
    }

    //When player moves
    public void QUIMoveAni()
    {
            QueenUIAnimator.Play("MoveAnimation");
    }

    //When player throws dynamite
    public void QUIThrowDynAni()
    {
        QueenUIAnimator.Play("ThrowDynamite");
        
    }

    //Randomly triggers when on idle
    public void QUISmokeIdleAni()
    {

        QueenUIAnimator.Play("IdleSmokeAnimation");
    }

    //IdleAnimation
    public void QUIIdleAni()
    {
        QueenUIAnimator.Play("StandardIdleAni");
    }



}
