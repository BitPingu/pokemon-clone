using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperFunctions
{
   public static float GetGroundY(Vector3 rayStartPosition)
    {
        //Shoot ray downwards til hit ground
        RaycastHit hit;
        bool hitGround = Physics.Raycast(rayStartPosition, Vector3.down, out hit, 1000, LayerMask.GetMask("Ground"));

        if (hitGround)
        {
            return hit.point.y;
        }
        return rayStartPosition.y;
    }
}
