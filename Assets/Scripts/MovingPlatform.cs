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

    // Start is called before the first frame update
    void Start()
    {
    }

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
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = speed * Time.deltaTime;
            platform.transform.position = Vector2.MoveTowards(platform.transform.position, targetPos, movementThisFrame);

            if (Vector2.Distance(targetPos, platform.transform.position) < Vector2.kEpsilon)
            {
                waypointIndex++;
                if (waypointIndex == waypoints.Count)
                {
                    waypointIndex = 0;
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
