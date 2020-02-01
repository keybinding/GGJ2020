using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailablePlaceBehaviour : RunningObjectBehaviour
{
    private RunningObjectBehaviour _obj;
    public RunningObjectBehaviour Object
    {
        get { return _obj; }
        set
        {
            _obj = value;
            if (_obj != null)
                _obj.gameObject.transform.position = gameObject.transform.position;
        }
    }
}
