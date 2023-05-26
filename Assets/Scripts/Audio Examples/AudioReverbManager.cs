using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AudioReverbManager : MonoBehaviour
{
    [SerializeField] private string basicReverbSnapshot;
    EventInstance basicReverbInstance;
    [SerializeField] bool playOnStart;

    void Start()
    {
        basicReverbInstance = FMODUnity.RuntimeManager.CreateInstance("snapshot:/" + basicReverbSnapshot);
        if(playOnStart) basicReverbInstance.start();
    }

    public void StartBasicReverb()
    {
        basicReverbInstance.start();
    }

    public void StopBasicReverb()
    {
        basicReverbInstance.stop(STOP_MODE.ALLOWFADEOUT);
    }
}
