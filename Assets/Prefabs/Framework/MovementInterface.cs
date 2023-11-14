using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovementInterface
{
    public void RotateTowards(Vector3 direction);
    public void RotateTowards(GameObject target);
}
