using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] GameObject gameObject_player;
    [SerializeField] Vector3 vector3_settup_camera;
    void LateUpdate()
    {
        transform.position = gameObject_player.transform.position + vector3_settup_camera;
    }
}
