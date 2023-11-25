using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    Animator anim;
    Command keyQ, keyW, keyE, keyP, keySpace, keyK;

    List<Command> oldCommands;

    Coroutine replayCoroutine;
    bool startReplay;
    bool isPlayingReplay;
    //bool 

    void Start()
    {
        oldCommands = new List<Command>();
        keyQ = new PerformJump();
        keyW = new DoNothing();
        keyE = new DoNothing();
        keyP = new PerformPunch();
        keyK = new PerformKick();


        keySpace = new PerformJump();
        anim = actor.GetComponent<Animator>();
        Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
    }

    void Update()
    {
        if (!isPlayingReplay)
            HandleInput();
        StartReplay();
    }

    void UndoLastCommand()
    {
        if (oldCommands.Count < 1) return;

        Command c = oldCommands[oldCommands.Count - 1];
        c.Execute(anim);
        oldCommands.RemoveAt(oldCommands.Count - 1);


    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keyQ.Execute(anim);
            oldCommands.Add(keyQ);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            keyP.Execute(anim);
            oldCommands.Add(keyP);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            keyK.Execute(anim);
            oldCommands.Add(keyK);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            keySpace.Execute(anim);
            oldCommands.Add(keySpace);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            startReplay = true;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            //swap punch and kick
            Command temp = keyK;
            keyK = keyP;
            keyP = temp;
        }
    }

    void StartReplay()
    {
        if (startReplay && oldCommands.Count > 0)
        {
            startReplay = false;
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }
            replayCoroutine = StartCoroutine(StartReplaySequence());
        }
    }

    IEnumerator StartReplaySequence()
    {
        isPlayingReplay = true;
        foreach (var command in oldCommands)
        {
            command.Execute(anim);
            yield return new WaitForSeconds(1f);
        }

        isPlayingReplay = true;
        yield return null;
    }
}
