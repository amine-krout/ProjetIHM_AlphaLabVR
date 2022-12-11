using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EleminateCollisionWithCharacter : MonoBehaviour
{
    public CharacterController collider;
    public BoxCollider objectCollider;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(objectCollider, collider);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
