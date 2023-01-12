using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10.0f;
    public float sensitivity = 1.0f;
    public float smoothSpeed = 0.1f;
    public float selectOffset = 1.0f;

    private Vector3 lastMouse;
    private Vector3 currentEuler;
    private Vector3 desiredEuler;
    private Vector3 totalEuler;
    private bool canRotate = false;
    GameObject targetEntity = null;

    void Start()
    {
        //initialize lastMouse as center of screen
        lastMouse = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
    }

    void Update()
    {
        TryResetTarget();
        MoveCamera();
        HandleMouseClick();
        FollowTargetEntity();
        RotateCamera();
    }
    private void TryResetTarget()
    {
        if (IsMoving())
        {
            targetEntity = null;
        }
    }

    private void MoveCamera()
    {
        // Move the camera forwards/backwards
        if (Input.GetKey("w"))
            transform.position += transform.forward * speed * Time.deltaTime;
        if (Input.GetKey("s"))
            transform.position -= transform.forward * speed * Time.deltaTime;

        // Move the camera left/right
        if (Input.GetKey("a"))
            transform.position -= transform.right * speed * Time.deltaTime;
        if (Input.GetKey("d"))
            transform.position += transform.right * speed * Time.deltaTime;
    }

    private void HandleMouseClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (!IsMoving())
            {
                targetEntity = TrySelectEntity();
            }

            if (targetEntity != null)
            {
                
            }
            else
            {
                canRotate = true;
                lastMouse = Input.mousePosition;
            }
        }

        if(Input.GetMouseButtonUp(0))
        {
            canRotate = false;
        }
    }

    private void FollowTargetEntity()
    {
        if (targetEntity)
        {
            transform.position = targetEntity.transform.position + Vector3.forward * selectOffset;
            transform.LookAt(targetEntity.transform);
            totalEuler = transform.eulerAngles;
        }
    }

    private void RotateCamera()
    {
        if (canRotate)
        {
            Vector3 delta = Input.mousePosition - lastMouse;
            desiredEuler = new Vector3(-delta.y * sensitivity, delta.x * sensitivity, 0);
            currentEuler = new Vector3(
                Mathf.LerpAngle(currentEuler.x, desiredEuler.x, smoothSpeed),
                Mathf.LerpAngle(currentEuler.y, desiredEuler.y, smoothSpeed),
                Mathf.LerpAngle(currentEuler.z, desiredEuler.z, smoothSpeed));
            totalEuler += currentEuler;
            if (totalEuler.x >= 90) totalEuler.x = 90;
            if (totalEuler.x <= -90) totalEuler.x = -90;
            transform.eulerAngles = totalEuler;
            lastMouse = Input.mousePosition;
        }
    }

    private GameObject TrySelectEntity()
    {
        print("Try Select");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            GameObject selectedObject = hit.transform.gameObject;
            if (selectedObject.CompareTag("Entity"))
            {
                return selectedObject;
            }
        }
        return null;
    }

    private bool IsMoving()
    {
        return Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d");
    }
}
