public static class GameEvents
{
    public static Observer<Tile> OnTileSelected = new Observer<Tile>();
    public static Observer<string> OnLevelChange = new Observer<string>();
    public static Observer<float> OnTimeLimitChange = new Observer<float>();
}
