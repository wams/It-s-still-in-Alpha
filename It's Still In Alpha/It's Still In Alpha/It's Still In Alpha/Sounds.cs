using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace It_s_Still_In_Alpha
{
    // to use insert Sounds.play_______(Content); into the area wanted
    class Sounds
    {
        static ContentManager Content;
        static Song intro;
        static Song lIS;
        static Song credits;
        static Song win;
        static Song crash;
        static SoundEffect pickUp;
        static SoundEffect small;
        static SoundEffect big;

        public static void loadSounds(ContentManager content) 
        {
            Content = content;
            intro = Content.Load<Song>("Sounds/intro");
            lIS = Content.Load<Song>("Sounds/lostInSpace");
            credits = Content.Load<Song>("Sounds/credits");
            win = Content.Load<Song>("Sounds/win");
            crash = Content.Load<Song>("Sounds/crash");

            pickUp = Content.Load<SoundEffect>("Sounds/pick_up");
            small = Content.Load<SoundEffect>("Sounds/small_boom");
            big = Content.Load<SoundEffect>("Sounds/big_boom");
        }
        //Songs
        //will play in the back ground
        public static void playIntro()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(intro);
            MediaPlayer.IsRepeating = true;
        }
        public static void playGameSong()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(lIS);
            MediaPlayer.IsRepeating = true;
        }
        public static void playCredits()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(credits);
        }
        public static void playWin()
        {
            
            MediaPlayer.Stop();
            MediaPlayer.Play(win);
        }
        public static void playCrash()
        {
            MediaPlayer.Stop();
            MediaPlayer.Play(crash);
        }

        //Sound effects
        //will go over songs
        public static void playPickUp()
        {
            pickUp.Play();
        }
        public static void playSmallBoom()
        {
            small.Play();
        }
        public static void playBigBoom()
        {
            big.Play();
        }

    }
}
