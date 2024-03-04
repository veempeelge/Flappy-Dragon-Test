using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] GameObject parallaxImage;
    float parallaxScale = 1f;

    [SerializeField] GameObject parallaxImageFloat;
     float parallaxScaleFloat = 2f;

    Vector3 initialLocalPosition;
    Vector3 initialLocalPositionFloat;
    Vector3 newPosition;
    bool isFlapUp = false;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = parallaxImage.transform.localPosition;
        initialLocalPositionFloat = parallaxImageFloat.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(isFlapUp)
        {
            initialLocalPosition.y -= parallaxScale * Time.fixedDeltaTime;
            initialLocalPositionFloat.y -= parallaxScaleFloat * Time.fixedDeltaTime;
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetMouseButtonUp(0))
        {
            isFlapUp = false;
        }

        parallaxImageFloat.transform.localPosition = initialLocalPositionFloat;
        parallaxImage.transform.localPosition = initialLocalPosition;
    }

    public void FlapUP()
    {
       isFlapUp = true;
    }
}
