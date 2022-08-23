using Xamarin.Forms;
using HMExtension.Xamarin;
using System;
using System.Linq;

namespace HMControls
{
    public class TintImageEffect : RoutingEffect
    {
        public TintImageEffect() : base($"{GroupName}.{Name}") { }

        public const string GroupName = "HM";
        public const string Name = "TintImageEffect";

        public Color TintColor { get; set; } = Color.Black;
    }
}