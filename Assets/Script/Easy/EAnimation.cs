using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAnimation : MonoBehaviour
{
    public Animator anim;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            anim.Play("walk_shoot_ar", -1, 0f);
        }
       
    }
}
