using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

    public Camera cam;

    void Start()
    {
        //cam = GetComponent<Camera>();
    }


    void Update()
    {
        //float y = Input.mouseScrollDelta.y;
        //Debug.Log(y);

        // If the wheel goes up it, decrement 5 from "zoomTo"
        //if (y >= 1)
        //{
        //    perspectiveZoomSpeed -= 5f;
        //    Debug.Log("Zoomed In");
        //}

        //// If the wheel goes down, increment 5 to "zoomTo"
        //else if (y >= -1)
        //{
        //    perspectiveZoomSpeed += 5f;
        //    Debug.Log("Zoomed Out");
        //}
        
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            //Debug.Log("Pinch");
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (cam.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                cam.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                cam.orthographicSize = Mathf.Max(cam.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                cam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}