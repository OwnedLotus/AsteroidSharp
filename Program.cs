using Raylib_CSharp.Windowing;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Colors;

Window.Init(800,480, "Hello World");

while (!Window.ShouldClose())
{
    Graphics.BeginDrawing();
    Graphics.ClearBackground(Color.White);

    Graphics.DrawText("Hello, world!", 12, 12, 20, Color.Black);

    Graphics.EndDrawing();
}

Window.Close();