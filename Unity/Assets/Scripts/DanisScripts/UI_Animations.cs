using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Animations : MonoBehaviour
{


    public Animator UIAnimator;

    //Play when group dies.
    public void CoronaHitAni()
    {
        UIAnimator.Play("CoronaHit");
    }
}
