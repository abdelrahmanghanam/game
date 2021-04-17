using UnityEngine;
using System.Collections;

public class MainCameraFollow : MonoBehaviour
{
    Quaternion startingRotation;
    [SerializeField] private GameObject target;
    bool firstFinished;
    private void Start()
    {
        startingRotation = target.transform.rotation;
        firstFinished = false;
        
    }
    /*


    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = target.transform.TransformPoint(offsetPosition);
        }
        else
        {
            transform.position = target.transform.position + offsetPosition;
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target.transform);
        }
        else
        {
            transform.rotation = target.transform.rotation;
        }
    }
    */

    public void RotateCam(float angle, float speed,float distance)
    {
        firstFinished = false;

        StartCoroutine(rotate(35,speed));
        
        StartCoroutine(backToOrigin());
    }
    IEnumerator rotate(float rotationAmount,float speed)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * startingRotation;

        while (this.transform.rotation != finalRotation &&!firstFinished)
        {
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, finalRotation, Time.deltaTime * speed);
            yield return 0;
            
        }
        if (finalRotation==this.transform.rotation)
        {
            firstFinished = true;
        }
    }
    IEnumerator backToOrigin()
    {

        while (this.transform.rotation != target.transform.rotation)
        {
            if (firstFinished)
                this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.transform.rotation, Time.deltaTime * 5.0f);
            yield return 0;

        }
    }

}