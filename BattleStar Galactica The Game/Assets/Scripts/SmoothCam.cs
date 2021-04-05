using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCam : MonoBehaviour
{
    IPlayer target;
    [SerializeField]Vector3 defaultDistance = new Vector3(0f, 2f, -5f);
    [SerializeField]float distanceDamp = 5f;
    [SerializeField] float rotationDamp = 2f;
    Transform myT;
    private void Awake()
    {
        myT = transform;
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj.GetComponent<ViperMovement>() is IPlayer)
            target = (IPlayer)obj.GetComponent<ViperMovement>();
    }

    private void LateUpdate()
    {
        if (target.IsOnTacticalView())
            RotateAroundPlayer();
        else
            Follow();
    }
    void Follow()
    {
        Vector3 toPos = target.GetTransform().position + (target.GetTransform().rotation * defaultDistance);
        Vector3 curPos = Vector3.Lerp(myT.position, toPos, distanceDamp * Time.deltaTime);
        myT.position = curPos;

        Quaternion toRot = Quaternion.LookRotation(target.GetTransform().position - myT.position, target.GetTransform().up);
        Quaternion curRot = Quaternion.Slerp(myT.rotation, toRot, rotationDamp * Time.deltaTime);
        myT.rotation = curRot;
    }

    void RotateAroundPlayer()
    {
        transform.RotateAround(target.GetTransform().position, transform.right, -Input.GetAxis("Mouse Y") * target.GetRotateSpeed() * Time.deltaTime);
        transform.RotateAround(target.GetTransform().position, transform.up, -Input.GetAxis("Mouse X") * target.GetRotateSpeed() * Time.deltaTime);
        transform.LookAt(target.GetTransform(), target.GetTransform().up);
    }
}
