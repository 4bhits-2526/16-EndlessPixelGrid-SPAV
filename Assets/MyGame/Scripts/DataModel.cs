using UnityEngine;
using UnityEngine.UI;

public class DataModel : MonoBehaviour
{
    // Datenmodell
    public bool[] inputLine = new bool[7];
    public bool[,] grid = new bool[10, 7];

    // UI nur Rendering!
    public Image[] inputPixels = new Image[7];
    public Transform gridParent;
    private Image[] gridPixels;

    void Awake()
    {
        // Grid-Images automatisch aus dem GridLayout einsammeln
        if (gridParent != null)
        {
            gridPixels = gridParent.GetComponentsInChildren<Image>(includeInactive: true);

            // nur die Kinder images nehmen
            if (gridParent.TryGetComponent<Image>(out _))
            {
                var list = new System.Collections.Generic.List<Image>();
                foreach (var img in gridPixels)
                {
                    if (img.transform != gridParent) list.Add(img);
                }
                gridPixels = list.ToArray();
            }
        }
    }

    void Start()
    {
        RenderAll();
    }

    void Update()
    {
        HandleInput();
    }

    // Logik ändert nur Daten
    void HandleInput()
    {
        bool inputChanged = false;
        bool gridChanged = false;

        if (Input.GetKeyDown(KeyCode.W)) { Toggle(0); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.A)) { Toggle(1); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { Toggle(2); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { Toggle(3); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.DownArrow)) { Toggle(4); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { Toggle(5); inputChanged = true; }
        if (Input.GetKeyDown(KeyCode.S)) { Toggle(6); inputChanged = true; }

        if (Input.GetKeyDown(KeyCode.D))
        {
            CommitLineToGridFIFO();
            inputChanged = true;
            gridChanged = true;
        }

        // Reset mit G
        if (Input.GetKeyDown(KeyCode.G))
        {
            ResetAllData();
            inputChanged = true;
            gridChanged = true;
        }

        // rendern
        if (inputChanged) RenderInputLine();
        if (gridChanged) RenderGrid();
    }

    void Toggle(int index) => inputLine[index] = !inputLine[index];

    void CommitLineToGridFIFO()
    {
        for (int row = 0; row < 9; row++)
            for (int col = 0; col < 7; col++)
                grid[row, col] = grid[row + 1, col];

        for (int col = 0; col < 7; col++)
            grid[9, col] = inputLine[col];

        for (int i = 0; i < 7; i++)
            inputLine[i] = false;
    }

    //Reset-Logik
    void ResetAllData()
    {
        // Eingabezeile leeren
        for (int i = 0; i < 7; i++)
            inputLine[i] = false;

        // Raster leeren
        for (int row = 0; row < 10; row++)
            for (int col = 0; col < 7; col++)
                grid[row, col] = false;
    }

    // Rendering ändert nur UI
    void RenderAll()
    {
        RenderInputLine();
        RenderGrid();
    }

    void RenderInputLine()
    {
        for (int i = 0; i < 7; i++)
        {
            if (inputPixels[i] == null) continue;
            inputPixels[i].color = inputLine[i] ? Color.white : Color.black;
        }
    }

    void RenderGrid()
    {
        if (gridPixels == null || gridPixels.Length < 70) return;

        for (int row = 0; row < 10; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                int index = row * 7 + col;
                if (gridPixels[index] == null) continue;

                gridPixels[index].color = grid[row, col] ? Color.white : Color.black;
            }
        }
    }
}
