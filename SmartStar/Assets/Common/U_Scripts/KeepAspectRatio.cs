using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepAspectRatio : MonoBehaviour
{
    // set the desired aspect ratio (the values in this example are
    // hard-coded for 16:9, but you could make them into public
    // variables instead so you can set them at design time)
    private float targetaspect;

    // determine the game window's current aspect ratio
    private float windowaspect;

    // current viewport height should be scaled by this amount
    private float scaleheight;

    // obtain camera component so we can modify its viewport
    private Camera cam;
    
    private void Awake()
    {
        targetaspect = 16.0f / 9.0f;
        windowaspect = (float)Screen.width / (float)Screen.height;
        scaleheight = windowaspect / targetaspect;
        
        cam = GetComponent<Camera>();
    }
    
    private void Update()
    {
        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            cam.rect = rect;
        } 
        else // add pillarbox
        {
            float scaleWidth = 1.0f / scaleheight;

            Rect rect = GetComponent<Camera>().rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            GetComponent<Camera>().rect = rect;
        }
    }
}