using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class Footsteps : MonoBehaviour
{
    [SerializeField]
    private bool _playSound;

    [SerializeField]
    private float DistancePerStep = 3f;
    private Vector3 _prevPos;
    private float _distanceCovered;

    [SerializeField] private FMODUnity.EventReference footstepEvent;
    EventInstance footstepEventInstance;
    [SerializeField] private FMODUnity.EventReference jumpEvent;
    EventInstance jumpEventInstance;
    [SerializeField] private FMODUnity.EventReference landEvent;
    EventInstance landEventInstance;

    private void Start()
    {
        footstepEventInstance = FMODUnity.RuntimeManager.CreateInstance(footstepEvent);
        jumpEventInstance = FMODUnity.RuntimeManager.CreateInstance(jumpEvent);
        landEventInstance = FMODUnity.RuntimeManager.CreateInstance(landEvent);
    }

    public void ExternalUpdate(bool isGonnaJump, bool isGrounded, bool isLandedThisFrame)
    {
        if(!_playSound) return;
        
        if (_distanceCovered > DistancePerStep)
        {
            _distanceCovered = 0;

            footstepEventInstance.start();
        }

        if (isGonnaJump && isGrounded)
        {
            jumpEventInstance.start();
        }

        if (isLandedThisFrame && !isGonnaJump)
        {
            landEventInstance.start();
        }

        if (isGrounded)
        {
            _distanceCovered += Vector3.Distance(_prevPos.WithY(0), transform.position.WithY(0));
        }
        _prevPos = transform.position;
    }
}
