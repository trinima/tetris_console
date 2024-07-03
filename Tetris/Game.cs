using System;
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

        public Game()
        {
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            Initialize();

            IsRunning = true;
            _lastUpdated = DateTime.Now;

            RunGameLoop();
        }

        private void RunGameLoop()
        {
            do
            {
                double elapsedMilliseconds = GetElapsedMilliseconds();
                UpdateGameObjects(elapsedMilliseconds);
                DrawGameObjects();

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

        private void DrawGameObjects()
        {
            Console.Clear();

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
        }

        private void Initialize()
        {
            _gameObjects.Add(new Area()
            {
                Width = 10,
                Height = 15,

                Shapes = new List<Shape>()
                {
                    ShapeFactory.CreatePyramid(5, 1)
                }
            });
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
