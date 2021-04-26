using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;


public class MainCameraFollow : MonoBehaviour
{
    Quaternion startingRotation;
    [SerializeField] private GameObject target;
    bool firstFinished;
    bool rotating;
    bool moving;
    private bool firstFinishedMoving;
    private ColorGrading colorGradingLayer = null;
    private Bloom bloomEffect = null;
    PostProcessVolume volume;
    private Vector3 originalPosition;
    private void Start()
    {

        originalPosition = this.transform.position;
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
        volume.profile.TryGetSettings(out bloomEffect);
        colorGradingLayer.enabled.value = false;
        bloomEffect.enabled.value = false;
        startingRotation = target.transform.rotation;
        firstFinished = false;
        rotating = false;
        firstFinishedMoving = false;
        moving = false;
    }

    public void beDizzy(float duration, float bloomDegree)
    {
        StartCoroutine(dizzy(duration,bloomDegree));
    }
    public void shakeCamera(float duration, float magnitude)
    {
        StartCoroutine(shake(duration, magnitude));
    }
    public void RotateCam(float angle, float speed,float distance)
    {
        if (rotating==false)
        {
            firstFinished = false;
            rotating = true;
            StartCoroutine(rotate(angle, speed));
        }
        StartCoroutine(backToOrigin());

    }    
    public void moveCameraBack(float distance, float speed)
    {
        Vector3 desiredTarget = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - distance);
        if (moving==false)
        {
            moving= true;
            StartCoroutine(hitBack(speed,desiredTarget));
        }


    }
    IEnumerator rotate(float rotationAmount,float speed)
    {
        Quaternion finalRotation = Quaternion.Euler(0, rotationAmount, 0) * startingRotation;
        bool speedDecreased = false;
        while (this.transform.rotation != finalRotation &&!firstFinished)
        {
            if (!speedDecreased && ((this.transform.eulerAngles.y > 80 || this.transform.eulerAngles.y < -80)))
            {
                speed = speed / 10;
                speedDecreased = true;
            }
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
        if (this.transform.rotation != target.transform.rotation)
        {
            while (this.transform.rotation != target.transform.rotation)
            {
                if (firstFinished)
                    this.transform.rotation = Quaternion.Lerp(this.transform.rotation, target.transform.rotation, Time.deltaTime * 5.0f);
                if (this.transform.rotation == target.transform.rotation)
                    rotating = false;
                yield return 0;

            }
        }else
        {
            rotating = false;
        }
    }



    IEnumerator shake(float duration, float magnitude)
    {
        Vector3 originalPosition = this.transform.position;
        float elapsed = 0.0f;
        float delta = magnitude / duration;
        while (elapsed<duration)
        {
            magnitude -= Time.deltaTime * delta;
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position= new Vector3(x+originalPosition.x, y+originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        if (!moving)
            transform.position = new Vector3(originalPosition.x, originalPosition.y, transform.position.z);
    }

    IEnumerator dizzy(float duration,float bloomDegree)
    {
        colorGradingLayer.enabled.value = true;
        bloomEffect.enabled.value = true;
        float elapsed = 0.0f;
        float delta = bloomDegree / duration;
        while (elapsed < duration)
        {
            bloomDegree -= Time.deltaTime *delta;
            colorGradingLayer.gamma.value = new Vector4(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f);
            bloomEffect.intensity.value = bloomDegree;
            elapsed += Time.deltaTime;
            yield return null;
        }
        bloomEffect.enabled.value = false;
        colorGradingLayer.enabled.value = false;
    }

    IEnumerator hitBack(float speed, Vector3 target)
    {
        moving = true;
        float step = (speed / (transform.position - target).magnitude) * Time.fixedDeltaTime;
        float t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            transform.position = Vector3.Lerp(originalPosition, target, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        transform.position = target;

        step = (speed / (target - originalPosition).magnitude) * Time.fixedDeltaTime;
         t = 0;
        while (t <= 1.0f)
        {
            t += step; // Goes from 0 to 1, incrementing by step each time
            transform.position = Vector3.Lerp(target, originalPosition, t); // Move objectToMove closer to b
            yield return new WaitForFixedUpdate();         // Leave the routine and return here in the next frame
        }
        transform.position = originalPosition;
        moving = false;
    }

    
}