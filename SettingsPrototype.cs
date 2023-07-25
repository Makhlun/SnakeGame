using System;

namespace Snake_v._0._0
{
    interface IClonable
    {
        Settings Clone();
    }
     
    public class SettingsPrototype: IClonable
    {
        public Settings SettingsProt;

        public SettingsPrototype()
        {
            SettingsProt = new Settings(GridSize.Middle, false, true, new GreenSnakeFactory());
        }

        public Settings Clone() => (Settings)SettingsProt.Clone();
    }
    [Serializable]
    public class Settings
    {
        public GridSize Size { get; set; }
        public bool IsToxicFood { get; set; }
        public bool IsStaticSpeed { get; set; }
        public SnakeColorFactory SnakeColor { get; set; }
        
        public Settings(GridSize size, bool isToxicFood, bool isStaticSpeed, SnakeColorFactory snakeColor)
        {
            Size = size;
            IsToxicFood = isToxicFood;
            IsStaticSpeed = isStaticSpeed;
            SnakeColor = snakeColor;
        }

        public object Clone() => MemberwiseClone();
    }

    public enum GridSize
    {
        Small = 10,
        Middle = 15,
        Big = 20,
    }

    public enum SnakeColor
    {
        Yellow,
        Green,
        Purple,
        Cyan
    }
}
