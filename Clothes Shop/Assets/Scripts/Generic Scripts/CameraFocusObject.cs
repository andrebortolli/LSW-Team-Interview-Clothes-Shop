using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocusObject : MonoBehaviour
{
    public GameObject objectToFocus;
    private Vector3 offset;
    private float cameraZOnStart;

    private void Start()
    {
        offset = transform.position - objectToFocus.transform.position;
        cameraZOnStart = transform.position.z;
    }

    void LateUpdate()
    {
        Vector2 cameraPosition = Vector2.Lerp(transform.position, objectToFocus.transform.position + offset, Time.deltaTime * 5.0f);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraZOnStart);
    }
}
