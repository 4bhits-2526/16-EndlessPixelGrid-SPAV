using UnityEngine;
using UnityEngine.UI;

public class DataModel : MonoBehaviour
{
    // Eingabezeile (Q6 - Q0)
    public bool[] inputLine = new bool[7];

    public bool[,] grid = new bool[10, 7];

    // UI Images für Q6 - Q0
    public Image[] inputPixels = new Image[7];

    void Update()
    {
        HandleInput();   // Feature 6: Tasten lesen
        UpdateView();    // Anzeige aktualisieren
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Toggle(0); // Q6

        if (Input.GetKeyDown(KeyCode.A))
            Toggle(1); // Q5

        if (Input.GetKeyDown(KeyCode.UpArrow))
            Toggle(2); // Q4

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Toggle(3); // Q3

        if (Input.GetKeyDown(KeyCode.DownArrow))
            Toggle(4); // Q2

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Toggle(5); // Q1

        if (Input.GetKeyDown(KeyCode.S))
            Toggle(6); // Q0
    }

    // Feld an/aus schalten
    void Toggle(int index)
    {
        inputLine[index] = !inputLine[index];
    }

 
    void UpdateView()
    {
        for (int i = 0; i < 7; i++)
        {
            if (inputLine[i])
                inputPixels[i].color = Color.white;
            else
                inputPixels[i].color = Color.black;
        }
    }
}
