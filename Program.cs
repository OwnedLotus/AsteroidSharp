using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;
using Raylib_CSharp;

Time.SetTargetFPS(60);

(int, int) window = (800, 480);

Window.Init(window.Item1, window.Item2, "AsteroidSharp");

var game = new Game(window);

while (!Window.ShouldClose())
{
    switch (game.state)
    {
        case GameState.Startup:
            game.LaunchGame();
            break;

        case GameState.Playing:
            game.RunGame();
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
    Graphics.DrawFPS(10, 10);

    // Graphics.DrawText("Hello, world!", 12, 12, 20, Color.Black);

    Graphics.EndDrawing();
}

Window.Close();
