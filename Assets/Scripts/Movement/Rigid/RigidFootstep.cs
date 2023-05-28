using UnityEngine;

public class RigidFootstep : MonoBehaviour
{
    public static RigidFootstep Instance { get; private set; }

    [SerializeField] float footstepDistance = 1;
    // Vector3 previousFootstepPosition;
    float distanceTraveled = 0;
    Vector3 previousPosition;

    public delegate void FootstepEvent(Vector3 position);
    public event FootstepEvent OnFootstep;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // previousFootstepPosition = transform.position;
        previousPosition = transform.position;
    }

    void Update()
    {
        distanceTraveled += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;

        if(distanceTraveled > footstepDistance)
        {
            Debug.Log("NEW FOOTSTEP");
            if (OnFootstep != null) OnFootstep(transform.position);
            distanceTraveled = 0;
        }


        // Vector3 currentPosition = transform.position;
        // if(Vector3.Distance(previousFootstepPosition, currentPosition) > footstepDistance)
        // {
        //     previousFootstepPosition = currentPosition;
        // }
    }
}
