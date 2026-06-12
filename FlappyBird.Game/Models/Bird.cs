using Raylib_cs;

public class Bird
{
    public float x = 200f;
    public float y = 360f;
    public float velocity = 0;
    public int radius = 20;


    public void Update(Bird bird , float gravity)
    {
        bird.velocity += gravity * Raylib.GetFrameTime();
        bird.y += bird.velocity * Raylib.GetFrameTime();

        if(bird.y < 0)
            {
                bird.y = 0;
            }
    }

    public void Draw()
    {
        Raylib.DrawCircle((int)x , (int)y , radius , Color.Yellow);
    }
}