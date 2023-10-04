using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform CameraExtreme;
    
    [Space]

    [Header("Middle Move")]
    [SerializeField] Transform rotationHolder;
    [SerializeField] float middleMultiplier = 10.0f;
    [SerializeField] float resetMiddleTime = 0.5f;
    [SerializeField] float timeSinceMiddle = 0f;
    [SerializeField] bool startMiddle = false;
    [Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
    Vector2 rotation = Vector2.zero;
    [SerializeField]Vector3 resetRotation;
    [SerializeField]Vector3 rotationHolderResetPosition;
    [SerializeField] float scrollSpeed;

    [Space]

    [Header("WASD")]
    [SerializeField] Transform moveHolder;
    [SerializeField] float moveSpeed;
    [SerializeField] float boost;

    // Start is called before the first frame update
    void Awake()
    {
        rotation.x += Input.GetAxisRaw("Mouse X") * middleMultiplier;
        rotation.y += Input.GetAxisRaw("Mouse Y") * middleMultiplier;

        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        rotationHolder.localRotation = xQuat * yQuat;

        ResetLook();
    }

    // Update is called once per frame
    void Update()
    {
        Middle();
        Scroll();
        Move();
        Clamp();
    }

    void Clamp()
    {
        float xClamped = Mathf.Clamp(moveHolder.position.x, -CameraExtreme.localScale.x/2 + CameraExtreme.position.x, CameraExtreme.localScale.x/2 + CameraExtreme.position.x);
        float yClamped = Mathf.Clamp(moveHolder.position.y, -CameraExtreme.localScale.y/2 + CameraExtreme.position.y, CameraExtreme.localScale.y/2 + CameraExtreme.position.y);
        float zClamped = Mathf.Clamp(moveHolder.position.z, -CameraExtreme.localScale.z/2 + CameraExtreme.position.z, CameraExtreme.localScale.z/2 + CameraExtreme.position.z);
        moveHolder.position = new Vector3(xClamped, yClamped, zClamped);
    }

    void Scroll()
    {
        if(Input.mouseScrollDelta.y == 0f) { return; }
        rotationHolder.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y * Time.deltaTime * scrollSpeed));
    }

    void Move()
    {
        Vector3 move = Vector3.zero;
        if(Input.GetKey(KeyCode.W)) { move += new Vector3(0, 0, 1 ); }
        if(Input.GetKey(KeyCode.A)) { move += new Vector3(-1, 0, 0); }
        if(Input.GetKey(KeyCode.S)) { move += new Vector3(0, 0, -1); }
        if(Input.GetKey(KeyCode.D)) { move += new Vector3(1, 0, 0 ); }

        if(move == Vector3.zero) { return; }

        float saveY = moveHolder.position.y;
        moveHolder.Translate(moveSpeed * Time.deltaTime * move);
        bool neg = (saveY - moveHolder.position.y < 0);
        if (saveY - moveHolder.position.y == 0) { return; }
        move = Mathf.Sqrt(Mathf.Abs(saveY - moveHolder.position.y)) * Vector3.up;
        if(neg) { move.y *= -1; }
        moveHolder.Translate(boost * moveSpeed * Time.deltaTime * move);
        moveHolder.position = new Vector3(moveHolder.position.x, saveY, moveHolder.position.z);

        // moveHolder
    }

    void ResetLook()
    {
        rotationHolder.rotation = Quaternion.Euler(resetRotation);
        rotation = (Vector2)resetRotation;
        rotationHolder.position = rotationHolderResetPosition;

        rotation.x = resetRotation.y;
        rotation.y = -resetRotation.x;

        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        rotationHolder.localRotation = xQuat * yQuat;
    }

    void Middle()
    {
        // Move Middle Click Look
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            rotation.x += Input.GetAxisRaw("Mouse X") * middleMultiplier;
            rotation.y += Input.GetAxisRaw("Mouse Y") * middleMultiplier;

            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

            rotationHolder.localRotation = xQuat * yQuat;
        }

        if (startMiddle)
        {
            timeSinceMiddle += Time.deltaTime;
            if (timeSinceMiddle > resetMiddleTime)
            {
                startMiddle = false;
            }
        }
        else
        {
            timeSinceMiddle = 0;
        }
        if (Input.GetMouseButtonDown(2))
        {
            if (startMiddle) // Middle mouse button
            {
                if (timeSinceMiddle <= resetMiddleTime)
                {
                    //transform.position = new Vector3(transform.position.x, 2, transform.position.z);
                    
                    ResetLook();
                }
                else
                {
                    startMiddle = true;
                    timeSinceMiddle = 0.0f;
                }
            }
            else
            {
                timeSinceMiddle = 0.0f;
                startMiddle = true;
            }
        }
    }
}
