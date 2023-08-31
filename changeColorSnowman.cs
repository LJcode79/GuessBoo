using UnityEngine;
using System.Collections;

public class changeColorSnowman : MonoBehaviour
{

    public Color[] colors;

    public Material material;
    private int rainbowPicker;
    private Color newColor;
    private Color oldColor;
    private float delay;
    public float duration = 1.0f;

    void Start()
    {
        //material = GetComponent<Goaliesnow>();
        rainbowPicker = 0;
        newColor = material.color;
        duration = 0.0f;
    }

    void Update()
    {
        //Debug.Log("rainbowPicker = " + rainbowPicker);
        //Debug.Log(Time.time);
        duration += Time.deltaTime;
        if (duration > 1.0f)
        {
            duration = 0f;
        }
        oldColor = newColor;
        //newColor = Color.Lerp(colors[rainbowPicker], colors[rainbowPicker + 1], Time.time);
        //material.color = Color.Lerp(colors[rainbowPicker], colors[rainbowPicker + 1], Time.time);
        material.color = Color.Lerp(colors[rainbowPicker], colors[rainbowPicker + 1], duration);
        //material.SetColor("_Color", newColor);
        //material.Color.Lerp(oldColor, newColor, .05f);

        if (Time.time >= delay)
        {
            delay = Time.time + 1f;
            rainbowPicker++;
            if (rainbowPicker >= 11)
                rainbowPicker = 0;
        }
    }
}