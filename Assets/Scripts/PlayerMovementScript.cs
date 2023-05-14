using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public CharacterController controller;
    public GameObject gravityGrenade;
    public Camera playerCamera;

    public float speed = 9f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundChecker;
    public float groundDistance = 0.4f;
    public LayerMask groundMask; // Make sure that we're walking only on the ground (currently set to Everything)
    
    
    Vector3 velovity;
    bool isGrounded;

    void Start() 
    {
        isGrounded = false;
    }

    void Update()
    {
        // Update input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check if standing
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (isGrounded && velovity.y < 0f) {
            if (velovity.x > 0f) {
                velovity.x = Mathf.Max(velovity.x - 10f, 0f);
            } else {
                velovity.x = Mathf.Min(velovity.x + 10f, 0f);
            }
            if (velovity.z > 0f) {
                velovity.z = Mathf.Max(velovity.z - 10f, 0f);
            } else {
                velovity.z = Mathf.Min(velovity.z + 10f, 0f);
            }
            // velovity.x = Mathf.Max(velovity.x - 10f, 0f);
            // velovity.z = Mathf.Max(velovity.z - 10f, 0f);
            velovity.y = -2f; // Use -2 because it sometimes triggers too early
        }

        if (isGrounded && Input.GetButtonDown("Jump")) {
            velovity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        // Apply gravity
        velovity.y += gravity * Time.deltaTime; 
        controller.Move(velovity * Time.deltaTime);

        Vector3 move = transform.right * x + transform.forward * z; // Player movement from input
        if (!isGrounded) {
            speed = 12f;
        } else {
            speed = 9f;
        }
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Fire1")) {
            Instantiate(gravityGrenade, transform.position + (playerCamera.transform.forward*1.1f) + new Vector3(0f, 1.75f, 0f), transform.rotation);
        }
    }
    
    // Method for adding the force of gravity grenades
    public void AddExplosionForce(Vector3 force) {
        velovity += force;
    }

}
