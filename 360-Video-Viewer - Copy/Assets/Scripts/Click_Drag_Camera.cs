//  Code Help Recieved From Unity Forums @:
//  https://answers.unity.com/questions/1189946/click-and-drag-to-rotate-camera-like-a-pan.html

using UnityEngine;

public class Click_Drag_Camera : MonoBehaviour
{
    public float speed = 3.5f;
    private float X;
    private float Y;

    void Update()
    {
        //  On Each Frame, Detect If The Mouse Button Is Down To Determine If A Rotation Should Occur.
        if (Input.GetMouseButton(0))
        {
            //  Recieve The Current X and Y Components Of Mouse Movement And Multiply By The Rotation Speed.
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * speed, -Input.GetAxis("Mouse X") * speed, 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }
}
