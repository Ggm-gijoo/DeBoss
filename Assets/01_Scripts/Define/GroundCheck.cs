using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform characterPos;
    public bool IsGrounded()
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(characterPos.position, 0.01f, Vector3.down * 0.1f, 0f);
        foreach(var ray in raycastHits)
        {
            if(ray.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
