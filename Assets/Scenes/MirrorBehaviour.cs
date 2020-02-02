using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehaviour : MonoBehaviour
{
    private Camera _camera;
    public Transform MirrorCam;
    public Transform PlayerCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (PlayerCam.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(dir);

        rot.eulerAngles = transform.eulerAngles - rot.eulerAngles;

        MirrorCam.rotation = rot;
    }
}
