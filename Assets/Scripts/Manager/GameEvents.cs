using TMPro;

public static class GameEvents
{
    public static Observer<Tile> OnTileSelected = new Observer<Tile>();
    public static Observer<int> OnLevelChange = new Observer<int>();
}
