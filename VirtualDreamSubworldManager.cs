namespace VirtualDream
{
    public static class VirtualDreamSubworldManager
    {
        public static bool? Enter(string id)
        {
            Mod subworldLibrary = ModLoader.GetMod("SubworldLibrary");
            if (subworldLibrary != null)
            {
                return subworldLibrary.Call("Enter", id) as bool?;
            }
            return null;
        }
    }
}
