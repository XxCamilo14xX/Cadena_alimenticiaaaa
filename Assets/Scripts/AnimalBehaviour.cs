using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public float speed = 1f;
    public float wanderRadius = 0.5f;
    Vector3 targetPos;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
            PickNewTarget();
    }

    void PickNewTarget()
    {
        targetPos = transform.position + new Vector3(Random.Range(-wanderRadius, wanderRadius),
                                                     Random.Range(-wanderRadius, wanderRadius), 0);
    }
}

