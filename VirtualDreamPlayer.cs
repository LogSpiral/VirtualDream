using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualDream
{
    public class VirtualDreamPlayer:ModPlayer
    {
        public static float screenShakeStrength;
        public override void ResetEffects()
        {
            screenShakeStrength = 0;
        }
        public override void ModifyScreenPosition()
        {
            if (screenShakeStrength > 0) Main.screenPosition += screenShakeStrength * Main.rand.NextFloat(0, 1) * Main.rand.NextVector2Unit();
        }
    }
}
