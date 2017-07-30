using UnityEngine;

public class SinMovement : MonoBehaviour 
{

    public float freq = 1;
    public bool autoOffset = true;

    [Space]
    public Vector3 positionA;
    public Vector3 positionB;

    private float offset;

    #region monobehaviour

    void Awake () 
	{
        offset = Random.Range(0, Mathf.PI);

        UpdatePosition();
    }
	
	void Update () 
	{
        UpdatePosition();
	}

    #endregion

    [ContextMenu("Set position A")]
    void SetPositionA()
    {
        positionA = transform.localPosition;
    }

    [ContextMenu("Set position B")]
    void SetPositionB()
    {
        positionB = transform.localPosition;
    }

    void UpdatePosition()
    {
        float sin = (1f + Mathf.Sin(2 * Mathf.PI * freq * Time.time + offset)) * .5f;

        transform.localPosition = Vector3.Lerp(positionA, positionB, sin);
    }
	
}