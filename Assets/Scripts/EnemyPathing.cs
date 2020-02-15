using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    WaveConfig waveConfig;

    float moveSpeed;
    List<Transform> waypoints;
    int waypointIndex = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (!waveConfig)
        {
            return;
        }
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;

        moveSpeed = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Move()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector2 currentPos = transform.position;
            Vector2 targetPos = waypoints[waypointIndex].transform.position;
            float movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(currentPos, targetPos, movementThisFrame);

            if ((Vector2)transform.position == targetPos)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
