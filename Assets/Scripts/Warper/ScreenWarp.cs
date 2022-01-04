using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWarp : MonoBehaviour
{
    Renderer[] _renderers;
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        _renderers = gameObject.GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        CheckRenderers();
    }

    void CheckRenderers()
    {
        foreach (var renderer in _renderers)
        {
            if (!renderer.isVisible)
            {
                WarpObject(renderer.gameObject);
            }
        }
    }

    void WarpObject(GameObject gameObject)
    {
        var viewportPosition = _mainCamera.WorldToViewportPoint(gameObject.transform.position);
        var newPosition = gameObject.transform.position;

        if (viewportPosition.x > 1 || viewportPosition.x < 0)
        {
            newPosition.x = -newPosition.x;
        }
        if (viewportPosition.y > 1 || viewportPosition.y < 0)
        {
            newPosition.y = -newPosition.y;
        }
        gameObject.transform.position = newPosition;
    }
}
