using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SeasonBackgroundController : MonoBehaviour
{

    [SerializeField] GameObject backgroundColor;
    [SerializeField] Material[] bgColor;
    [SerializeField] Material[] groundMaterial;
    [SerializeField] string[] seasonText;
    [SerializeField] TMP_Text season;

    Material whatMaterial;
    Material whatGroundMaterial;
    [SerializeField] GameObject ground;
    Material currentGroundMaterial;
    Material currentMaterial;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, bgColor.Length);

        backgroundColor = this.gameObject;

        currentMaterial = backgroundColor.GetComponent<Material>();
        whatMaterial = bgColor[index];

        currentGroundMaterial = ground.GetComponent<Material>();
        whatGroundMaterial = groundMaterial[index];

        season.SetText("Its " + seasonText[index] + "!");
    }

    // Update is called once per frame
    void Update()
    {
        ChangeObjectMaterial(whatMaterial);
        ChangeGroundMaterial(whatGroundMaterial);
    }



    // Function to change the material
    public void ChangeObjectMaterial(Material material)
    {
        Renderer renderer = GetComponent<Renderer>();

        if (renderer != null && material != null)
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogError("Renderer or newMaterial is null. Make sure to assign a material and attach this script to a GameObject with a Renderer component.");
        }
    }

    public void ChangeGroundMaterial(Material material)
    {
        Renderer renderer = ground.GetComponent<Renderer>();

        if (renderer != null && material != null)
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogError("Renderer or newMaterial is null. Make sure to assign a material and attach this script to a GameObject with a Renderer component.");
        }
    }

}
