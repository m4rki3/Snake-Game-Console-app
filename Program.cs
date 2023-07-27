using System;
namespace Homework;
public class Program
{
    public static void Main()
    {
        Homework.SnakeGame.Game game = new();
        game.Start();
        Console.ReadLine();
    }
}