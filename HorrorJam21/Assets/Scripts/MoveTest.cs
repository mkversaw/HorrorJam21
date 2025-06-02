using UnityEngine;
using UnityEngine.InputSystem;

public class MoveTest : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Rigidbody playerRigidBody;
    [Space]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float minYaw;
    [SerializeField] private float maxYaw;

    private void MovePlayer()
	{
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput * moveSpeed);
        playerRigidBody.linearVelocity = Vector3.zero + MoveVector;
	}

    private void MovePlayerCamera()
	{
        // use the amount the mouse has moved from its last position to determine camera rotation
        xRot -= PlayerMouseInput.y * mouseSensitivity;

        xRot = Mathf.Min(xRot, minYaw); // prevent looking too far down
        xRot = Mathf.Max(xRot, maxYaw); // prevent looking too far up

        // rotate the directio the player is facing
        transform.Rotate(0f, PlayerMouseInput.x * mouseSensitivity, 0f);

        // rotate the direction the camera is facing
        cameraTransform.localRotation = Quaternion.Euler(xRot, 0f, 0f); 
    }


    private void OnMove(InputValue value)
	{
		var v = value.Get<Vector2>();
        PlayerMovementInput = new Vector3(v.x,0,v.y);
        //MovePlayer();
    }

    private void OnLook(InputValue value)
	{
        var v = value.Get<Vector2>();
        PlayerMouseInput = v;
        //MovePlayerCamera();
    }

    void Update()
    {
        MovePlayer();
        MovePlayerCamera();
    }
}
