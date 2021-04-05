using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer 
{
    bool IsOnTacticalView();
    Transform GetTransform();
    float GetRotateSpeed();
}
