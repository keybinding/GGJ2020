using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorBehaviour : MonoBehaviour
{
    public Camera mainCamera;
    public Camera mirrorCamera;
    private Component root;

    private Vector3 GetNormal()
    {
        return root.transform.forward;
    }

    private Vector3 GetReflection()
    {
        Vector3 normal = GetNormal();
        if(Vector3.Dot(mainCamera.transform.forward, normal) >= 0) //must be negative
            return Vector3.negativeInfinity;
        Vector3 fromMainToMirror = mirrorCamera.transform.position - mainCamera.transform.position;   
        float proj_abs = Vector3.Dot(fromMainToMirror, normal); //must be negative
        if (proj_abs >= 0)
            return Vector3.negativeInfinity;
        Vector3 addition = -normal * proj_abs * 2;
        return fromMainToMirror + addition;
    }

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponentInParent(typeof(Component));
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 reflection = GetReflection();
        if (reflection.Equals(Vector3.negativeInfinity))
        {
            mirrorCamera.enabled = false;
            return;
        }
        else
        {
            mirrorCamera.enabled = true;
        }
        Debug.Log("enabled");
        mirrorCamera.transform.rotation = Quaternion.Euler(reflection);
        mirrorCamera.transform.LookAt(reflection + mirrorCamera.transform.position);
    }
}
