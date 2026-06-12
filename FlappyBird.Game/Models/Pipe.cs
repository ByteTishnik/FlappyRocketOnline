using Raylib_cs;

public class Pipe
{
    public float x = 1000f;
    public float gapY = 250f;
    public bool passed = false;
    public float gapSize = 260f;


    public void Update(float speed , int width , Random random)
    {
        x -= speed * Raylib.GetFrameTime();

        if(x < -80)
            {
                x = width;
                gapY = random.Next(100 , 440);
                passed = false;
            }
    }

    public void Draw(Pipe pipe , float tubeDownHight , float tubeDownY , float tubeUpY)
    {
        Raylib.DrawRectangle((int)pipe.x , (int)tubeUpY , 70 , (int)pipe.gapY , Color.Green);

        Raylib.DrawRectangle((int)pipe.x , (int)tubeDownY ,70 , (int)tubeDownHight , Color.Green);
    }
}