using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

// CURRENTLY UNUSED. PORTED TO FOOTSTEPS.CS
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private FMODUnity.EventReference footstepEvent;
    EventInstance footstepEventInstance;
    [SerializeField] private FMODUnity.EventReference jumpEvent;
    EventInstance jumpEventInstance;
    [SerializeField] private FMODUnity.EventReference landEvent;
    EventInstance landEventInstance;

    void Start()
    {
        footstepEventInstance = FMODUnity.RuntimeManager.CreateInstance(footstepEvent);
        jumpEventInstance = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
        landEventInstance = FMODUnity.RuntimeManager.CreateInstance(landEvent);
    }

    public void PlayFootstep()
    {
        footstepEventInstance.start();
    }

    public void PlayJump()
    {
        jumpEventInstance.start();
    }

    public void PlayLand()
    {
        landEventInstance.start();
    }
}
