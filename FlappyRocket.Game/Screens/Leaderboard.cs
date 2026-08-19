using Raylib_cs;

public class Leaderboard
{
    private int selectedIndex;
    public int SelectedIndex => selectedIndex;

    public void Update(Game game , string[] leaderboardDifficultyItems)
    {
        MenuNavigator.Update(ref selectedIndex , leaderboardDifficultyItems.Length);
                    

                    if(game.state == GameState.Leaderboard && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedIndex)
                        {
                            case 0:
                                game.state = GameState.LeaderboardMenu;
                            break;
                        }
                    }
    }
}