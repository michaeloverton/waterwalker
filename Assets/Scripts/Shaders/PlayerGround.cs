using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    [SerializeField] Material groundMaterial;
    [SerializeField] float footstepTime = 1f;
    [SerializeField] float footstepOnTime = 0.5f;
    [SerializeField] float footstepDecay = 0.1f;
    float timeElapsed = 0f;
    float footstepOnTimeElapsed = 0f;
    bool footstepOn;
    float currentFootstepMultiplier = 1;

    // Update is called once per frame
    void Update()
    {
        // Ray ray = new Ray(transform.position, -Vector3.up, 50);
        RaycastHit hit = new RaycastHit();
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 50)) 
        {
            Debug.Log(hit.textureCoord);
            groundMaterial.SetVector("_Point", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
        }


        // TESTING
        // timeElapsed += Time.deltaTime;
        // if(timeElapsed > footstepTime) 
        // {
        //     footstepOn = true;
        //     timeElapsed = 0;
        // }

        // if(footstepOn)
        // {
        //     Debug.Log("footstep on");
        //     groundMaterial.SetFloat("_FootstepMultiplier", currentFootstepMultiplier);
        //     currentFootstepMultiplier = Mathf.Clamp01(currentFootstepMultiplier- footstepDecay * Time.deltaTime);
        //     footstepOnTimeElapsed += Time.deltaTime;
        //     Debug.Log(currentFootstepMultiplier);

        //     if(footstepOnTimeElapsed > footstepOnTime)
        //     {
        //         footstepOn = false;
        //         groundMaterial.SetFloat("_FootstepMultiplier", 0);
        //         currentFootstepMultiplier = 1;
        //         footstepOnTimeElapsed = 0;
        //     }
        // }
    }
}
