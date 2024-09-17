﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game
    {
        private readonly List<IGameObject> _gameObjects = new List<IGameObject>();
        private DateTime _lastUpdated;
        private Area _area;

        public Game()
        {
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            Initialize();

            RunGameLoop();
        }

        private void RunGameLoop()
        {
            do
            {
                double elapsedMilliseconds = GetElapsedMilliseconds();
                UpdateGameObjects(elapsedMilliseconds);
                DrawGameObjects(elapsedMilliseconds );

                Thread.Sleep(40);
            } while (this.IsRunning);
        }

        private double GetElapsedMilliseconds()
        {
            DateTime now = DateTime.Now;
            var elapsed = now.Subtract(_lastUpdated);
            var elapsedMilliseconds = elapsed.TotalMilliseconds;
            _lastUpdated = now;

            return elapsedMilliseconds;
        }

        private void DrawGameObjects(double elapsedMilliseconds)
        {
            Console.Clear();
            Console.WriteLine(elapsedMilliseconds);
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Draw();
            }
        }

        private void UpdateGameObjects(double elapsedMilliseconds)
        {
            var pressedKey = GetUserInput();
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(elapsedMilliseconds, pressedKey);
            }
            _area.CheckCollision();
        }

        private void Initialize()
        {
            IsRunning = true;
            _lastUpdated = DateTime.Now;
            Console.CursorVisible = false;
            var area = new Area()
            {
                Width = 20,
                Height = 30,

                Shapes = new List<Shape>()
                {
                    ShapeFactory.CreateRightL(5, 1)
                }
            };

            Console.SetWindowSize(area.Width + 5, area.Height + 5);
            Console.SetBufferSize(area.Width + 5, area.Height + 5);

            _gameObjects.Add(area);
            _area = area;

        }

        private ConsoleKeyInfo? GetUserInput()
        {
            ConsoleKeyInfo? key = null;

            if (Console.KeyAvailable)
            {
                key = Console.ReadKey();
            }

            return key;
        }

    }
}
