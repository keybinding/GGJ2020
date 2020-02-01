using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningObjectBehaviour : MonoBehaviour
{

    private ObjectManager _oM;
    // Start is called before the first frame update
    void Start()
    {
        _oM = GameObject.FindWithTag("objectManager").GetComponent<ObjectManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _oM.ObjectStateCalculated(this, IsInFOV());
    }

    private bool IsInFOV()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        return onScreen;
    }
}
