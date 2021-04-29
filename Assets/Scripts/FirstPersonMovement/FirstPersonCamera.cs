using UnityEngine;

public class FirstPersonCamera : MonoBehaviour {

    public Transform characterBody;
    public Transform characterHead;

    float sensitivityX = 1.0f;
    float sensitivityY = 1.0f;

    float rotationX = 0;
    float rotationY = 0;

    float angleYmin = -90;
    float angleYmax = 90;

    float smoothRotx = 0;
    float smoothRoty = 0;

    float smoothCoefx = 0.05f;
    float smoothCoefy = 0.05f;
	
	float verticalDelta;
	float horizontalDelta;

    void Start() 
	{
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate() 
	{
        transform.position = characterHead.position;
    }

    void Update() 
	{
        verticalDelta = Input.GetAxisRaw("Mouse Y") * sensitivityY;
        horizontalDelta = Input.GetAxisRaw("Mouse X") * sensitivityX;

		SmoothingCamera();
		LimitCamera();

        characterBody.localEulerAngles = new Vector3(0, rotationX, 0);
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }

	void SmoothingCamera()
	{
        smoothRotx = Mathf.Lerp(smoothRotx, horizontalDelta, smoothCoefx);
        smoothRoty = Mathf.Lerp(smoothRoty, verticalDelta, smoothCoefy);

        rotationX += smoothRotx;
        rotationY += smoothRoty;
	}
	
	void LimitCamera()
	{
        rotationY = Mathf.Clamp(rotationY, angleYmin, angleYmax);
	}
}