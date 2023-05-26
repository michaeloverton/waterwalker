using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AmbienceManager : MonoBehaviour
{
    [SerializeField] private bool playAmbience;
    [SerializeField] private bool playTraffic;
    [SerializeField] private string ambienceEventName;
    EventInstance ambienceInstance;
    [SerializeField] private string trafficEventName;
    EventInstance trafficInstance;
    [SerializeField] private string birdsEventName;
    EventInstance birdsInstance;

    // Music Events
    [SerializeField] private bool playMusic;
    [SerializeField] private string technoMusicEventName;
    EventInstance technoMusic;
    [SerializeField] private string houseMusicEventName;
    EventInstance houseMusic;
    [SerializeField] private string weirdMusicEventName;
    EventInstance weirdMusic;

    // Start is called before the first frame update
    void Start()
    {
        ambienceInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + ambienceEventName);
        if(playAmbience) ambienceInstance.start();

        trafficInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + trafficEventName);
        if(playTraffic) trafficInstance.start();

        birdsInstance = FMODUnity.RuntimeManager.CreateInstance("event:/" + birdsEventName);
        birdsInstance.start();

        technoMusic = FMODUnity.RuntimeManager.CreateInstance("event:/" + technoMusicEventName);
        if(playMusic) technoMusic.start();

        houseMusic = FMODUnity.RuntimeManager.CreateInstance("event:/" + houseMusicEventName);
        if(playMusic) houseMusic.start();

        weirdMusic = FMODUnity.RuntimeManager.CreateInstance("event:/" + weirdMusicEventName);
        if(playMusic) weirdMusic.start();
    }

    public void SetMusicParameter(string parameter, float value) {
        switch(parameter) 
        {
            case "technoVolume": 
                technoMusic.setParameterByName(parameter, value);
                break;
            case "houseVolume": 
                houseMusic.setParameterByName(parameter, value);
                break;
            case "weirdVolume": 
                weirdMusic.setParameterByName(parameter, value);
                break;
            default:
                Debug.Log("ERROR SETTING MUSIC PARAMETER");
                break;
        }
    }

    public void SetCrowdParameter(string parameter, float value) {
        ambienceInstance.setParameterByName(parameter, value);
    }

    public void SetTrafficParameter(string parameter, float value) {
        trafficInstance.setParameterByName(parameter, value);
    }

    public void SetBirdsParameter(string parameter, float value) {
        birdsInstance.setParameterByName(parameter, value);
    }

    public void StopMusic()
    {
        ambienceInstance.stop(STOP_MODE.IMMEDIATE);
        ambienceInstance.release();
        
        trafficInstance.stop(STOP_MODE.IMMEDIATE);
        trafficInstance.release();

        birdsInstance.stop(STOP_MODE.IMMEDIATE);
        birdsInstance.release();
        
        technoMusic.stop(STOP_MODE.IMMEDIATE);
        technoMusic.release();
        
        houseMusic.stop(STOP_MODE.IMMEDIATE);
        houseMusic.release();
        
        weirdMusic.stop(STOP_MODE.IMMEDIATE);
        weirdMusic.release();
    }
}
