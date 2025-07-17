public static class GameEvents
{
    public static Observer<Tile> OnTileSelected = new Observer<Tile>();
    public static Observer<int> OnLevelChange = new Observer<int>();
    public static Observer<float> OnTimeLimitChange = new Observer<float>();
}
