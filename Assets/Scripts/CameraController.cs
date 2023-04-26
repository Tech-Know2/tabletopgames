using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform followTransform;
    public static CameraController instance;

    public float normalSpeed;
    public float fastSpeed; 
    public float moveSpeed;
    public float moveTime;
    public float rotationAmount;
    public Vector3 zoomAmount;

    public int maxZoomDistance = 150;
    public int minZoomDistance = 15;

    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newZoom;

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 rotateStartPosiiton;
    public Vector3 rotateCurrentPosition;

    void Start()
    {
        instance = this;
        newPosition = transform.position;
        newRotation = transform.rotation;
        newZoom = cameraTransform.localPosition;
    }

    void Update()
    {   
        //Follow An Object With the Mouse
        //Add this code to all moving game objects so that the camera can follow it
        /*
        public void OnMouse Down()
        {
            CameraController.instance.followTransform = transform;
        }
        */

        if(followTransform != null)
        {
            transform.position = followTransform.position;
        }else 
        {
            HandelMouseInput();
            HandelMovementInput();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            followTransform = null;
        }
    }

    void HandelMouseInput()
    {
        //Scroll With The Mouse
        if(Input.mouseScrollDelta.y != 0)
        {
            if(newZoom.y >= maxZoomDistance && Input.mouseScrollDelta.y > 0) {
                // Zooming in when already at maximum zoom distance is not allowed.
                return;
            } else if(newZoom.y <= minZoomDistance && Input.mouseScrollDelta.y < 0) {
                // Zooming out when already at minimum zoom distance is not allowed.
                return;
            } else {
                newZoom += Input.mouseScrollDelta.y * zoomAmount;
                if(newZoom.y >= maxZoomDistance) {
                    newZoom.y = maxZoomDistance - 10f;
                } else if(newZoom.y <= minZoomDistance) {
                    newZoom.y = minZoomDistance + 10f;
                }
            }
        }

        //Pan With the Mouse
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if(Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        if(Input.GetMouseButtonDown(2))
        {
            rotateStartPosiiton = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosiiton - rotateCurrentPosition;

            rotateStartPosiiton =  rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f)); 
        }
    }

    void HandelMovementInput()
    {
        //Going Faster or Slower with Shift
        if(Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed;
        }else 
        {
            moveSpeed = normalSpeed;
        }

        //Moving the Camera in All Cardinal Directions
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * moveSpeed);
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -moveSpeed);
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -moveSpeed);
        }
        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * moveSpeed);
        }
        
        //Rotation the Camera
        if(Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmount);
        }
        if(Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmount);
        }

        //Smoothing Out All Movement and Input
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * moveTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * moveTime);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, newZoom, Time.deltaTime * moveTime);
    }
}

