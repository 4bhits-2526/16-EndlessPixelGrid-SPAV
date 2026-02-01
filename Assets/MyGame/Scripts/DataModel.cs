using UnityEngine;
using UnityEngine.UI;

public class DataModel : MonoBehaviour
{
    // Eingabezeile (Q6 - Q0)
    public bool[] inputLine = new bool[7];

    // Raster (10 Zeilen, 7 Spalten)
    public bool[,] grid = new bool[10, 7];

    // UI Images für Eingabezeile (unten)
    public Image[] inputPixels = new Image[7];

    void Update()
    {
        HandleInput();   // Toggle + D
        UpdateView();    // zeigt nur Eingabezeile
    }

    void HandleInput()
    {
        // Feature 6: Toggle Eingabe
        if (Input.GetKeyDown(KeyCode.W)) Toggle(0);         // Q6
        if (Input.GetKeyDown(KeyCode.A)) Toggle(1);         // Q5
        if (Input.GetKeyDown(KeyCode.UpArrow)) Toggle(2);   // Q4
        if (Input.GetKeyDown(KeyCode.LeftArrow)) Toggle(3); // Q3
        if (Input.GetKeyDown(KeyCode.DownArrow)) Toggle(4); // Q2
        if (Input.GetKeyDown(KeyCode.RightArrow)) Toggle(5);// Q1
        if (Input.GetKeyDown(KeyCode.S)) Toggle(6);         // Q0

        // Feature 7: D übernimmt die Zeile ins grid (FIFO)
        if (Input.GetKeyDown(KeyCode.D))
            CommitLineToGridFIFO();
    }

    void Toggle(int index)
    {
        inputLine[index] = !inputLine[index];
    }


    void CommitLineToGridFIFO()
    {
        // 1) Rasterzeilen nach oben kopieren
        for (int row = 0; row < 9; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                grid[row, col] = grid[row + 1, col];
            }
        }

        // 2) Neue Zeile unten einfügen
        for (int col = 0; col < 7; col++)
        {
            grid[9, col] = inputLine[col];
        }

        // 3) Eingabezeile zurücksetzen
        for (int i = 0; i < 7; i++)
        {
            inputLine[i] = false;
        }

        Debug.Log("FIFO ausgeführt (D gedrückt).");
        PrintGridToConsole();
    }

    // SChauen ob das FIFO richtig funktioniert
    void PrintGridToConsole()
    {
        string output = "\n--- GRID (oben->unten) ---\n";

        for (int row = 0; row < 10; row++)
        {
            output += row.ToString("00") + ": ";

            for (int col = 0; col < 7; col++)
            {
                // 1 = an/weiß, 0 = aus/schwarz
                output += grid[row, col] ? "1 " : "0 ";
            }

            output += "\n";
        }

        Debug.Log(output);
    }


    void UpdateView()
    {
        for (int i = 0; i < 7; i++)
        {
            if (inputPixels[i] != null)
                inputPixels[i].color = inputLine[i] ? Color.white : Color.black;
        }
    }
}
