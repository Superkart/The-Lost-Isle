using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public GridManager gridManager;

    private void OnDrawGizmos()
    {
        if (gridManager == null || gridManager.grid == null) return;

        foreach (Node node in gridManager.grid)
        {
            if (node.walkable)
                Gizmos.color = Color.white;
            else
                Gizmos.color = Color.red;

            Gizmos.DrawCube(node.worldPosition + Vector3.up * 0.1f, Vector3.one * (gridManager.nodeRadius * 1.8f));
        }
    }
}
