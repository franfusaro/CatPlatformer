using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] GameObject platform;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] public bool move = true;

    int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
        }
    }

    private void Move()
    {
        if (waypoints.Count == 0) { return; }

        var targetPos = waypoints[waypointIndex].transform.position;
        var movementThisFrame = speed * Time.deltaTime;
        platform.transform.position = Vector2.MoveTowards(platform.transform.position, targetPos, movementThisFrame);

        if (Vector2.Distance(targetPos, platform.transform.position) < Vector2.kEpsilon)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }
}
