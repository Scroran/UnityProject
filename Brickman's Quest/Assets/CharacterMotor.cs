using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour {

    //Animation du joueur
    Animation animations;

    //Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    //Input
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;


    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    //Counter
    public int jumpCounter = 0;

    public bool isDead = false;

    // Use this for initialization
    void Start () {
		animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();

	}

    bool IsGrounded()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        return (Physics.Raycast(transform.position, dwn, 0.05f));
        //return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.09f);
    }

    // Update is called once per frame
    void Update() {
        if (!isDead)
        {
            //Avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                animations.Play("walk");
            }

            //Sprint
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animations.Play("run");
            }
            //Reculer
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                animations.Play("walk");
            }

            //Rotation 
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);

            }

            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
            }

            //Stand idle
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))
            {
                animations.Play("idle");
            }

            //Jump
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                //Preparation
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                //Jump
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
                jumpCounter = 0;
            }

            //DoubleJump
            if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && jumpCounter == 0)
            {
                //Preparation
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                //Double Jump
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
                jumpCounter = 1;

            }
        }
    }
}
