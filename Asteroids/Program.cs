using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp;

Time.SetTargetFPS(60);

(uint, uint) windowDimensions = (800, 480);

Window.Init((int)windowDimensions.Item1, (int)windowDimensions.Item2, "AsteroidSharp");

var game = new Game(windowDimensions);

while (!Window.ShouldClose())
{
    switch (game.state)
    {
        case GameState.Startup:
            game.LaunchGame();
            break;

        case GameState.Playing:
            game.UpdateGame();
            break;

        case GameState.GameOver:
            game.RunGameOver();
            break;
        case GameState.Paused:
            break;
    }

    Graphics.BeginDrawing();
    Graphics.ClearBackground(Color.Black);

    game.DrawGame();

#if DEBUG
    Graphics.DrawFPS(10, 10);
#endif

    Graphics.EndDrawing();
}

Window.Close();
