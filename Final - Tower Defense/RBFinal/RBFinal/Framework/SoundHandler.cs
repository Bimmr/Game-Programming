using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RBFinal.Framework
{

    public static class SoundHandler
    {
        public static SoundEffect sfDone;
        public static SoundEffect sfDrill;

        public static Song sdMusic;

        public static void setup(TowerDefense towerDefense)
        {
            sfDone = towerDefense.Content.Load<SoundEffect>("sound/done");
            sfDrill = towerDefense.Content.Load<SoundEffect>("sound/drill");
            sdMusic = towerDefense.Content.Load<Song>("sound/music");

            MediaPlayer.Play(sdMusic);
            MediaPlayer.IsRepeating = true;
        }

    }
}
