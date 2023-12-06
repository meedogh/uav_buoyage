using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAI : MonoBehaviour
{
    private BoatController boatController;
    // Start is called before the first frame update
    void Awake()
    {
        boatController = GetComponent<BoatController>();
    }

    // Update is called once per frame
    void Update()
    {
        float forward = 0;
        float turn = 0;

        float reached = 7f;
        float distance = Vector3.Distance(transform.position, boatController.GetMoveTarget());
        if(distance > reached)
        {
            Vector3 direction = (boatController.GetMoveTarget() - transform.position).normalized;
            float dot = Vector3.Dot(transform.forward, direction);

            if (dot > 0)
            {
                forward = 1f;
            }
            else
            {
                forward = -1f;
            }
            float angle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
            if (angle > 0)
            {
                turn = 1f;
            }
            else
            {
                turn = -1f;
            }
        }
        else
        {
            forward = 0;
            turn = 0;
        }

        
        boatController.SetInput(forward, turn);
    }
}
