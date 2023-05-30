using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public static FootstepManager Instance { get; private set; }

    [SerializeField] float footstepDistance = 1;
    // Vector3 previousFootstepPosition;
    float distanceTraveled = 0;
    Vector3 previousPosition;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask waterLayer;
    RigidMovement movementController;
    private bool inAir = false;

    public delegate void FootstepEvent(Vector3 position);
    public event FootstepEvent OnFootstep;
    public event FootstepEvent OnWaterFootstep;
    public event FootstepEvent OnLanding;
    public event FootstepEvent OnWaterLanding;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        movementController = GetComponent<RigidMovement>();
        previousPosition = transform.position;
    }

    void Update()
    {
        if(movementController.IsGrounded())
        {
            if(inAir) // If inAir is still true, then in previous frame, we were airborne. So this is the landing frame.
            {
                inAir = false;
                distanceTraveled = 0;
                previousPosition = transform.position;

                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out hit, 50, waterLayer)) 
                {
                    Debug.Log("water landing!"  + " " + Time.frameCount);
                    if (OnWaterLanding != null) OnWaterLanding(new Vector3(hit.textureCoord.x, hit.textureCoord.y, 0)); // Ignore the third component.
                }

                return;
            }

            distanceTraveled += Vector3.Distance(previousPosition, transform.position);
            previousPosition = transform.position;

            if(distanceTraveled > footstepDistance)
            {
                if (OnFootstep != null) OnFootstep(transform.position);
                
                RaycastHit hit = new RaycastHit();
                if(Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out hit, 50, waterLayer)) 
                {
                    if (OnWaterFootstep != null) OnWaterFootstep(new Vector3(hit.textureCoord.x, hit.textureCoord.y, 0)); // Ignore the third component.
                }

                distanceTraveled = 0;
            }
        }
        else
        {
            distanceTraveled = 0;
            previousPosition = transform.position;
            inAir = true;
        }
    }
}
