using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class WaterFootstepListener : MonoBehaviour
{
    Material material;

    [Header("Outer Rim")]
    [SerializeField] [Range(0f, 5f)] float outerRippleDuration;
    [SerializeField] [Range(-1, 1)] float outerStartValue;
    [SerializeField] [Range(0, 2)] float outerEndValue;

    [Header("Inner Rim")]
    [SerializeField] [Range(0f, 5f)] float innerRippleDuration;
    [SerializeField] [Range(-1, 1)] float innerStartValue;
    [SerializeField] [Range(0, 2)] float innerEndValue;
    [SerializeField] [Range(0, 5)] float innerTweenDelayTime;

    [Header("Footstep")]
    int currentFootstep;
    [SerializeField] List<FootstepShaderProperties> footstepProps;
    
    [Header("Landing")]
    int currentLanding;
    [SerializeField] List<FootstepShaderProperties> landingProps;

    [Serializable]
    private class FootstepShaderProperties {
        [SerializeField] public string position;
        [SerializeField] public string innerFade;
        [SerializeField] public string outerFade;
    }

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        currentFootstep = 0;
        currentLanding = 0;

        FootstepManager.Instance.OnWaterFootstep += OnWaterFootstep;
        FootstepManager.Instance.OnWaterLanding += OnWaterLanding;
    }

    void OnWaterFootstep(Vector3 texturePosition)
    {
        int tweeningStep = currentFootstep;

        Debug.Log("footstep " + tweeningStep);
        material.SetVector(footstepProps[tweeningStep].position, new Vector4(texturePosition.x, texturePosition.y, 0, 0));

        // Outer
        DOTween.To(
            x => material.SetFloat(footstepProps[tweeningStep].outerFade, x),
            outerStartValue,
            outerEndValue,
            outerRippleDuration
        )
        // .SetEase(Ease.InOutExpo)
        .OnComplete(() => material.SetFloat(footstepProps[tweeningStep].outerFade, outerStartValue));
        
        // Inner
        DOTween.To(
            x => material.SetFloat(footstepProps[tweeningStep].innerFade, x),
            innerStartValue,
            innerEndValue,
            innerRippleDuration
        )
        // .SetEase(Ease.InOutExpo)
        .SetDelay(innerTweenDelayTime)
        .OnComplete(() => material.SetFloat(footstepProps[tweeningStep].innerFade, innerStartValue));
        
        // Increment footstep count.
        currentFootstep = (currentFootstep + 1) % footstepProps.Count;
    }

    void OnWaterLanding(Vector3 texturePosition)
    {
        int tweeningStep = currentLanding;

        Debug.Log("landing " + tweeningStep + " " + Time.frameCount);
        material.SetVector(landingProps[tweeningStep].position, new Vector4(texturePosition.x, texturePosition.y, 0, 0));

        // Outer
        DOTween.To(
            x => material.SetFloat(landingProps[tweeningStep].outerFade, x),
            0,
            3,
            7
        )
        // .SetEase(Ease.InOutExpo)
        .OnComplete(() => material.SetFloat(landingProps[tweeningStep].outerFade, outerStartValue));
        
        // Inner
        DOTween.To(
            x => material.SetFloat(landingProps[tweeningStep].innerFade, x),
            0,
            5,
            7
        )
        // .SetEase(Ease.InOutExpo)
        // .SetDelay(innerTweenDelayTime)
        .OnComplete(() => material.SetFloat(landingProps[tweeningStep].innerFade, innerStartValue));

        currentLanding = (currentLanding + 1) % landingProps.Count;
    }
}
