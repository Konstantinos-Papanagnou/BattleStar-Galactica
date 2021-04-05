using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperMovement : MonoBehaviour, IPlayer
{
    [SerializeField]
    private Thruster[] thrusters;

    [SerializeField]private const float TacticalRotation = 150f;

    private const float Speed = 2.0f;
    private const float rotateSpeed = 100f;
    private const float wingSpeed = 50f;

    private Vector3 rotate;
    
    private bool Stabilize = false;
    private float CurrentSpeed;
    private const float MaxSpeed = 10f;
    private Vector3 forward = new Vector3(-1, 0, 0);

    private bool TacticalView = false;
    void Start()
    {
        CurrentSpeed = 0f;
        
       // thrusters = GetComponents<Thruster>();
    }


    // Update is called once per frame
    void Update()
    {

        RotationHandler();
        MovementHandler();
        if (Input.GetKeyDown(KeyCode.Mouse1))
            TacticalView = true;
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            TacticalView = false;
        }


    }

    void MovementHandler()
    {
        HandleThrusters();

        if (!TacticalView)
        {
            if (Input.GetKey(KeyCode.W))
            {
                CurrentSpeed += Speed;
                foreach (Thruster t in thrusters)
                    t.ActivateThrusters();
            }
            if (Input.GetKey(KeyCode.S))
            {
                CurrentSpeed -= Speed;
            }


            if (CurrentSpeed > MaxSpeed)
                CurrentSpeed = MaxSpeed;
            else if (CurrentSpeed < -MaxSpeed)
                CurrentSpeed = -MaxSpeed;
        }
        transform.Translate(Vector3.forward * CurrentSpeed * Time.deltaTime);
    }

    void RotationHandler()
    {
        if (TacticalView)
        {
            if (Input.GetKey(KeyCode.D))          
                transform.Rotate(Vector3.up, Time.deltaTime * TacticalRotation);
            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.down, Time.deltaTime * TacticalRotation);
            if (Input.GetKey(KeyCode.W))
                transform.Rotate(Vector3.left, Time.deltaTime * TacticalRotation);
            if (Input.GetKey(KeyCode.S))
                transform.Rotate(Vector3.right, Time.deltaTime * TacticalRotation);
        }
        else
        {
            transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateSpeed);
            if (Input.GetKey(KeyCode.D))
            {
                Stabilize = false;
                rotate += Vector3.back;
            }

            if (Input.GetKey(KeyCode.A))
            {
                rotate += Vector3.forward;
                Stabilize = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                rotate = new Vector3();
                Stabilize = true;
            }

        }

        if (!Stabilize)
        {
            CorrectRotation();
            transform.Rotate(rotate, wingSpeed * Time.deltaTime);
        }
    }


    private void CorrectRotation()
    {
        if (rotate.x > 1)
            rotate.x = 1;
        else if (rotate.x < -1)
            rotate.x = -1;
        if (rotate.y > 1)
            rotate.y = 1;
        else if (rotate.y < -1)
            rotate.y = -1;
        if (rotate.z > 1)
            rotate.z = 1;
        else if (rotate.z < -1)
            rotate.z = -1; 
    }

    public bool IsOnTacticalView()
    {
        return TacticalView;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public float GetRotateSpeed()
    {
        return rotateSpeed;
    }

    void HandleThrusters()
    {
        if(Input.GetKeyUp(KeyCode.W))
            foreach (Thruster t in thrusters)
                t.ActivateThrusters(false);
    }
}
