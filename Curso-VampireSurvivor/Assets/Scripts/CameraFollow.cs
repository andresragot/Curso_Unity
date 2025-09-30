using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineFollow))]
[RequireComponent(typeof(CinemachineCamera))]
public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CinemachineCamera camera = GetComponent<CinemachineCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        camera.Target.TrackingTarget = player.transform;
    }
}
