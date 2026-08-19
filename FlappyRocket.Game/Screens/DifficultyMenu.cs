using Raylib_cs;

public class DifficultyMenu
{
    private int selectedIndex;
    public int SelectedIndex => selectedIndex;

    public void Update(Game game , string[] difficultyItems)
    {

        MenuNavigator.Update(ref selectedIndex , difficultyItems.Length);

        if(game.state == GameState.DifficultyMenu && Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                switch(selectedIndex)
                {
                    case 0:
                       game.difficulty = Difficulty.Easy;
                       game.state = GameState.Menu;
                    break;

                    case 1:
                        game.difficulty = Difficulty.Medium;
                        game.state = GameState.Menu;
                    break;

                    case 2:
                        game.difficulty = Difficulty.Hard;
                        game.state = GameState.Menu;
                    break;

                    case 3:
                        game.difficulty = Difficulty.Dynamic;
                        game.state = GameState.Menu;
                    break;

                    case 4:
                        game.state = GameState.Menu;
                    break;
                }
            }
    }
}