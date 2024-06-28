using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp;

Time.SetTargetFPS(60);

Window.Init(800, 480, "Hello World");

var game = new Game();
game.state = GameState.Playing;

while (!Window.ShouldClose())
{
    switch (game.state)
    {
        case GameState.Startup:
            LaunchGame();
            break;

        case GameState.Playing:
            GameLoop();
            break;

        case GameState.GameOver:
            RunGameOver();
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

void LaunchGame()
{
    throw new NotImplementedException();
}

// activates once a frame -- loop only when needed
void GameLoop()
{
}

void RunGameOver()
{
    throw new NotImplementedException();
}
