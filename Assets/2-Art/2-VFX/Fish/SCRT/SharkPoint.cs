using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPoint : MonoBehaviour
{
    [SerializeField] GameObject Target;

    [SerializeField] Transform Point1;
    [SerializeField] Transform Point2;

    [SerializeField] FlockingCmp flockingCmp;

    void Start()
    {
        Target.transform.position = Point1.position;
        Invoke(nameof(MoveToPointB), flockingCmp.waitTimeToActivate);
    }

    void MoveToPointB()
    {
        Target.transform.position = Point2.position;
    }
}
