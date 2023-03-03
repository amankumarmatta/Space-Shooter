using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public float speed = 0.1f;
    
    void Update()
    {
        Vector2 offset = meshRenderer.material.mainTextureOffset;
        meshRenderer.material.mainTextureOffset += new Vector2(0, speed * Time.deltaTime);
    }
}
