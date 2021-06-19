using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class InputController : MonoBehaviour
{
    //private string inputSteerAxis = "Horizontal";
    //private string inputThrottleAxis = "Vertical";

    public PhotonView PV;
    public Camera playerCamera;

    //private string bomb = "Bomb";
    //private string drop = "Drop";

    public GameObject player;
    //public GameObject ballSpawn; 

    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    public Joystick joystick;
    [SerializeField]
    private Transform carDirection;
    [SerializeField]
    private GameObject touchInterface;
    [SerializeField]
    private Canvas canvasInterface;

    public bool draggingJoystick; // Wird im Joystick gesetzt

    private float direction = 0; // 1 -> Driving forward // -1 -> Driving backwards // 0 -> no Input

    private float distance;
    
    private int keyboard = 0;
    private int touch = 1;
    private int controlMode;

    public bool allowInputs = true;

    [SerializeField]
    public InputMaster controls;

    [SerializeField]
    public GameObject tireSmoke;

    private void Awake()
    {
        if (!PV.IsMine)
        {
            
            this.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            ActivateCamera();

            controlMode = touch;

            tireSmoke.GetComponent<VisualEffect>().Stop();
        }
    }

    private void ActivateCamera() // Activate only the Camera of one Player
    {
        playerCamera.gameObject.SetActive(true);
        canvasInterface.gameObject.SetActive(true);

        //DeleteAllOtherCameras();
    }

    private void DeleteAllOtherCameras()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        foreach(GameObject c in cameras)
        {
            if (!PV.IsMine)
            {
                Debug.Log(c);
                Destroy(c);
            }
        }
    }

    void FixedUpdate()
    {
        if (PV.IsMine) {
            //if (Input.anyKeyDown)
            //{
            //    if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            //    {
            //        controlMode = touch;
            //        return;
            //    }
            //    controlMode = keyboard;
            //}
            //else if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            //{
            //    controlMode = touch;
            //}

            //if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            //{
            //    controlMode = touch;
            //}

            if (controlMode == touch)
            {
                EnableTouchInterface();
                TouchControl();
            }
            else if (controlMode == keyboard)
            {
                DisableTouchInterface();
                //KeyboardControl();
            }

            //if (Input.GetButton(bomb))
            //{
            //    player.GetComponent<BombThrow>().ThrowBomb();
            //}
            //
            //if (Input.GetButton(drop))
            //{
            //    player.GetComponent<MineDrop>().DropMine();
            //}
        }
    }

    public void OnEnableTouch() // Wird vom neuen Input System aufgerufen, wenn man auf den Bildschirm tippt oder klickt
    {
        if (PV.IsMine)
        {
            controlMode = touch;
        }
    }

    private void EnableTouchInterface()
    {
        if (PV.IsMine)
        {
            touchInterface.SetActive(true);
        }
    }

    private void DisableTouchInterface()
    {
        if (PV.IsMine)
        {
            touchInterface.SetActive(false);
        }
    }

    private float map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

    private float lerp(float start, float end, float amt)
    {
        return (1 - amt) * start + amt * end;
    }

    public void Gas()
    {
        if (PV.IsMine && allowInputs)
        {
            //Debug.Log("Gas Pressed");
            direction = 1;
            tireSmoke.GetComponent<VisualEffect>().Play();
        }
    }

    public void Reverse()
    {
        if (PV.IsMine && allowInputs)
        {
            //Debug.Log("Reverse Pressed");
            direction = -1;
            tireSmoke.GetComponent<VisualEffect>().Play();
        }
    }

    public void ResetDirection()
    {
        if (PV.IsMine)
        {
            //Debug.Log("Reset");
            direction = 0;
            ThrottleInput = 0;
            tireSmoke.GetComponent<VisualEffect>().Stop();
        }
    }

    public void ResetSteerInput()
    {
        if (PV.IsMine)
        {
            //Debug.Log("Reset");
            SteerInput = 0;
        }
    }

    //private void KeyboardControl(InputValue input)
    //{
    //    if (PV.IsMine)
    //    {
    //        Vector2 inputVec = input.Get<Vector2>();
    //
    //        SteerInput = inputVec.x;
    //        ThrottleInput = inputVec.y;
    //        //controls.Player.Movement.ReadValue<Vector2>();
    //        //SteerInput = InputSystem.GetAxis(inputSteerAxis);
    //        //ThrottleInput = Input.GetAxis(inputThrottleAxis);
    //    }
    //}

    public void OnMovement(InputValue input) // Wird getriggert, wenn man WASD drückt -> wird vom neuen Input System aufgerufen
    {
        //Debug.Log("Movement");
        //Debug.Log(PV.Owner);
        if (PV.IsMine && allowInputs)
        {
            controlMode = keyboard;
            //Debug.Log("Movement");
            Vector2 inputVec = input.Get<Vector2>();

            //Debug.Log(inputVec);
            SteerInput = inputVec.x;
            ThrottleInput = inputVec.y;
            

            if(ThrottleInput == 0)
            {
                tireSmoke.GetComponent<VisualEffect>().Stop();
            }
            else
            {
                tireSmoke.GetComponent<VisualEffect>().Play();
            }
        }
    }

    //public void OnBomb() // Wird vom neuen Input System aufgerufen, wenn die left-shift Taste gedrückt wird
    //{
    //    if (PV.IsMine)
    //    {
    //        player.GetComponent<BombThrow>().ThrowBomb();
    //    }
    //}
    //
    //public void OnDrop() // Wird vom neuen Input System aufgerufen, wenn die c Taste gedrückt wird
    //{
    //    if (PV.IsMine)
    //    {
    //        player.GetComponent<MineDrop>().DropMine();
    //    }
    //}

    private void TouchControl()
    {
        if (PV.IsMine && allowInputs)
        {
            //ThrottleInput = joystick.Direction.magnitude * direction;
            ThrottleInput = direction;

            float joystickAngle = Vector2.Angle(Vector2.down, joystick.Direction);

            if (joystick.Direction.x > 0)
            {
                joystickAngle = joystickAngle * -1;
            }

            float remap = map(joystickAngle, -180, 180, 0, 360);

            remap = (remap + 180f) % 360f;

            distance = Mathf.Sqrt((carDirection.rotation.eulerAngles.y - remap) * (carDirection.rotation.eulerAngles.y - remap));
            if (carDirection.rotation.eulerAngles.y != remap && draggingJoystick)
            {
                if (remap > 180)
                {
                    if (carDirection.rotation.eulerAngles.y < 360 && carDirection.rotation.eulerAngles.y > remap || carDirection.rotation.eulerAngles.y > 0 && carDirection.rotation.eulerAngles.y < (remap - 180))
                    {
                        if (distance > 5)
                        {
                            SteerInput = -1;
                        }
                        else
                        {
                            SteerInput = map(distance, 0, 180, 0, -1);
                        }
                    }
                    else
                    {
                        if (distance > 5)
                        {
                            SteerInput = 1;
                        }
                        else
                        {
                            SteerInput = map(distance, 0, 180, 0, 1);
                        }
                    }
                }
                else
                {
                    if (carDirection.rotation.eulerAngles.y > 0 && carDirection.rotation.eulerAngles.y < remap || carDirection.rotation.eulerAngles.y < 360 && carDirection.rotation.eulerAngles.y > (remap + 180))
                    {
                        if (distance > 5)
                        {
                            SteerInput = 1;
                        }
                        else
                        {
                            SteerInput = map(distance, 0, 180, 0, 1);
                        }
                    }
                    else
                    {
                        if (distance > 5)
                        {
                            SteerInput = -1;
                        }
                        else
                        {
                            SteerInput = map(distance, 0, 180, 0, -1);
                        }
                    }
                }
            }
            else
            {
                SteerInput = 0;
            }
            //Debug.Log(joystickAngle);
            //Debug.Log(draggingJoystick);
            //Debug.Log(SteerInput);
        }
    }
}


