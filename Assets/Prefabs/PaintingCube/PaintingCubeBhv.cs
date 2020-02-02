using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingCubeBhv : MonoBehaviour
{
    public GameObject go;
    public Material invisibleMat;

    private void OnBecameInvisible()
    {
        go.GetComponent<MeshRenderer>().material = invisibleMat;
    }
}
