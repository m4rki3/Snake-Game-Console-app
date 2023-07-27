using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.SnakeGame;
public class Game
{
    #region Fields
    private const int fieldWidth = 40;
    private const int fieldHeight = 20;
    private const int movingTimerPeriod = 200;
    private bool isGameOver = false;
    private Snake snake = new();
    private System.Threading.Timer? timer;
    private Action? SnakeDefaultMove;
    private Dollar dollar = new();
    #endregion
    #region Methods
    public void Start()
    {
        PrintField();
        Preparation(fieldWidth, fieldHeight);
        SnakeDefaultMove = snake.MoveRight;
        dollar.SetNewPosition(snake, fieldWidth, fieldHeight);
        while (!isGameOver)
        {
            timer = new(
                (_) => MakeMove(),
                null,
                movingTimerPeriod / 2,
                movingTimerPeriod
            );
            try
            {
                SnakeDefaultMove = snake.SwitchDirection();
            }
            catch (Exception) { }
            //Thread.Sleep(movingTimerPeriod);
            //MakeMove();
            timer.Dispose();
        }
        Console.SetCursorPosition(fieldWidth / 16, fieldHeight / 2);
        Console.Write($"Game is Over. You have got {snake.DollarsGot} dollars.");
        Console.SetCursorPosition(fieldWidth, fieldHeight + 1);
    }
    private void PrintField()
    {
        int i;
        for (i = 0; i <= fieldHeight; i++)
        {
            Console.SetCursorPosition(fieldWidth, i);
            Console.Write('#');
        }
        for (i = 0; i <= fieldWidth; i = i + 2)
        {
            Console.SetCursorPosition(i, fieldHeight);
            Console.Write('#');
        }
    }
    private void MakeMove()
    {
        if (SnakeDefaultMove != null) SnakeDefaultMove();
        if (snake.AteBody() || snake.OutOfZone(fieldWidth, fieldHeight))
        {
            isGameOver = true;
            if (timer != null) timer.Dispose();
        }
        else
        {
            dollar.Show();
            snake.Show();
            TryTookDollar();
        }
    }
    private void Preparation(int left, int top)
    {
        Console.SetCursorPosition(left / 2, top / 2);
        Console.Write("3...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(left / 2, top / 2 + 1);
        Console.Write("2...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(left / 2, top / 2 + 2);
        Console.Write("1...");
        Thread.Sleep(1000);
        Console.SetCursorPosition(left / 2, top / 2);
        Console.Write("    ");
        Console.SetCursorPosition(left / 2, top / 2 + 1);
        Console.Write("    ");
        Console.SetCursorPosition(left / 2, top / 2 + 2);
        Console.Write("    ");
    }
    private void TryTookDollar()
    {
        if (SnakeTookDollar())
        {
            snake.DollarsGot++;
            snake.Position.Add(snake.PlaceForTail);
            snake.Length++;
            dollar.SetNewPosition(snake, fieldWidth, fieldHeight);
        }
    }
    private bool SnakeTookDollar()
    {
        return snake.Head == dollar.Position;
    }
    #endregion
}