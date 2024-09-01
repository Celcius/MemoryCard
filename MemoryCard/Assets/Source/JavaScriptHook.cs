using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class JavaScriptHook : MonoBehaviour
{
     public Renderer targetRenderer;

    public void ApplyImage(string imageData)
    {
        byte[] imageBytes = System.Convert.FromBase64String(imageData);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageBytes);
        targetRenderer.material.mainTexture = texture;
    }
}
