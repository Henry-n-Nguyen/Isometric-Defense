using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float panBorderThickness = 100f;

    public float sensitivity = 10f;

    public float minX = -5f;
    public float maxX = 5f;

    public float minY = -3f;
    public float maxY = 3f;

    public float minZ = -10f;
    public float maxZ = -2f;

    public float minZoom = 1f;
    public float maxZoom = 5f;

    private Camera mainCam;
    private bool doMovement = false;

    private void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
        {
            return;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.up * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.down * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        pos.z -= scrollDelta * 1000 * sensitivity * Time.deltaTime;
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        mainCam.orthographicSize -= scrollDelta * sensitivity;
        mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize, minZoom, maxZoom);

        transform.position = pos;
    }
}
