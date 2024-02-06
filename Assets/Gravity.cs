using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = 9.8f; 

    void Update()
    {
        Vector3 gravityForce = new Vector3(0, -gravity, 0);
     
        transform.position += gravityForce * Time.deltaTime;
    }
}