using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public Animator ExplosionAnimator;

    // Update is called once per frame
    void Update()
    {
        //DEBUGS
        if(Input.GetKeyDown(KeyCode.A))
        {
            ExplosionAni();
        }
    }

    public void ExplosionAni()
    {
        ExplosionAnimator.Play("Explosion");
    }

}
