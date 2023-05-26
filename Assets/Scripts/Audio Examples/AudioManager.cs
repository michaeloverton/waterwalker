using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // [SerializeField] PauseManager pauseManager;
    [SerializeField] private string stepSurfaceParam;
    
    [SerializeField] private string masterFilterParam;
    [SerializeField] private float minMasterFilter;
    [SerializeField] private string masterPhaserParam;
    [SerializeField] private bool masterPhaserOn;

    // void Start()
    // {
    //     pauseManager.OnPausePressed += onPausePressed;
    // }

    private void onPausePressed(bool val)
    {
        if(val)
        {
            FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(masterFilterParam, minMasterFilter);
            if(result != FMOD.RESULT.OK) Debug.LogError(result);
        }
        else
        {
            FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(masterFilterParam, 1);
            if(result != FMOD.RESULT.OK) Debug.LogError(result);
        }
    }

    public void SetMasterPhaser(float val)
    {
        if(!masterPhaserOn) return;
        
        FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(masterPhaserParam, val);
        if(result != FMOD.RESULT.OK) Debug.LogError(result);
    }

    public void ResetMasterFilter()
    {
        FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(masterFilterParam, 1);
        if(result != FMOD.RESULT.OK) Debug.LogError(result);
    }

    public void SetStepSurface(int surfaceIndex) {
        FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.setParameterByName(stepSurfaceParam, surfaceIndex);
        if(result != FMOD.RESULT.OK) Debug.LogError(result);
    }
}
