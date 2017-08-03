using Microsoft.Xna.Framework;

namespace DXBall
{
    public class Level1 : Level
    {
        public Level1(Game1 game) : base(game)
        {
        }

        public override void create()
        {
            int[,] layout = {
                {1, 1, 1, 0, 0, 0, 0, 1, 1, 1},
                {1, 1, 1, 2, 2, 2, 2, 1, 1, 1},
                {1, 1, 1, 0, 0, 0, 0, 1, 1, 1}
            };
            const int distTop = 25;
            const int distLeft = 90;

            for (int r = 0; r < layout.GetLength(0); r++)
                for (int c = 0; c < layout.GetLength(1); c++)
                    if(layout[r,c] > 0)
                        addBrick(new Brick(Game, Game.spriteBatch, Game.brickTexture,
                            new Point(Brick.WIDTH * c + distLeft, Brick.HEIGHT * r + distTop), layout[r,c]));
        }
    }
}