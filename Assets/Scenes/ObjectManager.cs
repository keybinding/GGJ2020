using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // Start is called before the first frame update

    HashSet<RunningObjectBehaviour> objectsInFov = new HashSet<RunningObjectBehaviour>();
    HashSet<RunningObjectBehaviour> objectsOutOfFov = new HashSet<RunningObjectBehaviour>();
    ParanormalManager _pM = null;

    void Start()
    {
        _pM = GameObject.FindWithTag("paranormalManager").GetComponent<ParanormalManager>();
    }

    public void ObjectStateCalculated(RunningObjectBehaviour obj, bool isInFOV)
    {
        if (isInFOV && !objectsInFov.Contains(obj))
        {
            objectsInFov.Add(obj);
            objectsOutOfFov.Remove(obj);
            _pM.ObjectInView(obj);
        }
        else if (!isInFOV && !objectsOutOfFov.Contains(obj))
        {
            objectsOutOfFov.Add(obj);
            objectsInFov.Remove(obj);
            _pM.ObjectOutOfView(obj);
        }
    }
}
