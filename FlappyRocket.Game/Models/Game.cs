using Raylib_cs;

public class Game
{
    public int score = 0;

    public float gravity = 0;
    public float speed = 0;

    public float pipeGap = 260f;


    public GameState state = GameState.Menu;
    public Difficulty difficulty = Difficulty.Medium;
    public Language language = Language.English;

    public void Start(Game game)
    {
        
            if(game.state == GameState.Playing )
            {
                game.gravity = 900f;
                game.speed = 260f;
            }
    }

    public void ScoreSystem(Pipe pipe , Rocket rocket)
    {
        if(!pipe.passed && pipe.x + 70 < rocket.x)
            {
                score++;
                pipe.passed = true;
            }
    }

    public void Reset(
        Game game,    
        Rocket rocket, 
        Pipe[] pipes, 
        float width,  
        Random random)
    {
        if(game.state == GameState.GameOver  && Raylib.IsKeyPressed(KeyboardKey.R))
            {
                game.state = GameState.Menu;

                for(int i = 0; i < pipes.Length ; i++)
                {
                    pipes[i].x = width + random.Next(100 , 400) + i * 400;
                    pipes[i].gapY = random.Next(100 , 500);
                    pipes[i].passed = false;
                }

                rocket.x = 200f;
                rocket.y = 360f;
                rocket.velocity = 0;
                game.gravity = 0;
                game.speed = 0;
                game.score = 0;
            }
    }

    public void CheckPause()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.P))
        {
            if(state == GameState.Playing)
            {
                state = GameState.Paused;
            }
            else if(state == GameState.Paused)
            {
                state = GameState.Playing;
            }
        }
    }

    public void ApplyDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                speed = 180f;
                pipeGap = 280f;
            break;

            case Difficulty.Medium:
                speed = 260f;
                pipeGap = 220f;
            break;

            case Difficulty.Hard:
                speed = 340f;
                pipeGap = 190f;
            break;

            case Difficulty.Dynamic:
                speed = 220f + score * 5;
                pipeGap = Math.Max(150f , 250f - score * 2);
            break;
        }
    }
}