using UnityEngine;
using UnityEngine.UI;

public class DataModel : MonoBehaviour
{
    // Eingabezeile
    public bool[] inputLine = new bool[7];

    // Raster
    public bool[,] grid = new bool[10, 7];

    // UI Images für Q6–Q0
    public Image[] inputPixels = new Image[7];

    void Update()
    {
        UpdateView();
    }

    void UpdateView()
    {
        for (int i = 0; i < 7; i++)
        {
            if (inputLine[i])
            {
                inputPixels[i].color = Color.white;
            }
            else
            {
                inputPixels[i].color = Color.black;
            }
        }
    }
}
