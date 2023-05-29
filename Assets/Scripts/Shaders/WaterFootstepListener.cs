using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class WaterFootstepListener : MonoBehaviour
{
    Material material;
    int currentFootstep;
    
    [Header("Outer Rim")]
    [SerializeField] [Range(0f, 5f)] float outerRippleDuration;
    [SerializeField] [Range(-1, 1)] float outerStartValue;
    [SerializeField] [Range(0, 2)] float outerEndValue;

    [Header("Inner Rim")]
    [SerializeField] [Range(0f, 5f)] float innerRippleDuration;
    [SerializeField] [Range(-1, 1)] float innerStartValue;
    [SerializeField] [Range(0, 2)] float innerEndValue;
    [SerializeField] [Range(0, 5)] float innerTweenDelayTime;
    [SerializeField] List<FootstepShaderProperties> footstepProps;

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

        FootstepManager.Instance.OnWaterFootstep += OnWaterFootstep;
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
        .OnComplete(() => material.SetFloat(footstepProps[tweeningStep].outerFade, outerStartValue));
        
        // Inner
        DOTween.To(
            x => material.SetFloat(footstepProps[tweeningStep].innerFade, x),
            innerStartValue,
            innerEndValue,
            innerRippleDuration
        )
        .SetDelay(innerTweenDelayTime)
        .OnComplete(() => material.SetFloat(footstepProps[tweeningStep].innerFade, innerStartValue));;
        
        // Increment footstep count.
        currentFootstep = (currentFootstep + 1) % footstepProps.Count;
    }
}
