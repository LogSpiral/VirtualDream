using LogSpiralLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDream.Contents.StarBound.Weapons.Broken
{
    public class BrokenRarity : ModRarity
    {
        public override Color RarityColor => Color.Lerp(Color.Gray, Color.MediumPurple, ((float)LogSpiralLibraryMod.ModTime).CosFactor(120));
    }
}
