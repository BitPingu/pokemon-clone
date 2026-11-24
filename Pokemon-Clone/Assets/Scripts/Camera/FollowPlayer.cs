using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private PlayerInputHandler inputhandler;

    [SerializeField] private Transform targetTransform;
    
    [Header("Movement Variables")]
    [SerializeField] private float catchupSpeed = 2f;
    [SerializeField] private float lookAheadCatchupSpeed = 3f;
    [SerializeField] private float lookAheadXPos = 1f;
    [SerializeField] private float lookAheadZPos = 1f;

    private float currentLookAheadX;
    private float currentLookAheadZ;

    private Vector3 initialOffset;
   
    private void Start()
    {
        inputhandler = FindObjectOfType<PlayerInputHandler>();

        if (targetTransform == null) return;

        SetLookTarget(targetTransform);

        initialOffset = targetTransform.position - transform.position;

    }

    private void Update()
    {
        if (targetTransform == null) return;

        //Get movement input. If x < 0, move camera more left
        //move camera towards target position
        Vector3 newPos = Vector3.Slerp(transform.position, targetTransform.position - initialOffset, Time.deltaTime * catchupSpeed);

        
        //Now move the camera more left, right, forward, or backward depending on what input the player presses. 
        //Seen in metroidvania games, if going left the camera should move further left so player can see more of their surroundings
        //Get value of X and Z input
        Vector3 input = inputhandler.Movement;

        float lookXTarget = input.x != 0 ? lookAheadXPos : 0;
        currentLookAheadX = Mathf.Lerp(currentLookAheadX, lookXTarget * Mathf.Sign(input.x), Time.deltaTime * lookAheadCatchupSpeed);

        float lookZTarget = input.y != 0 ? lookAheadZPos : 0;
        currentLookAheadZ = Mathf.Lerp(currentLookAheadZ, lookZTarget * Mathf.Sign(input.y), Time.deltaTime * lookAheadCatchupSpeed);


        //Now apply movement to Camera
        transform.position = new Vector3(newPos.x + (currentLookAheadX * Time.deltaTime), transform.position.y, newPos.z + (currentLookAheadZ * Time.deltaTime));
  
    }

    public void SetLookTarget(Transform target)
    {
        targetTransform = target;
        transform.LookAt(targetTransform);
    }


    private void OnDrawGizmos()
    {
        if (targetTransform != null) 
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(targetTransform.position, transform.position);
        }
            

        if(Application.isPlaying)
        {
            if (targetTransform == null) return;
            Gizmos.color = Color.red;
            //Drawing Look Ahead X position
            Gizmos.DrawLine(transform.position, new Vector3(targetTransform.position.x - initialOffset.x - lookAheadXPos, targetTransform.position.y - initialOffset.y, targetTransform.position.z - initialOffset.z));
            Gizmos.DrawLine(transform.position, new Vector3(targetTransform.position.x - + initialOffset.x + lookAheadXPos, targetTransform.position.y - initialOffset.y, targetTransform.position.z - initialOffset.z));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(targetTransform.position.x - initialOffset.x, targetTransform.position.y - initialOffset.y, targetTransform.position.z - initialOffset.z - lookAheadZPos));
            Gizmos.DrawLine(transform.position, new Vector3(targetTransform.position.x - initialOffset.x, targetTransform.position.y - initialOffset.y, targetTransform.position.z - initialOffset.z + lookAheadZPos));
        }
        else
        {
            Gizmos.color = Color.red;
            //Drawing Look Ahead X position
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x - lookAheadXPos, transform.position.y, transform.position.z));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + lookAheadXPos, transform.position.y, transform.position.z));

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - lookAheadZPos));
            Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + lookAheadZPos));
        }
           
    }
}
