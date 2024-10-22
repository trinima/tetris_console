using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game : IDisposable
    {
        private readonly List<IGameObject> _gameObjects = new List<IGameObject>();
        private Area _area;
        private IScreenDrawer _screenDrawer;
        private int _gameSpeed;

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
                UpdateGameObjects(_gameSpeed);
                DrawGameObjects();
                Thread.Sleep(_gameSpeed);
            } while (this.IsRunning && _area.IsRunning);
        }

        private void DrawGameObjects()
        {
            if (!_area.IsRunning)
            {
                return;
            }

            _screenDrawer.Clear();

            foreach (var drawable in _gameObjects)
            {
                drawable.Draw(_screenDrawer);
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
            _gameSpeed = 40;
            Console.CursorVisible = false;
            var firstShape = ShapeFactory.CreateRandomShape(5, 1);
            var area = new Area()
            {
                Width = 20,
                Height = 30,
                FallingShape = firstShape,
                IsRunning = true
            };

            Console.SetWindowSize(area.Width, area.Height);
            Console.SetBufferSize(area.Width, area.Height);

            _gameObjects.Add(area);
            _area = area;
            _screenDrawer = new ScreenDrawer(_area.Height, _area.Width);
        }

        private ConsoleKeyInfo? GetUserInput()
        {
            ConsoleKeyInfo? key = null;

            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }

            return key;
        }

        public void Dispose()
        {
            this._screenDrawer.Dispose();
        }
    }
}
