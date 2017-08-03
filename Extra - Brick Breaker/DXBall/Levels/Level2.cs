using Microsoft.Xna.Framework;

namespace DXBall
{
    public class Level2 : Level
    {
        public Level2(Game1 game) : base(game)
        {
        }

        public override void create()
        {
            int[,] layout = {
                {0, 0, 0, 1, 1, 1, 1, 0, 0, 0},
                {0, 0, 1, 1, 0, 0, 1, 1, 0, 0},
                {0, 1, 1, 0, 2, 2, 0, 1, 1, 0},
                {1, 1, 0, 0, 2, 2, 0, 0, 1, 1},
                {0, 1, 1, 0, 2, 2, 0, 1, 1, 0},
                {0, 0, 1, 1, 0, 0, 1, 1, 0, 0},
                {0, 0, 0, 1, 1, 1, 1, 0, 0, 0}
            };
            const int distTop = 25;
            const int distLeft = 100;

            for (int r = 0; r < layout.GetLength(0); r++)
                for (int c = 0; c < layout.GetLength(1); c++)
                    if(layout[r,c] > 0)
                        addBrick(new Brick(Game, Game.spriteBatch, Game.brickTexture,
                            new Point(Brick.WIDTH * c + distLeft, Brick.HEIGHT * r + distTop), layout[r,c]));
        }
    }
}