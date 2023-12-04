using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab;
    public int segmentCount = 10;
    public Transform startPoint;
    public Transform endPoint;

    void Start()
    {
        CreateRope();
    }

    void CreateRope()
    {
        Vector3 ropeSegmentSpacing = (endPoint.position - startPoint.position) / segmentCount;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 segmentPosition = startPoint.position + ropeSegmentSpacing * i;
            GameObject segment = Instantiate(ropeSegmentPrefab, segmentPosition, Quaternion.identity);
            
            // Connect the segments with Hinge Joints (except the last one)
            if (i < segmentCount - 1)
            {
                HingeJoint joint = segment.AddComponent<HingeJoint>();
                joint.connectedBody = (i == 0) ? startPoint.GetComponent<Rigidbody>() : transform.GetChild(i - 1).GetComponent<Rigidbody>();
            }

            // Attach the segment to the rope
            segment.transform.parent = transform;
        }
    }
}
