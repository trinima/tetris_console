using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Game
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
            foreach (var drawable in _gameObjects)
            {
                drawable.Draw(_screenDrawer);
            }

            DrawCharacter[] buffer = _screenDrawer.DrawFrame();
            Console.SetCursorPosition(1, 1);
            //Console.WriteLine("jigglypuff");
            //Console.WriteLine(DateTime.Now);
            //for (int bufferIndex = 0; bufferIndex < buffer.Length; bufferIndex++)
            //{
            //    if (Console.ForegroundColor != buffer[bufferIndex].Color)
            //    {
            //        Console.ForegroundColor = buffer[bufferIndex].Color;
            //    }

            //    Console.Write(buffer[bufferIndex].Character);
            //}
            Console.Write(buffer.Select(x => x.Character).ToArray());
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetWindowSize(area.Width, area.Height);
            Console.SetBufferSize(area.Width, area.Height);

            _gameObjects.Add(area);
            _area = area;
            _screenDrawer = new ScreenDrawer(_area.Height, _area.Width);
            for (int i = 0; i < area.Height; i++)
            {
                Console.WriteLine("JigglypuffJigglypuff");
            }
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
    }
}
