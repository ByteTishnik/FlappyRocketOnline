using Raylib_cs;

public class Bird
{
    public float x = 200f;
    public float y = 360f;
    public float velocity = 0;
    public int radius = 20;


    public void Update(float gravity)
    {
        velocity += gravity * Raylib.GetFrameTime();
        y += velocity * Raylib.GetFrameTime();

        if(y < 0)
            {
                y = 0;
            }
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)x , (int)y , radius , Color.Red);
    }
    
}