using System.Numerics;

namespace AsteroidSharp.Models;

class Player
{
    private float coefficientOfFriction;
    public (int, int) Position { get; private set; }
    public Vector2 Velocity { get; private set; }
    


    public Player((int,int) pos, Vector2 vel, float cof = 1)
    {
        Position = pos;
        Velocity = vel;
        coefficientOfFriction = cof;
    }
}