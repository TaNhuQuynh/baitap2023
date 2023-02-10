using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] public float smoothing;

    Vector3 offset;

    float lowY;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;

        lowY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetCameraPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCameraPosition,smoothing*Time.deltaTime);

    }
}
