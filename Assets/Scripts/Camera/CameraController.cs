using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Movement Factors")]
    [SerializeField]
    float moveFactor;   // Modifier for camera movement speed (horizontal + vertical)
    [SerializeField]
    float zoomFactor;   // Modifier for camera zoom speed
    [Header("Movement Constraints")]
    [SerializeField]
    float moveVertMax;  // Maximum vertical movement (+= from 0)
    [SerializeField]
    float moveHorzMax;  // Maximum horizontal movement (+= from 0)
    [SerializeField]
    float zoomMin;      // Minimum zoom value
    [SerializeField]
    float zoomMax;      // Maximum zoom value

    InputAction zoom;
    InputAction moveHorz;
    InputAction moveVert;

    [Header("Movement Values")]
    [SerializeField]
    float zoomValue = 0.0f;
    [SerializeField]
    float horzMoveValue = 0.0f;
    [SerializeField]
    float vertMoveValue = 0.0f;

    void Start()
    {
        // Assign our InputActions
        zoom = InputSystem.actions.FindAction("Zoom");
        moveHorz = InputSystem.actions.FindAction("Move LeftRight");
        moveVert = InputSystem.actions.FindAction("Move UpDown");
    }

    void Update()
    {
        // Poll our camera inputs
        zoomValue = zoom.ReadValue<float>();
        horzMoveValue = moveHorz.ReadValue<float>();
        vertMoveValue = moveVert.ReadValue<float>();
        
        // Zoom the camera
        if (zoomValue != 0) {
            this.transform.Translate(zoomValue * Vector3.forward * Time.deltaTime * zoomFactor);

            if (transform.position.z > -zoomMin) {
                this.transform.Translate(Vector3.forward * (-zoomMin - this.transform.position.z));
            }

            if (transform.position.z < -zoomMax) {
                this.transform.Translate(Vector3.forward * (-zoomMax - this.transform.position.z));
            }
        }

        // Move the camera
        if (horzMoveValue != 0) {
            this.transform.Translate(horzMoveValue * Vector3.right * Time.deltaTime * moveFactor);

            if (this.transform.position.x > moveHorzMax) {
                this.transform.Translate(Vector3.right * (moveHorzMax - this.transform.position.x));
            }

            if (this.transform.position.x < -moveHorzMax) {
                this.transform.Translate(Vector3.right * (-moveHorzMax - this.transform.position.x));
            }
        }
        
        if (vertMoveValue != 0) {
            this.transform.Translate(vertMoveValue * Vector3.up * Time.deltaTime * moveFactor);

            if (this.transform.position.y > moveVertMax) {
                this.transform.Translate(Vector3.up * (moveVertMax - this.transform.position.y));
            }

            if (this.transform.position.y < - moveVertMax) {
                this.transform.Translate(Vector3.up * (-moveVertMax - this.transform.position.y));
            }
        }
    }
}
