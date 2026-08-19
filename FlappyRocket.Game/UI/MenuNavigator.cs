using Raylib_cs;

public static class MenuNavigator
{
    public static void Update(ref int selectedIndex , int itemsCount)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            selectedIndex--;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            selectedIndex++;
        }

        if(selectedIndex < 0)
        {
            selectedIndex = itemsCount - 1;
        }

        if(selectedIndex >= itemsCount)
        {
            selectedIndex = 0;
        }
    }
}