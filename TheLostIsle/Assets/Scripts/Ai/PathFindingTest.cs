using UnityEngine;
using System.Collections.Generic;

public class PathfindingTest : MonoBehaviour
{
    public PathFinder pathfinder;
    public Transform player;
    public Transform target;
    private List<Node> path;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            path = pathfinder.FindPath(player.position, target.position);

            if (path == null)
            {
                Debug.LogWarning("No valid path found!");
            }
        }

        if (path != null)
        {
            for (int i = 0; i < path.Count - 1; i++)
            {
                Debug.DrawLine(path[i].worldPosition, path[i + 1].worldPosition, Color.green, 1f);
            }
        }
    }
}
