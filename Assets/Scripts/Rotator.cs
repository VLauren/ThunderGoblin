using UnityEngine;

public class Rotator : MonoBehaviour 
{

    public Vector3 direction;
    public float angularRotation;

    private void FixedUpdate()
    {
        transform.Rotate(direction * angularRotation * Time.fixedDeltaTime);
    }

}