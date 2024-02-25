using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public Transform[] path;
    public Transform startPoint;

    private Camera mainCamera;



    private Color[] weatherColors; //open arr 4 colors




    private float weatherChangeInterval = 30f; //set 30s limit to timer
    private float timer = 0f;

    private void Awake()
    {
        main = this;
        mainCamera = Camera.main;



  
        weatherColors = new Color[] { Color.red, Color.blue, Color.yellow, Color.cyan }; //color options
    }

    """Eventually, the colors with correlate with the weather patterns
    """Each time a color changes, the weather changes.
 

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= weatherChangeInterval)
        {
            ChangeWeather();
            timer = 0f;
        }
    }

    private void ChangeWeather()
    {

        Color randomColor = weatherColors[Random.Range(0, weatherColors.Length)];
        mainCamera.backgroundColor = randomColor;
    }
}
