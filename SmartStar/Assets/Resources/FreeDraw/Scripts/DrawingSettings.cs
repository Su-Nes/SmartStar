using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace FreeDraw
{
    // Helper methods used to set drawing settings
    public class DrawingSettings : MonoBehaviour
    {
        public static bool isCursorOverUI = false;
        [SerializeField] private byte transparency = 255;
        [SerializeField] private float highlightScaleMultiplier = 1.25f;

        
        private void Awake()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                button.onClick.AddListener(delegate{HighlightSelectedMarker(button.transform);});
            }
        }

        private void Start()
        {
            int randPencil = Random.Range(0, transform.childCount);
            transform.GetChild(randPencil).GetComponent<Button>().onClick.Invoke();
        }

        // Changing pen settings is easy as changing the static properties Drawable.Pen_Colour and Drawable.Pen_Width
        public void SetMarkerColour(Color new_color)
        {
            Drawable.Pen_Colour = new_color;
        }
        // new_width is radius in pixels
        public void SetMarkerWidth(int new_width)
        {
            Drawable.Pen_Width = new_width;
        }
        public void SetMarkerWidth(float new_width)
        {
            SetMarkerWidth((int)new_width);
        }

        public void SetTransparency(byte amount)
        {
            transparency = amount;
            Color32 c = Drawable.Pen_Colour;
            c.a = amount;
            Drawable.Pen_Colour = c;
        }


        // Call these these to change the pen settings
        public void SetColorR(int value)
        {
            Color32 c = Drawable.Pen_Colour;
            c.r = (byte)value;
            c.a = transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }
        public void SetColorG(int value)
        {
            Color32 c = Drawable.Pen_Colour;
            c.g = (byte)value;
            c.a = transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }
        public void SetColorB(int value)
        {
            Color32 c = Drawable.Pen_Colour;
            c.b = (byte)value;
            c.a = transparency;
            SetMarkerColour(c);
            Drawable.drawable.SetPenBrush();
        }

        public void HighlightSelectedMarker(Transform selectedMarker)
        {
            foreach (Transform marker in transform)
            {
                //selectedMarker.localScale = marker == selectedMarker ? Vector3.one * highlightScaleMultiplier : Vector3.one;
                if (marker == selectedMarker)
                {
                    marker.localScale *= highlightScaleMultiplier;
                    marker.GetComponent<Image>().raycastTarget = false;
                }
                else
                {
                    marker.localScale = Vector3.one;
                    marker.GetComponent<Image>().raycastTarget = true;
                }
            }
        }
        
        public void SetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0f));
        }

        public void PartialSetEraser()
        {
            SetMarkerColour(new Color(255f, 255f, 255f, 0.5f));
        }

        public void SetFillBrush()
        {
            Drawable.drawable.SetFillBrush();
        }
    }
}