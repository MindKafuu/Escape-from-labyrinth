using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distance = 10.0f;
    [SerializeField] private float height = 5.0f;

    private void Start()
    {
        transform.position = target.localPosition - target.transform.forward * distance + Vector3.up * height;
        transform.LookAt(target.transform.position);
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            target.position - new Vector3(0, 0, 1) * distance + Vector3.up * height, 1.0f * Time.deltaTime);
        //transform.LookAt(target.position);

    }
}

