using Raylib_cs;

public class SettingsMenu
{
    private int selectedIndex;
    public int SelectedIndex => selectedIndex;

    public void Update(Game game , string[] settingItems)
    {
        MenuNavigator.Update(ref selectedIndex , settingItems.Length);


                    if(game.state == GameState.Setting && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedIndex)
                        {
                            case 0:
                                if(game.language == Language.English)
                                {
                                    game.language = Language.Russian;
                                    selectedIndex = 0;
                                }
                                else if(game.language == Language.Russian)
                                {
                                    game.language = Language.English;
                                    selectedIndex = 0;
                                }

                            break;

                            case 1:
                                    game.state = GameState.Menu;
                            break;
                        }
                    }
    }
}