using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Follow + minor collisions https://generalistprogrammer.com/unity/unity-2d-how-to-make-camera-follow-player/
public class CameraFollow : MonoBehaviour
{
    public Transform FollowTransform;
    public BoxCollider2D mapBounds;
    
    private float xMin, xMax, yMin, yMax;
    private float camX, camY;
    private float camOrthize;
    private float cameraRatio;
    private Camera mainCamera;

    private void Start()
    {
        xMin = mapBounds.bounds.min.x;
        xMax = mapBounds.bounds.max.x;
        yMin = mapBounds.bounds.min.y;
        yMax = mapBounds.bounds.max.y;
        mainCamera = GetComponent<Camera>();
        camOrthize = mainCamera.orthographicSize;
        cameraRatio = (xMax + camOrthize) / 2.0f;
    }

    void FixedUpdate()
    {
        camX = Mathf.Clamp(FollowTransform.position.x, xMin + cameraRatio, xMax - cameraRatio);
        camY = Mathf.Clamp(FollowTransform.position.y, yMin + camOrthize, yMax - camOrthize);
        this.transform.position = new Vector3(camX, camY, this.transform.position.z);
    }
}
