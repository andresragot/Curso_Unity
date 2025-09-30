using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyFollower2D : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Velocidad")]

    [SerializeField] float maxSpeed = 4f;

    [SerializeField] float acceleration = 12f;

    [SerializeField] float steerResponse = 6f;

    [SerializeField] float stopDistance = 0.02f;

    [Header("Separación")]
    [SerializeField] bool enableSeparation = true;

    [SerializeField] float separationRadius = 0.5f;

    [SerializeField] float separationWeight = 0.75f;

    [SerializeField] LayerMask separationLayerMask;

    [Header("Wobble / Variacion")]
    [SerializeField] float wobbleAmplitude = 0.25f;
    [SerializeField] float wobbleFrequency = 1.2f;

    Rigidbody2D rb;
    float currentSpeed;
    float wobbleSeed;

    int overlapsCount;
    static Collider2D[] overlapsBuffer = new Collider2D[16];

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Va a ir entre 0 y 1
        wobbleSeed = Random.value * 1000f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSpeed = 0f;
        if (rb != null) rb.linearVelocity = Vector2.zero;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        if (!target)
            return;

       Vector2 pos = rb.position;
        Vector2 toTarget = (Vector2)target.position - pos;

        float dist = toTarget.magnitude;

        if (dist < stopDistance)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 1f - Mathf.Exp(-steerResponse * Time.deltaTime));
            return;
        }

        Vector2 dir = toTarget / Mathf.Max(dist, 0.0001f);

        // ---- Wobble
        float t = Time.time + wobbleSeed;
        float wobble = Mathf.Sin(t * wobbleFrequency) * wobbleAmplitude;
        Vector2 perp = new Vector2(-dir.y, dir.x);
        Vector2 wobbleVec = perp * wobble;

        // -- Separacion
        Vector2 separation = Vector2.zero;
        if (enableSeparation && separationRadius > 0.01f)
        {
            overlapsBuffer = Physics2D.OverlapCircleAll(pos, separationRadius, separationLayerMask);

            for (int i = 0; i < overlapsBuffer.Length; ++i)
            {
                var c = overlapsBuffer[i];
                if (!c) continue;
                if (c.attachedRigidbody == rb) continue;

                Vector2 toMe = (Vector2)transform.position - (Vector2)c.transform.position;
                float d = toMe.magnitude;
                if (d > 0.0001f)
                {
                    // Empuje inversamente proporcional a la distancia
                    separation += toMe / (d * d);
                }

                if (separation != Vector2.zero)
                    separation = separation.normalized * separationWeight;
            }
        }



        Vector2 desiredDir = (dir + wobbleVec + separation);
        if (desiredDir.sqrMagnitude > 0.0001f)
            desiredDir.Normalize();
        else
            desiredDir = dir;


        currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, acceleration * Time.deltaTime);

        Vector2 desiredVelocity = currentSpeed * desiredDir;

        float k = 1f - Mathf.Exp(-steerResponse * Time.deltaTime);
        Vector2 newVel = Vector2.Lerp(rb.linearVelocity, desiredVelocity, k);

        rb.linearVelocity = newVel;
    }
}
