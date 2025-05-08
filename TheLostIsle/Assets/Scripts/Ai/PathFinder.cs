using UnityEngine;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour
{
    public GridManager gridManager;
    public Transform player;
    private Transform currentTarget;
    private List<Node> currentPath; // Store the current path

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Debug.Log($"Finding path from {startPos} to {targetPos}");

        Node startNode = gridManager.NodeFromWorldPoint(startPos);
        Node targetNode = gridManager.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node> { startNode };
        HashSet<Node> closedSet = new HashSet<Node>();

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                    currentNode = openSet[i];
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                currentPath = RetracePath(startNode, targetNode); // Save the path
                Debug.Log($"Path found with {currentPath.Count} nodes.");
                return currentPath;
            }

            foreach (Node neighbor in gridManager.GetNeighbors(currentNode))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newCost = currentNode.gCost + GetDistance(currentNode, neighbor);

                if (newCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);
                    neighbor.parent = currentNode;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        Debug.Log("No path found.");
        currentPath = null;
        return null;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }

    public Transform FindClosestOxygenTank()
    {
        GameObject[] oxygenTanks = GameObject.FindGameObjectsWithTag("OxygenTank");

        Transform closestTank = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject tank in oxygenTanks)
        {
            float distance = Vector3.Distance(player.position, tank.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestTank = tank.transform;
            }
        }

        return closestTank;
    }

    public void SetCurrentTarget(Transform target)
    {
        currentTarget = target;
    }

    public Transform GetCurrentTarget()
    {
        return currentTarget;
    }

    private void OnDrawGizmos()
    {
        if (currentPath == null || currentPath.Count == 0) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < currentPath.Count - 1; i++)
        {
            if (currentPath[i] != null && currentPath[i + 1] != null)
            {
                Gizmos.DrawLine(currentPath[i].worldPosition, currentPath[i + 1].worldPosition);
            }
        }
    }
}
