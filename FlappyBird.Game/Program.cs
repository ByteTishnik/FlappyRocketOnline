using Raylib_cs;

class Program
{
    static bool CheckCollision(Bird bird , Pipe pipe , float tubeDownY , float tubeDownHeight)
    {
        Rectangle birdHitBox = new Rectangle(
        
            bird.x - bird.radius,
            bird.y - bird.radius,
            bird.radius * 2,
            bird.radius * 2
        );

        Rectangle TopTubeHitBox = new Rectangle(

            pipe.x,
            0,
            70,
            pipe.gapY
        );

        Rectangle BottomTubeHistBox = new Rectangle(

            pipe.x,
            tubeDownY,
            70,
            tubeDownHeight
        );

        return
             Raylib.CheckCollisionRecs(birdHitBox , TopTubeHitBox) ||
             Raylib.CheckCollisionRecs(birdHitBox , BottomTubeHistBox);
    }


    static void DrawCenteredText(string text , Color color)
    {
        int fontsize = 50;
        
        int textWidth = Raylib.MeasureText(text , fontsize);

        int x = (Raylib.GetScreenWidth() - textWidth) / 2;
        int y = (Raylib.GetScreenHeight() - fontsize) / 2;

        Raylib.DrawText(text , x , y , fontsize , color);
    }
    

    static void Main()
    {

        Bird bird = new Bird();

        Pipe[] pipes = new Pipe[3];

        Random random = new Random();

        Game game = new Game();

        string[] menuItems = {
            "Play",
            "Difficulty",
            "Settings",
            "Leaderboard",
            "Exit"
        };

        string[] difficultyItems = {
            "Easy",
            "Medium",
            "Hard",
            "Dynamic",
            "Back"
        };

        int selectedIndex = 0;
        int selectedDifficultyIndex = 0;
        

        const int width = 1280;
        const int height = 720;


        float jumpfoce = -300f;
        
        float tubeUpY = 0f;

        // Setting variables

        Raylib.InitWindow(width , height , "Flappy Bird");
        Raylib.SetTargetFPS(60);


            for(int i = 0; i < pipes.Length ; i++)
            {
                pipes[i] = new Pipe();

                pipes[i].x = width + i * 400;
            }

        while (!Raylib.WindowShouldClose())
        {

        
            game.Start(game);
        
        
            if (game.state == GameState.Playing)
            {
                bird.Update(bird , game.gravity);

                foreach(Pipe pipe in pipes)
            {
                pipe.Update(game.speed , width , random);
                game.ScoreSystem(pipe , bird);
            }
            }

            switch (game.state)
            {
                case GameState.Menu :

                    if(game.state == GameState.Menu)
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
                    selectedIndex = menuItems.Length - 1;
                }
                if(selectedIndex >= menuItems.Length)
                {
                    selectedIndex = 0;
                }
            }


            if(game.state == GameState.Menu && Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                switch (selectedIndex)
                {
                    case 0:
                        game.state = GameState.Playing;
                    break;

                    case 1:
                        game.state = GameState.DifficultyMenu;
                    break;

                    case 4:
                        return;
                }
            }

                break;

                case GameState.DifficultyMenu :

                    if(game.state == GameState.DifficultyMenu)
            {
                if (Raylib.IsKeyPressed(KeyboardKey.Up))
                {
                    selectedDifficultyIndex--;
                }
                if (Raylib.IsKeyPressed(KeyboardKey.Down))
                {
                    selectedDifficultyIndex++;
                }
                if(selectedDifficultyIndex < 0)
                {
                    selectedDifficultyIndex = difficultyItems.Length - 1;
                }
                if(selectedDifficultyIndex >= difficultyItems.Length)
                {
                    selectedDifficultyIndex = 0;
                }
            }

            if(game.state == GameState.DifficultyMenu && Raylib.IsKeyPressed(KeyboardKey.Enter))
            {
                switch(selectedDifficultyIndex)
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
                        Console.WriteLine("Back is passed");
                        game.state = GameState.Menu;
                    break;
                }
            }
                
                break;
            }



            game.Reset(game, bird , pipes , width , random);

            if (game.state == GameState.Playing && Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                bird.velocity = jumpfoce;
            }

            game.CheckPause();

            //Input



            if(bird.y > height + 10)
            {
               game.state = GameState.GameOver;
            }


            foreach(Pipe pipe in pipes)
            {
                float tubeDownY = pipe.gapY + pipe.gapSize;
                float tubeDownHeight = height - tubeDownY;

                if (CheckCollision(bird , pipe , tubeDownY , tubeDownHeight))
                {
                    game.state = GameState.GameOver;
                }
            }
            

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);


            foreach(Pipe pipe in pipes)
            {
                float tubeDownY = pipe.gapY + pipe.gapSize;
                float tubeDownHeight = height - tubeDownY;

                pipe.Draw(pipe , tubeDownHeight , tubeDownY , tubeUpY);
            }

            //Drawing pipes! 

            bird.Draw();

            //Draw bird

            if (game.state == GameState.Menu)
            {
               for (int i = 0 ; i < menuItems.Length; i++)
                {
                    Color color =
                    i == selectedIndex ? Color.Yellow : Color.Black;

                Raylib.DrawText(menuItems[i] , 500 , 250 + i * 60 , 40 , color);
                }
            }

            if(game.state == GameState.DifficultyMenu)
            {
                for(int i = 0 ; i < difficultyItems.Length; i++)
                {
                    Color color =
                    i == selectedDifficultyIndex ? Color.Yellow : Color.Black;

                    Raylib.DrawText(difficultyItems[i] , 500 , 250 + i * 60 , 40 , color);
                }
            }

            if (game.state == GameState.GameOver)
            {
               DrawCenteredText("Game over!" , Color.Red);
            }

            if(game.state == GameState.Paused)
            {
                DrawCenteredText("Pause" , Color.Black);
            }

            Raylib.DrawText("Flappy Bird" , 20 , 20 , 25 , Color.Black);
            Raylib.DrawText($"Score: {game.score}" , 20 , 60 , 30 , Color.Black);

        //debug panel:

            Raylib.DrawText($"DifficultyIndex: {selectedDifficultyIndex}",20,100,20,Color.Red);

            Raylib.DrawText($"State: {game.state}",20,130,20,Color.Red);

            Raylib.DrawText($"Selected Difficulty: {game.difficulty}",20,160,20,Color.Red);


        //Drawing text

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}