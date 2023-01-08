using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace CSharpFull
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            ConsoleKeyInfo userKey = new ConsoleKeyInfo('l', ConsoleKey.L, false, false, false);

            char[,] map = ReadMap("map.txt");

            int pacmenX = 1, pacmenY = 1;
            int score = 0;

            Task.Run(() =>
            {
                while (true)
                {
                    userKey = Console.ReadKey();
                }
            });

            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Blue;
                DrawMap(map);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(52, 0);
                Console.Write($"Счёт: {score}");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(pacmenX, pacmenY);
                Console.Write('@');

                Thread.Sleep(1000);

                HandleInput(map, userKey, ref pacmenX, ref pacmenY, ref score);
            }
        }

        private static char[,] ReadMap(string location)
        {
            string[] arrStr = File.ReadAllLines(location);
            char[,] map = new char[arrStr.Length, MaxLength(arrStr)];
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = arrStr[x][y];
                }
            }

            return map;
        }

        private static void DrawMap(char[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        private static int MaxLength(string[] str)
        {
            int max = str[0].Length;
            foreach (var item in str)
            {
                if (item.Length > max)
                {
                    max = item.Length;
                }
            }
            return max;
        }

        private static void HandleInput(char[,] map, ConsoleKeyInfo keyInfo, ref int X, ref int Y, ref int score)
        {
            int[] direction = GetDirection(keyInfo);

            int nextPositionPacmenX = X + direction[1];
            int nextPositionPacmenY = Y + direction[0];

            if (map[nextPositionPacmenY, nextPositionPacmenX] != '#')
            {
                X = nextPositionPacmenX;
                Y = nextPositionPacmenY;
                if (map[nextPositionPacmenY, nextPositionPacmenX] == '.')
                {
                    map[nextPositionPacmenY, nextPositionPacmenX] = ' ';
                    score++;
                }
            }

        }

        private static int[] GetDirection(ConsoleKeyInfo keyInfo)
        {
            int[] direction = new int[] { 0, 0 };

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    direction[0] = -1;
                    break;
                case ConsoleKey.DownArrow:
                    direction[0] = 1;
                    break;
                case ConsoleKey.LeftArrow:
                    direction[1] = -1;
                    break;
                case ConsoleKey.RightArrow:
                    direction[1] = 1;
                    break;
                default:
                    break;
            }
            return direction;
        }
    }
}
