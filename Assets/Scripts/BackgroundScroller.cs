using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.1f;

    Material material;
    Vector2 offset;

    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        offset = new Vector2(material.mainTextureOffset.x, material.mainTextureOffset.y);
    }

    void Update()
    {
        if (material.mainTextureOffset.y < 1.0f)
        {
            offset.y += scrollSpeed * Time.deltaTime;
            material.mainTextureOffset = offset;
        }
        else
        {
            offset.y = 0.0f;
            material.mainTextureOffset = offset;
        }
    }
}
