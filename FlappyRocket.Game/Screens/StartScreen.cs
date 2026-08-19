using Raylib_cs;

public class StartScreen
{
    public int selectedIndex;

    public bool Update(Game game , string[] startScreenItems)
    {
        MenuNavigator.Update(ref selectedIndex , startScreenItems.Length);


                    if(game.state == GameState.StartScreen && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedIndex)
                        {
                            case 0:
                                game.state = GameState.Register;
                            break;

                            case 1:
                                game.state = GameState.Login;
                            break;

                            case 2:
                                game.state = GameState.Menu;
                            break;

                            case 3:
                                return true;
                        }
                    }
                    return false;
    }
}