using UnityEngine;
public class FaceTheCharacter : MonoBehaviour 
{ 
    public Transform target; 

    void Update() 
    { 
        if (target != null) 
        {
            transform.position = new Vector3(0, target.position.y, 0);
            transform.LookAt(target); 
        } 
    } 
}