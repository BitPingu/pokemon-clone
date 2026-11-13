using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Classes and Components
    private PlayerInputHandler inputManager;
    private Rigidbody rb;

    //Other Variables
    [SerializeField] private float movementSpeed;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        inputManager = FindObjectOfType<PlayerInputHandler>();

    }

    private void Update()
    {
        Vector2 mov = inputManager.Movement;
        Vector3 input = new Vector3(mov.x, 0, mov.y);

        rb.velocity = input * movementSpeed;
    }
}
