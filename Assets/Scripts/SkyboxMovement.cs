using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxMovement : MonoBehaviour
{
    [SerializeField]
    private float skyboxSpeed = 0.25f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyboxSpeed);
    }
}
