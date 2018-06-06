using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationComponent  {

//	private  AnimationComponent animInastance;

    public AnimationComponent()
    {
    }

    public void playAnimation(GameObject obj, string anim){
		obj.GetComponent<Animator>().Play(anim);
	}

  public void playCanvasAnimation(Animator animator, string nameAnim){
    animator.Play(nameAnim);
  }

  public void playButtonAnimation(Button btn, string anim){
    btn.GetComponent<Animator>().Play(anim);
  }
}
