using UnityEngine;

public class RandomMoveInSquare : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Area (Local Space)")]
    public Vector2 areaSize = new Vector2(5f, 5f);

    [Header("Timing")]
    public float interval = 2f;

    private float timer;

    void Update()
    {
        if (!target) return;

        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0f;
            MoveTarget();
        }
    }

    void MoveTarget()
    {
        float x = Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f);
        float z = Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f);

        Vector3 localPos = new Vector3(x, 0f, z);
        target.position = transform.TransformPoint(localPos);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 center = transform.position;
        Vector3 size = new Vector3(areaSize.x, 0f, areaSize.y);

        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
