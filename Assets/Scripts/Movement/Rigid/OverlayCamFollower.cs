using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCamFollower : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform baseCamera;
    [SerializeField] Transform cameraOrientation;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position + baseCamera.localPosition;
        transform.rotation = cameraOrientation.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + baseCamera.localPosition;
        transform.rotation = cameraOrientation.rotation;
    }
}
