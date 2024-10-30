using Tetris;

namespace Tetris_Forms
{
    public partial class GameWindow : Form
    {
        private Game _game = new Game();

        public GameWindow()
        {
            _game = new Game();

            InitializeComponent();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            _game.Draw(e.Graphics);
        }

        private void GameWindow_Load(object sender, EventArgs e)
        {
            Task.Run(() => _game.Start());

            Task.Run(() =>
            {
                do
                {
                    Thread.Sleep(1000 / 30); // 30 fps
                    this.Invalidate();
                } while (true);
            });
        }
    }
}
