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

    public delegate void FootstepEvent(Vector3 position);
    public event FootstepEvent OnFootstep;
    public event FootstepEvent OnWaterFootstep;

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
        distanceTraveled += Vector3.Distance(previousPosition, transform.position);
        previousPosition = transform.position;

        if(distanceTraveled > footstepDistance)
        {
            Debug.Log("NEW FOOTSTEP");
            if (OnFootstep != null) OnFootstep(transform.position);
            

            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(transform.position + Vector3.up, -Vector3.up, out hit, 50, waterLayer)) 
            {
                if (OnWaterFootstep != null) OnWaterFootstep(new Vector3(hit.textureCoord.x, hit.textureCoord.y, 0)); // Ignore the third component.
                Debug.Log("water footstep!");
            }

            distanceTraveled = 0;
        }
    }
}
