using System.Numerics;
using Raylib_cs;

class Program
{

    static bool CheckCollision(Rocket rocket , Pipe pipe , float tubeDownY , float tubeDownHeight)
    {
        Rectangle birdHitBox = new Rectangle(
        
            rocket.x - 45,
            rocket.y - 30,
            55,
            30
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

     static bool debugMenu = false;
    

    static async Task Main()
    {

        Pipe[] pipes = new Pipe[3];

        Random random = new Random();

        Game game = new Game();

        HttpClient client = new HttpClient();

        ApiService _apiService = new ApiService(client);

        UserSession session = new UserSession();

        MainMenu mainMenu = new MainMenu();

        DifficultyMenu difficultyMenu = new DifficultyMenu();

        SettingsMenu settingsMenu = new SettingsMenu();

        StartScreen startScreen = new StartScreen();

        Leaderboard leaderboardDifficulty = new Leaderboard();


         

        string[] menuItems = Array.Empty<string>();
        string[] difficultyItems = Array.Empty<string>();
        string[] settingItems = Array.Empty<string>();
        string[] startScreenItems = Array.Empty<string>();
        string[] loginScreenItems = Array.Empty<string>();
        string[] registerScreenItems = Array.Empty<string>();
        string[] leaderboardScreenItems = Array.Empty<string>();
        string[] leaderboardDifficultyItems = Array.Empty<string>();


        int selectedLoginScreenIndex = 0;
        int selectedRegisterScreenIndex = 0;
        int selectedLeaderboardScreenIndex = 0;

        

        const int width = 1280;
        const int height = 720;


        float jumpfoce = -300f;
        
        float tubeUpY = 0f;

        // Setting variables

        Raylib.InitWindow(width , height , "Flappy Bird");
        Raylib.SetTargetFPS(60);


        Font gameFont = Raylib.LoadFontEx("Assets/Fonts/PixelifySans-VariableFont_wght.ttf" , 40 , null , 0);


        Texture2D background = Raylib.LoadTexture("Assets/Sprites/Background.png");
        Texture2D title = Raylib.LoadTexture("Assets/Sprites/GameTitle.png");


        Raylib.InitAudioDevice();
        Music ambient = Raylib.LoadMusicStream("Assets/ambient.mp3");
        Raylib.PlayMusicStream(ambient);
        Raylib.SetMusicVolume(ambient , 0.5f);



        Rocket rocket = new Rocket();


        float bgX1 = 0;
        float bgX2 = background.Width;
        float bgSpeed = 50f;


        string username = "";
        string password = "";
        string loginError = "";
        string registerMessage = "";


        Task<String>? loginTask = null;

        Task<bool>? registerTask = null;

        Task<List<LeaderboardEntryDto>?> leaderboardTask = null;

        List<LeaderboardEntryDto>? leaderboardEntries = null;

        Task<bool>? addScoreTask = null;



            for(int i = 0; i < pipes.Length ; i++)
            {
                pipes[i] = new Pipe();

                pipes[i].x = width + i * Pipe.Spacing;
            }

        while (!Raylib.WindowShouldClose())
        {

            Raylib.UpdateMusicStream(ambient);


            game.Start(game);


            float dt = Raylib.GetFrameTime();

            bgX1 -= bgSpeed * dt;
            bgX2 -= bgSpeed * dt;
        

            if(bgX1 <= -background.Width)
            {
                bgX1 = bgX2 + background.Width;
            }

            if(bgX2 <= -background.Width)
            {
                bgX2 = bgX1 + background.Width;
            }


        
            if (game.state == GameState.Playing)
            {
                game.ApplyDifficulty();

                rocket.Update(game.gravity);

                foreach(Pipe pipe in pipes)
            {
                pipe.Update(pipes , game.speed , width , random , game.pipeGap);
                game.ScoreSystem(pipe , rocket);
            }
            }

            if (game.language == Language.English)
            {
            menuItems = new string[] {
                "Play",
                "Difficulty",
                "Settings",
                "Leaderboard",
                "Exit"
            };

            difficultyItems = new string[] {
                "Easy",
                "Medium",
                "Hard",
                "Dynamic",
                "Back"
            };

            settingItems = new string[] {
                $"Language: {game.language}",
                "Back"
            };

            startScreenItems = new string[]
            {
                "Register",
                "Login",
                "Play as Guest",
                "Exit"
            };

            loginScreenItems = new string[]
            {
                "Username: ",
                "Password: ",
                "Enter",
                "Back"
            };

            registerScreenItems = new string[]
            {
                "Username: ",
                "Password: ",
                "Enter",
                "Back"
            };

            leaderboardScreenItems = new string[]
            {
                "Easy",
                "Medium",
                "Hard",
                "Dynamic",
                "Back",
            };

            leaderboardDifficultyItems = new string[]
            {
                "Back"
            };

            }


            else
            {

            menuItems = new string[]
            {
                "Играть",
                "Сложность",
                "Настройки",
                "Таблица Лидеров",
                "Выход"
            };

            difficultyItems = new string[]
            {
                "Легко",
                "Средне",
                "Сложно",
                "Динамика",
                "Назад"
            };
                
       
            settingItems = new string[]
            {
            $"Язык:{game.language}",
            "Назад"
            };

            startScreenItems = new string[]
            {
                "Зарегестрироватся",
                "Войти",
                "Играть как гость",
                "Выход"
            };
            }
            

            switch (game.state)
            {
                case GameState.Menu :

                    if (mainMenu.Update(game , menuItems))
                    {
                        return;
                    }

                break;

                case GameState.DifficultyMenu :

                difficultyMenu.Update(game , difficultyItems);
                
                break;

                    case GameState.Setting:

                    settingsMenu.Update(game , settingItems);
        
                    break;

                    case GameState.StartScreen:

                    if(startScreen.Update(game , startScreenItems))
                    {
                        return;
                    }

                    break;

                    case GameState.Login:

                    MenuNavigator.Update(ref selectedLoginScreenIndex , loginScreenItems.Length);


                if(game.state == GameState.Login && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedLoginScreenIndex)
                    {

                        case 0:
                            break;


                        case 1:
                            break;


                        case 2:
                             loginTask = _apiService.LoginAsync(username , password);
                        break;


                        case 3:
                            game.state = GameState.StartScreen;
                            startScreen.selectedIndex = 0;
                            selectedLoginScreenIndex = 0;
                        break;
                    }
                    }

                    int key = Raylib.GetCharPressed();

                    while(key > 0)
                        {
                            if (selectedLoginScreenIndex == 0)
                            {
                                 username += (char)key;
                            }
                            if(selectedLoginScreenIndex == 1)
                            {
                                 password += (char)key;
                            }


                            key = Raylib.GetCharPressed();
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                            {
                                if (selectedLoginScreenIndex == 0)
                                {
                                    if(username.Length > 0)
                                    {
                                        username = username[..^1];
                                    }
                                }

                                else if (selectedLoginScreenIndex == 1)
                                    {
                                        if(password.Length > 0)
                                        {
                                            password = password[..^1];
                                        }
                                    }
                            }                        
                    break;


                    case GameState.Register:

                    if (Raylib.IsKeyPressed(KeyboardKey.Up))
                    {
                        selectedRegisterScreenIndex--;
                    }
                    if (Raylib.IsKeyPressed(KeyboardKey.Down))
                    {
                        selectedRegisterScreenIndex++;
                    }
                    if(selectedRegisterScreenIndex < 0)
                    {
                        selectedRegisterScreenIndex = registerScreenItems.Length - 1;
                    }
                    if(selectedRegisterScreenIndex >= registerScreenItems.Length)
                    {
                        selectedRegisterScreenIndex = 0;
                    }

                
                if(game.state == GameState.Register && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedRegisterScreenIndex)
                    {
                        case 0:
                            break;

                        case 1:
                            break;

                        case 2:
                            registerTask = _apiService.RegisterAsync(username , password);
                        break;

                        case 3:
                            game.state = GameState.StartScreen;
                            startScreen.selectedIndex = 0;
                            selectedRegisterScreenIndex = 0;
                        break;
                    }
                    }

                    key = Raylib.GetCharPressed();

                    while(key > 0)
                        {
                            if (selectedRegisterScreenIndex == 0)
                            {
                                 username += (char)key;
                            }
                            if(selectedRegisterScreenIndex == 1)
                            {
                                 password += (char)key;
                            }


                            key = Raylib.GetCharPressed();
                        }

                        if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
                            {
                                if (selectedRegisterScreenIndex == 0)
                                {
                                    if(username.Length > 0)
                                    {
                                        username = username[..^1];
                                    }
                                }

                                else if (selectedRegisterScreenIndex == 1)
                                    {
                                        if(password.Length > 0)
                                        {
                                            password = password[..^1];
                                        }
                                    }
                    }

                    break;

                    case GameState.LeaderboardMenu:

                    MenuNavigator.Update(ref selectedLeaderboardScreenIndex , leaderboardScreenItems.Length);                


                    if(game.state == GameState.LeaderboardMenu && Raylib.IsKeyPressed(KeyboardKey.Enter))
                    {
                        switch (selectedLeaderboardScreenIndex)
                        {
                            case 0:
                                game.leaderboardDifficulty = Difficulty.Easy;

                                leaderboardTask = _apiService.GetLeaderboardsAsync(game.leaderboardDifficulty); 

                                game.state = GameState.Leaderboard;
                            break;

                            case 1:
                                game.leaderboardDifficulty = Difficulty.Medium;

                                leaderboardTask = _apiService.GetLeaderboardsAsync(game.leaderboardDifficulty);

                                game.state = GameState.Leaderboard;
                            break;

                            case 2:
                                game.leaderboardDifficulty = Difficulty.Hard;

                                leaderboardTask = _apiService.GetLeaderboardsAsync(game.leaderboardDifficulty);

                                game.state = GameState.Leaderboard;
                            break;

                            case 3:
                                game.leaderboardDifficulty = Difficulty.Dynamic;

                                leaderboardTask = _apiService.GetLeaderboardsAsync(game.leaderboardDifficulty);

                                game.state = GameState.Leaderboard;
                            break;

                            case 4:
                                game.state = GameState.Menu;
                            break;
                        }
                    }

                    break;

                    case GameState.Leaderboard:

                    leaderboardDifficulty.Update(game , leaderboardDifficultyItems);

                    break;
            }


            game.Reset(game, rocket , pipes , width , random);
            

            if (game.state == GameState.Playing && Raylib.IsKeyPressed(KeyboardKey.Space))
            {
                rocket.velocity = jumpfoce;
            }

            if(Raylib.IsKeyPressed(KeyboardKey.B))
            {

            if (debugMenu == false)
                {
                    debugMenu = true;
                }
            
            else if(debugMenu == true)
                {
                    debugMenu = false;
                }
            }


            game.CheckPause();

            //Input



            if(rocket.y > height + 10)
            {
               game.state = GameState.GameOver;
            }


            foreach(Pipe pipe in pipes)
            {
                float tubeDownY = pipe.gapY + pipe.gapSize;
                float tubeDownHeight = height - tubeDownY;

                if (CheckCollision(rocket , pipe , tubeDownY , tubeDownHeight))
                {
                    game.state = GameState.GameOver;
                }
            }

            if (registerTask != null && registerTask.IsCompleted)
            {
                bool success = registerTask.Result;

                if (success)
                {
                    registerMessage = "Registration successful!\n\nPlease log in.";
                    password = "";
                    registerTask = null;
                }
                else
                {
                    registerMessage = "User already exists!";
                    registerTask = null;
                }
            }


            if(loginTask != null && loginTask.IsCompleted)
            {
                string token = loginTask.Result;

                if(token != null)
                {
                    session.Token = token;
                    game.state = GameState.Menu;
                }

                else
                {
                    loginError = "Incorrect username or password!";
                }

                loginTask = null;
            }

            if(leaderboardTask != null && leaderboardTask.IsCompleted)
            {
                leaderboardEntries = leaderboardTask.Result;

                leaderboardTask = null;
            }

            if(game.state == GameState.GameOver && session.IsLoggedIn && addScoreTask == null && !game.scoreSent)
            {
                addScoreTask = _apiService.AddScoreAsync(game.score , game.difficulty , session.Token);
                

                game.scoreSent = true;
            }

            if(addScoreTask != null && addScoreTask.IsCompleted)
            {
                bool success = addScoreTask.Result;

                if (success)
                {
                    Console.WriteLine("Score uploded");
                }

                addScoreTask = null;
            }
        

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.SkyBlue);


            Raylib.DrawTexture(background , (int)bgX1 , 0 , Color.White);
            Raylib.DrawTexture(background , (int)bgX2 , 0 , Color.White);


            foreach(Pipe pipe in pipes)
            {
                float tubeDownY = pipe.gapY + pipe.gapSize;
                float tubeDownHeight = height - tubeDownY;

                pipe.Draw(pipe , tubeDownHeight , tubeDownY , tubeUpY);
            }

            //Drawing pipes! 

           if(game.state == GameState.Playing || game.state == GameState.Paused || game.state == GameState.GameOver)
            {
                 rocket.Draw();
            }

            //Draw bird

            if (game.state == GameState.Menu)
            {

                Raylib.DrawTexture(title , 440 , 60 , Color.White);

               for (int i = 0 ; i < menuItems.Length; i++)
                {
                    Color color =
                    i == mainMenu.SelectedIndex ? Color.Yellow : Color.White;

                int menuStartY = 350;

                Raylib.DrawTextEx(gameFont , menuItems[i] , new Vector2(500 , menuStartY + i * 60) , 40 , 2 , color);

                }
            }

            
            if(game.state == GameState.DifficultyMenu)
            {
                for(int i = 0 ; i < difficultyItems.Length; i++)
                {
                    Color color =
                    i == difficultyMenu.SelectedIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , difficultyItems[i] , new Vector2(500 , 250 + i * 60) , 40 , 2 , color);
                }
            }

            if(game.state == GameState.Setting)
            {
                for(int i = 0; i < settingItems.Length ; i++)
                {
                    Color color = 
                    i == settingsMenu.SelectedIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , settingItems[i] , new Vector2(500 , 250 + i * 60) , 40 , 2 , color);
                }
            }

