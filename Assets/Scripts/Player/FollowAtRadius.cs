using UnityEngine;

public class FollowAtRadius : MonoBehaviour
{
    private Vector2 center;
    public float radius;
    public Transform target;
    public EnemyManager enemyManager;

    void Start()
    {
        this.center = transform.localPosition;
    }

    void Update()
    {
        if (target != null && target.gameObject.activeInHierarchy)
        {
            Vector2 targetPos = target.position;
            Vector2 heading = (targetPos - center);

            if (heading.sqrMagnitude > radius)
                heading = heading.normalized;

            transform.localPosition = center + heading * radius;
        } else
        {
            target = enemyManager.GetNewTarget();
            transform.localPosition = center;
        }
    }
}
