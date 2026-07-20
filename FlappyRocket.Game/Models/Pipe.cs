using Raylib_cs;

public class Pipe
{
    public float x = 1000f;
    public float gapY = 250f;
    public bool passed = false;
    public float gapSize = 260f;

    public float pipeGap = 260f;


    public void Update(Pipe[] pipes , float speed , int width , Random random , float pipeGap)
    {
        x -= speed * Raylib.GetFrameTime();

        if(x < -80)
            {
                x += pipes.Length * 450;
                gapY = random.Next(100 , 440);
                gapSize = pipeGap;
                passed = false;
            }
    }

    public void ResetPipes(Pipe[] pipes , float width , Random random)
    {
        for(int i = 0; i < pipes.Length; i++)
        {
            pipes[i].x = width + random.Next(100 , 400) + i * 400;
            pipes[i].gapY = random.Next(100,500);
            pipes[i].passed = false;
        }
    }

    public void Draw(Pipe pipe , float tubeDownHight , float tubeDownY , float tubeUpY)
    {
        Raylib.DrawRectangle((int)pipe.x , (int)tubeUpY , 70 , (int)pipe.gapY , Color.Gray);

        Raylib.DrawRectangle((int)pipe.x , (int)tubeDownY ,70 , (int)tubeDownHight , Color.Gray);
    }
}