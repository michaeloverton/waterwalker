using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReverbZoneTrigger : MonoBehaviour
{
    [SerializeField] private AudioReverbManager reverbManager;
    [SerializeField] private bool exitTrigger;
    [SerializeField] private bool stopReverb;

    void OnTriggerEnter(Collider other)
    {
        if(!stopReverb) reverbManager.StartBasicReverb();
        else reverbManager.StopBasicReverb();
    }

    void OnTriggerExit(Collider other)
    {
        if(exitTrigger && !stopReverb) reverbManager.StopBasicReverb();
        else if(exitTrigger && stopReverb) reverbManager.StartBasicReverb();
    }
}
