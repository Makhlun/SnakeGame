using System;
using System.Windows.Media;

namespace Snake_v._0._0
{

    //  Factory

    public interface ISnakeColorFactory
    {
        ImageSource Head();
        ImageSource Body();
        ImageSource DeadHead();
        ImageSource DeadBody();
    }
    [Serializable]
    public abstract class SnakeColorFactory
    {
        public abstract ISnakeColorFactory CreateSnakeColorFactory();
        public abstract SnakeColor GetColor();
    }
    [Serializable]
    public class YellowSnakeFactory : SnakeColorFactory
    {
        public override ISnakeColorFactory CreateSnakeColorFactory() => new YellowSnake();
        public override SnakeColor GetColor() => SnakeColor.Yellow;
    }
    [Serializable]
    public class GreenSnakeFactory : SnakeColorFactory
    {
        public override ISnakeColorFactory CreateSnakeColorFactory() => new GreenSnake();
        public override SnakeColor GetColor() => SnakeColor.Green;
    }
    [Serializable]
    public class PurpleSnakeFactory : SnakeColorFactory
    {
        public override ISnakeColorFactory CreateSnakeColorFactory() => new PurpleSnake();
        public override SnakeColor GetColor() => SnakeColor.Purple;
    }
    [Serializable]
    public class CyanSnakeFactory : SnakeColorFactory
    {
        public override ISnakeColorFactory CreateSnakeColorFactory() => new CyanSnake();
        public override SnakeColor GetColor() => SnakeColor.Cyan;
    }
    [Serializable]
    public class YellowSnake : ISnakeColorFactory
    {
        public ImageSource Body() => Images.BodyYellow;

        public ImageSource Head() => Images.HeadYellow;

        public ImageSource DeadBody() => Images.DeadBodyYellow;

        public ImageSource DeadHead() => Images.DeadHeadYellow;
    }
    [Serializable]
    public class GreenSnake : ISnakeColorFactory
    {
        public ImageSource Body() => Images.Body;

        public ImageSource Head() => Images.Head;

        public ImageSource DeadBody() => Images.DeadBody;

        public ImageSource DeadHead() => Images.DeadHead;
    }
    [Serializable]
    public class PurpleSnake : ISnakeColorFactory
    {
        public ImageSource Body() => Images.BodyPurp;

        public ImageSource Head() => Images.HeadPurp;

        public ImageSource DeadBody() => Images.DeadBodyPurp;

        public ImageSource DeadHead() => Images.DeadHeadPurp;
    }
    [Serializable]
    public class CyanSnake : ISnakeColorFactory
    {
        public ImageSource Body() => Images.BodyCyan;

        public ImageSource Head() => Images.HeadCyan;

        public ImageSource DeadBody() => Images.DeadBodyCyan;

        public ImageSource DeadHead() => Images.DeadHeadCyan;
    }
}
