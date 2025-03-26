using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureHelper : MonoBehaviour
{
    public static List<Node> TraverseGraphToExtractLowestLeafs(RoomNode parentNode)
    {
        Queue<Node> nodesToCheck = new Queue<Node>();
        List<Node> listToReturn = new List<Node>();
        if (parentNode.ChildrenNodeList.Count == 0)
        {
            return new List<Node>() { parentNode };
        }

        foreach (var child in parentNode.ChildrenNodeList)
        {
            nodesToCheck.Enqueue(child);
        }

        while (nodesToCheck.Count > 0)
        {
            var currentNode = nodesToCheck.Dequeue();
            if (currentNode.ChildrenNodeList.Count == 0)
            {
                listToReturn.Add(currentNode);
            }
            else
            {
                foreach (var child in currentNode.ChildrenNodeList)
                {
                    nodesToCheck.Enqueue(child);
                }
            }
        }
        return listToReturn;
    }

    public static Vector2Int GenerateBottomLeftCornerBetween(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;
        int x = Random.Range(minX, maxX - (int)((maxX - minX) * (1 - pointModifier)));
        int y = Random.Range(minY, maxY - (int)((maxY - minY) * (1 - pointModifier)));

        return new Vector2Int(x, y);
    }
    
    public static Vector2Int GenerateTopRightCornerBetween(Vector2Int boundaryLeftPoint, Vector2Int boundaryRightPoint, float pointModifier, int offset)
    {
        int minX = boundaryLeftPoint.x + offset;
        int maxX = boundaryRightPoint.x - offset;
        int minY = boundaryLeftPoint.y + offset;
        int maxY = boundaryRightPoint.y - offset;

        int x = Random.Range(minX + (int)((maxX - minX) * pointModifier), maxX);
        int y = Random.Range(minY + (int)((maxY - minY) * pointModifier), maxY);

        return new Vector2Int(x, y);
    }
}
