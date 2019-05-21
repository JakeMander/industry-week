using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    //  Rotational Speed Of The Camera.
    public float turnSpeed = 50f;

    //  Define The Mouse Look Sensitivity.
    public float sensitvityX = 10f;
    public float senstivityY = 10f;

    //  Define The Default Mouse Sensitivity.
    public float defaultSensitivityX = 2f;
    public float defaultSensitivityY = 2f;

    //  Define The Range Of Camera Movement Along The Y Axis.
    public float minimumY = -60f;
    public float maximumY = 60f;

    //  Number of Frames That We Are Averaging The Mouse Movement Over.
    public int frameCounterX = 35;
    public int frameCounterY = 35;

    //  Mouse Roation Input
    public float rotationX = 0f;
    public float rotationY = 0f;

    //  Final Averages Of The Rotations Passed To A Transform Matrix
    //  For Computation Of The Camera Movement.

    private Quaternion xQuaternion;
    private Quaternion yQuaternion;

    //  Track Our Initial Rotation So The New Rotational Value
    //  Can Be Applied To This.
    private Quaternion originalRotation;

    //  Store Each Rotation That Occurs Throughout Each Averaging Pass.
    private List<float> rotArrayX = new List<float>();
    private List<float> rotArrayY = new List<float>();

    void Start()
    {
        Screen.lockCursor = true;
        originalRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Screen.lockCursor)
        {
            //  Smooth The Camera Movement In The X Axis.
            float rotationAverageX = 0f;

            //  Add The Motion Of The Mouse Movement To Our Rotation So We
            //  Can Calculate An Average in The X Axis.
            rotationX += Input.GetAxis("Mouse X") * sensitvityX;
            rotArrayX.Add(rotationX);

            //  We Have Recorded The Rotation Value For All The Frames We Need. Remove
            //  The Oldest From The List So We Can Add A New Rotation And
            //  Calculate A New Value.
            if (rotArrayX.Count >= frameCounterX)
            {
                rotArrayX.RemoveAt(0);
            }

            //  Increment Through The List And Total The Rotations In Order To Calculate The Average.
            for (int i_counterX = 0; i_counterX < rotArrayX.Count; i_counterX++)
            {
                rotationAverageX += rotArrayX[i_counterX];
            }

            rotationAverageX /= rotArrayX.Count;

            float rotationAverageY = 0f;

            rotationY += Input.GetAxis("Mouse Y") * senstivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            rotArrayY.Add(rotationY);

            //  We Have Recorded The Rotation Value For All The Frames We Need. Remove
            //  The Oldest from Te List So We Can Add A New Rotation And Calculate A
            //  New Value

            if(rotArrayY.Count >= frameCounterY)
            {
                rotArrayY.RemoveAt(0);
            }

            //  Increment Through THe List And Total The Rotations In Order To Calculate The Average.
            for (int i_counterY = 0; i_counterY < rotArrayY.Count; i_counterY++)
            {
                rotationAverageY += rotArrayY[i_counterY];
            }

            rotationAverageY /= rotArrayY.Count;

            xQuaternion = Quaternion.AngleAxis (rotationAverageX, Vector3.up);
            yQuaternion = Quaternion.AngleAxis(rotationAverageY, Vector3.left);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
    }

    private float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
        {
            angle += 360f;
        }

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return Mathf.Clamp(angle, min, max);
    }
}
