using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationComponent  {

//	private  AnimationComponent animInastance;

    public AnimationComponent()
    {
    }

    // public AnimationComponent GetAnimInstance()
    // {
    //     Debug.Log("animo get instance");
    //     if (animInastance == null) animInastance = new AnimationComponent();
    //     return animInastance;
    // }

    public void playAnimation(GameObject obj, string anim){
		Debug.Log("animo play anim");
		obj.GetComponent<Animator>().Play(anim);

	}
}