            if(game.state == GameState.StartScreen)
            {
                
                Raylib.DrawTexture(title , 440 , 60 , Color.White);

                for(int i = 0; i < startScreenItems.Length ; i++)
                {
                    Color color = 
                    i == startScreen.selectedIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , startScreenItems[i] , new Vector2(500 , 350 + i * 60) , 40 , 2 , color);
                }
            }


            if(game.state == GameState.Login)
            {
                for(int i = 0; i < loginScreenItems.Length ; i++)
                {
                    Color color = 
                    i == selectedLoginScreenIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , "Login" , new Vector2(440 , 60) , 60 , 2 , Color.White);

                    Raylib.DrawTextEx(gameFont , loginScreenItems[i] , new Vector2(500 , 350 + i * 60) , 40 , 2 , color);

                    Raylib.DrawTextEx(gameFont , username , new Vector2(750, 350) , 40 , 2 , Color.White);

                    Raylib.DrawTextEx(gameFont , new string('*' , password.Length) , new Vector2(750, 410) , 40 , 2 , Color.White);
                }
            }

            if(loginError != "" && game.state == GameState.Login)
            {
                Raylib.DrawTextEx(gameFont , loginError , new Vector2(500, 600) , 30 , 2 , Color.Red);
            }


            if(game.state == GameState.Register)
            {
                for(int i = 0; i < registerScreenItems.Length ; i++)
                {
                    Color color = 
                    i == selectedRegisterScreenIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , "Register" , new Vector2(440 , 60) , 60 , 2 , Color.White);

                    Raylib.DrawTextEx(gameFont , registerScreenItems[i] , new Vector2(500 , 350 + i * 60) , 40 , 2 , color);

                    Raylib.DrawTextEx(gameFont , username , new Vector2(750, 350) , 40 , 2 , Color.White);

                    Raylib.DrawTextEx(gameFont , new string('*' , password.Length) , new Vector2(750, 410) , 40 , 2 , Color.White);
                }
            }

            if(game.state == GameState.LeaderboardMenu)
            {
                for(int i = 0; i < leaderboardScreenItems.Length ; i++)
                {
                    Color color = 
                    i == selectedLeaderboardScreenIndex ? Color.Yellow : Color.White;

                    Raylib.DrawTextEx(gameFont , leaderboardScreenItems[i] , new Vector2(500 , 350 + i * 60) , 40 , 2 , color);

                    Raylib.DrawTextEx(gameFont , "Leaderboard" , new Vector2(440 , 60) , 60 , 2 , Color.White);
                }
            }

            if(game.state == GameState.Leaderboard)
            {
                Raylib.DrawTextEx(gameFont , "Username" , new Vector2(150, 150) , 30 , 2 , Color.Yellow);

                Raylib.DrawTextEx(gameFont , "Score" , new Vector2(550, 150) , 30 , 2 , Color.Yellow);

                Raylib.DrawTextEx(gameFont , "Date" , new Vector2(800, 150) , 30 , 2 , Color.Yellow);

            if(leaderboardEntries!= null)
                {
                    for(int i = 0 ; i < leaderboardEntries.Count ; i++)
                    {
                        LeaderboardEntryDto entry = leaderboardEntries[i];

                        Raylib.DrawTextEx(gameFont , $"{i + 1}. {entry.Username}" , new Vector2(150 , 180 + i * 40) , 30 , 2 , Color.White);

                        Raylib.DrawTextEx(gameFont , $"{entry.Score}" , new Vector2(550 , 180 + i * 40) , 30 , 2 , Color.White);

                        Raylib.DrawTextEx(gameFont , $"{entry.Date:dd.MM.yyyy}" , new Vector2(800 , 180 + i * 40) , 30 , 2 , Color.White);
                    }
                }


            }

            if(registerMessage != "" && game.state == GameState.Register)
            {
                Raylib.DrawTextEx(gameFont , registerMessage , new Vector2(500, 600) , 30 , 2 , Color.Red);
            }



            if (game.state == GameState.GameOver)
            {
               DrawCenteredText("Game over!" , Color.Red);
            }

            if(game.state == GameState.Paused)
            {
                DrawCenteredText("Pause" , Color.White);
            }

            if(game.state == GameState.Playing || game.state == GameState.Paused)
            {
                Raylib.DrawText($"Score: {game.score}" , 20 , 20 , 30 , Color.White);
            }

        //debug panel:

        if(debugMenu == true)
            {
                Raylib.DrawText($"DifficultyIndex: {difficultyMenu.SelectedIndex}",20,100,20,Color.Red);

                Raylib.DrawText($"State: {game.state}",20,130,20,Color.Red);

                Raylib.DrawText($"Selected Difficulty: {game.difficulty}",20,160,20,Color.Red);
            }
            


        //Drawing text

            Raylib.EndDrawing();
        }

        Raylib.UnloadFont(gameFont);
        Raylib.UnloadTexture(title);
        Raylib.UnloadTexture(background);
        rocket.UnLoad();

        Raylib.UnloadMusicStream(ambient);
        Raylib.CloseAudioDevice();

        Raylib.CloseWindow();
    }
}