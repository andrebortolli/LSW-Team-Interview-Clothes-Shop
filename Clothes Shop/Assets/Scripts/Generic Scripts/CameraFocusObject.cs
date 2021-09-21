using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjectExtensions.Variables;

public class CameraFocusObject : MonoBehaviour
{
    public GameObject objectToFocus;
    public FloatVariable cameraFollowSpeed;
    private Vector3 offset;
    private float cameraZOnStart;

    private void Start()
    {
        offset = transform.position - objectToFocus.transform.position;
        cameraZOnStart = transform.position.z;
    }

    void LateUpdate()
    {
        Vector2 cameraPosition = Vector2.Lerp(transform.position, objectToFocus.transform.position + offset, Time.deltaTime * cameraFollowSpeed.Value);
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, cameraZOnStart);
    }
}
