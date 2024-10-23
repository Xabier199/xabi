using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{

    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("FadeOut", 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FadeOut()
    {
        animator.Play("FadeOut");
    }
}
