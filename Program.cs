using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp;

Time.SetTargetFPS(60);

Window.Init(800,480, "Hello World");

var game = new Game();

while (!Window.ShouldClose())
{
    switch (game.state)
    {
        case GameState.Startup:
            game.LaunchGame();
            break;
        
        case GameState.Playing:
            RunGame();
            break;
        
        case GameState.GameOver:
            break;
        
        default:
            break;
    }
    
    Graphics.BeginDrawing();
    Graphics.ClearBackground(Color.White);

    game.DrawGame();

    // Graphics.DrawText("Hello, world!", 12, 12, 20, Color.Black);

    Graphics.EndDrawing();
}

Window.Close();

// activates once a frame -- loop only when needed
void RunGame()
{

}