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

        public Game()
        {
            IsRunning = false;
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            Initialize();

            IsRunning = true;

            do
            {
                Console.Clear();

                foreach (var gameObject in _gameObjects)
                {
                    gameObject.Update(40);
                }

                foreach (var gameObject in _gameObjects)
                {
                    gameObject.Draw();
                }

                Thread.Sleep(40);
            } while (this.IsRunning);
        }

        private void Initialize()
        {
            _gameObjects.Add(new Area()
            {
                Width = 10,
                Height = 20,

                Shapes = new List<Shape>()
                {
                    new Shape()
                    {
                        X = 5,
                        Y = 1,
                        Blocks = new[]
                        {
                            new Block() { OffsetX = 0, OffsetY = 0 },

                            new Block() { OffsetX = -1, OffsetY = 1 },
                            new Block() { OffsetX = 0, OffsetY = 1 },
                            new Block() { OffsetX = 1, OffsetY = 1 }
                        }
                    }
                }
            });
        }
    }
}
