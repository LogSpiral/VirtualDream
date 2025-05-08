using LogSpiralLibrary;
using LogSpiralLibrary.CodeLibrary.Utilties.Extensions;

namespace VirtualDream.Contents.StarBound.Weapons.Broken
{
    public class BrokenRarity : ModRarity
    {
        public override Color RarityColor => Color.Lerp(Color.Gray, Color.MediumPurple, ((float)LogSpiralLibraryMod.ModTime).CosFactor(120));
    }
}
