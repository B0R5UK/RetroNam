using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float walkSpeed = 5f;
    [SerializeField]float runSpeed = 15f;
    [SerializeField] float jumpPower = 20f;

    [SerializeField] CharacterController characterController;
    [SerializeField] float verticalRotLim = 80f;

    float verticalRotation;
    float forwardMov;
    float sideMov;
    float vertMov;


    private void Update()
    {
        if (!GameController.Instance.isPaused)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0); //lewo-prawo

            //gora-dol
            verticalRotation -= Input.GetAxis("Mouse Y");
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotLim, verticalRotLim);
            Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

            vertMov += Physics.gravity.y * Time.deltaTime;

            //movement
            if (characterController.isGrounded)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    forwardMov = Input.GetAxis("Vertical") * runSpeed;
                    sideMov = Input.GetAxis("Horizontal") * runSpeed;
                }
                else
                {
                    forwardMov = Input.GetAxis("Vertical") * walkSpeed;
                    sideMov = Input.GetAxis("Horizontal") * walkSpeed;
                }

                if (Input.GetButton("Jump"))
                {
                    vertMov = jumpPower;
                }
            }

            characterController.Move(transform.rotation * new Vector3(sideMov, vertMov, forwardMov) * Time.deltaTime);

        }
    }




}
