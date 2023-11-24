using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    Animator anim;
    Command keyQ, keyW, keyE, keyP, keySpace, keyK;

    void Start()
    {
        keyQ = new PerformJump();
        keyW = new DoNothing();
        keyE = new DoNothing();
        keyP = new PerformPunch();


        keySpace = new PerformJump();
        anim = actor.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            keyP.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            keyK.Execute(anim);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(anim);
        }
    }
}
