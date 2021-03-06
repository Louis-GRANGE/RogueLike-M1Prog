using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tools
{
    public static Vector3 GetRandomPointInsideCollider(BoxCollider boxCollider)
    {
        Vector3 extents = boxCollider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z)
        ) + boxCollider.center;
        return boxCollider.transform.TransformPoint(point);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles, float distance)
    {
        return Quaternion.Euler(angles) * ((point - pivot) * distance) + pivot;
    }

    public static Vector3 BarycentreOfPoints(List<Vector3> points)
    {
        Vector3 barycentre = Vector3.zero;
        foreach (Vector3 point in points)
        {
            barycentre += point;
        }
        barycentre /= points.Count;
        return barycentre;
    }
}
