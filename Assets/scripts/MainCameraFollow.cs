using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;


public class MainCameraFollow : MonoBehaviour
{
    Quaternion startingRotation;
    [SerializeField] private GameObject target;
    bool firstFinished;
    bool rotating;
    private ColorGrading colorGradingLayer = null;
    PostProcessVolume volume;
    private void Start()
    {
        volume = gameObject.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out colorGradingLayer);
        colorGradingLayer.enabled.value = false;
        startingRotation = target.transform.rotation;
        firstFinished = false;
        rotating = false;
    }

    public void beDizzy(float duration)
    {
        StartCoroutine(dizzy(duration));
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
        while (elapsed<duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position= new Vector3(x+originalPosition.x, y+originalPosition.y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(originalPosition.x, originalPosition.y, transform.position.z);
    }

    IEnumerator dizzy(float duration)
    {
        colorGradingLayer.enabled.value = true;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {

            colorGradingLayer.gamma.value = new Vector4(Random.Range(0.0f,1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 0.0f);
            elapsed += Time.deltaTime;
            yield return null;
        }
        colorGradingLayer.enabled.value = false;
    }
}