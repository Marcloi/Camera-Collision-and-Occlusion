using UnityEngine;

public class CameraMovement3P : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    private float cameraDistance = 5f;

    [SerializeField, Range(0f,1f)]
    private float sensitivity = 1f;

    [SerializeField]
    private float cameraColliderSize = 0.2f;

    private float yMouse;
    private float xMouse;

    // Start is called before the first frame update
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock the cursor at the center of the screen
    }

    // Update is called once per frame
    private void Update()
    {
        /*Get mouse inputs*/
        xMouse += Input.GetAxisRaw("Mouse X") * 300f * sensitivity * Time.deltaTime;
        yMouse -= Input.GetAxisRaw("Mouse Y") * 300f * sensitivity * Time.deltaTime;

        yMouse = Mathf.Clamp(yMouse, -85f, 50f); //Clamp the vertical axis 

        transform.eulerAngles = new Vector3(yMouse, xMouse); //Apply the input to move the camera

        transform.position = target.position - transform.forward * cameraDistance; //Calculate the position of the camera relative to the player

        Vector3 cameraRelativePosition = (transform.position - target.position).normalized; //Calculate the direction of the camera relative to the target

        RaycastHit raycastHit;
        if (Physics.SphereCast(target.position, cameraColliderSize, cameraRelativePosition, out raycastHit, cameraDistance + 1f)) //Check for collision
        {
            transform.position = raycastHit.point; //Apply the hit.point to our cameras transform
        }
    }

    private void OnDrawGizmos()
    {
        /*A simple debug draw to visualize the collider and the raycast at the editor*/
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, cameraColliderSize);
        Gizmos.DrawLine(target.position, transform.position);
    }
}
