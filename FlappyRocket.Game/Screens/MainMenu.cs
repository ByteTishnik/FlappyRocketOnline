using Raylib_cs;

public class MainMenu
{
    private int selectedIndex;
    public int SelectedIndex => selectedIndex;

    public bool Update(Game game , string[] menuItems)
    {
        MenuNavigator.Update(ref selectedIndex , menuItems.Length);

        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            switch (selectedIndex)
            {
                case 0:
                    game.state = GameState.Playing;
                break;

                case 1:
                    game.state = GameState.DifficultyMenu;
                break;

                case 2:
                    game.state = GameState.Setting;
                break;

                case 3:
                    game.state = GameState.LeaderboardMenu;
                break;

                case 4:
                    return true;
                
            }
        }

        return false;
    }
}