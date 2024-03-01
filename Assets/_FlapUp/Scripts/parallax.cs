using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    [SerializeField] GameObject parallaxImage;
    [SerializeField] float parallaxScale;
    Vector3 initialLocalPosition;
    Vector3 newPosition;
    bool isFlapUp = false;
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPosition = parallaxImage.transform.localPosition;
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
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetMouseButtonUp(0))
        {
            isFlapUp = false;
        }

        parallaxImage.transform.localPosition = initialLocalPosition;
    }

    public void FlapUP()
    {
       isFlapUp = true;
    }
}
