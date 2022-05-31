using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingUI : MonoBehaviour
{
    public Renderer followedRenderer;

    private void Update()
    {
        if (followedRenderer)
        {
            var wantedposition = Camera.main.WorldToScreenPoint(followedRenderer.transform.position);
            if (followedRenderer.IsVisibleFrom(Camera.main)) transform.position = wantedposition;
            else transform.position = new Vector3(2000, 2000, 0);
        }
        else
        {
            transform.position = new Vector3(2000, 2000, 0);
        }
    }
}

public static class RendererExtensions
{
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
