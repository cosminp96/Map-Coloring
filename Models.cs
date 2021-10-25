using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapColoring
{
    /// <summary>
    /// Colors enumeration
    /// </summary>
    public enum ThreeColors
    {
        NONE,
        RED,
        GREEN,
        BLUE
    }

    /// <summary>
    /// Country class
    /// </summary>
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Country> Neighbours { get; set; }
        public List<Color> AvailableColors { get; set; }
        public Color Color { get; set; }
        public Country()
        {
            Color = new Color();
            Neighbours = new List<Country>();
            AvailableColors = new List<Color>();
        }
    }
    /// <summary>
    /// Color class
    /// </summary>
    public class Color
    {
        public ThreeColors SelectedColor { get; set; }

        public Color() { }
        public Color(ThreeColors color)
        {
            SelectedColor = color;
        }
    }
}
