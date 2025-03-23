using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        initialRotation = transform.rotation; // 초기 회전값 저장
    }

    void LateUpdate()
    {
        transform.rotation = initialRotation; // 처음 회전값 유지 (회전 방지)
    }
}
