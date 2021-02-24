using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 3.0f;

    public float smoothing = 1.0f;
    // The character
    public GameObject character;
    // Get the incremental value of mouse moving
    private Vector2 mouseLook;
    // Smooth the mouse moving
    private Vector2 smoothV;

    public bool LookatPlayer = false;
    public Transform playerTransform;

    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenuScript.GameisPaused == false)
        {
            // Md is mouse delta
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            // The interpolated float result between the two float values
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            // Incrementally add to the camera look
            mouseLook += smoothV;

            // Vector3.right means the x-axis
            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

            // Lock at the player
            if (LookatPlayer)
            {
                transform.LookAt(playerTransform);
            }
        }
    }
}