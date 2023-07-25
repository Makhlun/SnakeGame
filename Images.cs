using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake_v._0._0
{
     
    public static class Images
    {
        public readonly static ImageSource Empty = LoadImage("Empty.png");
        public readonly static ImageSource ToxicFood = LoadImage("FoodToxic.png");
        public readonly static ImageSource Food = LoadImage("Food.png");

        //green
        public readonly static ImageSource Body = LoadImage("Body.png"); 
        public readonly static ImageSource Head = LoadImage("Head.png");
        public readonly static ImageSource DeadBody = LoadImage("DeadBody.png");
        public readonly static ImageSource DeadHead = LoadImage("DeadHead.png");

        //cyan
        public readonly static ImageSource BodyCyan = LoadImageSnakes("BodyCyan.png");
        public readonly static ImageSource HeadCyan = LoadImageSnakes("HeadCyan.png");
        public readonly static ImageSource DeadBodyCyan = LoadImageSnakes("DeadBodyCyan.png");
        public readonly static ImageSource DeadHeadCyan = LoadImageSnakes("DeadHeadCyan.png");

        //yellow
        public readonly static ImageSource BodyYellow = LoadImageSnakes("BodyYellow.png");
        public readonly static ImageSource HeadYellow = LoadImageSnakes("HeadYellow.png");
        public readonly static ImageSource DeadBodyYellow = LoadImageSnakes("DeadBodyYellow.png");
        public readonly static ImageSource DeadHeadYellow = LoadImageSnakes("DeadHeadYellow.png");

        //Purple
        public readonly static ImageSource BodyPurp = LoadImageSnakes("BodyPurp.png");
        public readonly static ImageSource HeadPurp = LoadImageSnakes("HeadPurp.png");
        public readonly static ImageSource DeadBodyPurp = LoadImageSnakes("DeadBodyPurp.png");
        public readonly static ImageSource DeadHeadPurp = LoadImageSnakes("DeadHeadPurp.png");

        private static ImageSource LoadImage(string fileName)
        {
            return new BitmapImage(new Uri(("Assets/" + fileName), UriKind.Relative));
        }

        private static ImageSource LoadImageSnakes(string fileName)
        {
            return new BitmapImage(new Uri(("Assets/ColorfulSnake/" + fileName), UriKind.Relative));
        }
    }
}
