using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    CharacterController characterController;
    public string[] bag = new string[2];
    public List<string> Tray = new List<string>();
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    public List<string> Chopper = new List<string>();
    public List<string> Vessel = new List<string>();
    void Start()
    {
        bag[0] = "null";
        bag[1] = "null";
        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;


        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void OnTriggerStay(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject

        //If the GameObject has the same tag as specified, output this message in the console

        if (Input.GetButtonUp("Take"))
        {

            if (bag[0] == "null")
            {
                bag[0] = collision.gameObject.tag;
                Debug.Log("took");
            }
            else if (bag[1] == "null")
            {
                Debug.Log("took");
                bag[1] = collision.gameObject.tag;
            }
        }
        if (collision.gameObject.tag == "ChopperA")
        {
            if (Input.GetButtonUp("Chop"))
            {
                Debug.Log("Chopping!!!!!!!!!!!!!!");
                foreach (string item in bag)
                {
                    if (item != "null")
                        Chopper.Add(item);
                }
                foreach (string salad in Chopper)
                {
                    Debug.Log(salad);
                }
                Debug.Log("Chopped!!!!!!!!!!!!!!!!");
                bag[0] = "null";
                bag[1] = "null";

            }
            if (Input.GetButtonUp("Take"))
            {
                Debug.Log("Serving!!!!!!!");
                Vessel = Chopper;
                Chopper = new List<string>();
            }
        }

        if (collision.gameObject.tag == "Customer")
        {
            if (Input.GetButtonUp("Serve"))
            {
                var order = collision.gameObject;
                var items = order.GetComponent<Customer_Order>().place_order();
                Debug.Log(Vessel.Count);
                if (Vessel.Count == items.Count)
                {

                    for (int i = 0; i < Vessel.Count; i++)
                    {

                        if (Vessel[i] == items[i])
                        {
                            Debug.Log("true");
                        }

                    }
                }
            }

        }

    }
}
