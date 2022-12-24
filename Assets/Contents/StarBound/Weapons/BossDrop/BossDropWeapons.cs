//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VirtualDream.Contents.StarBound.Weapons.BossDrop
//{
//    public interface IBossDropWeaponProj<Normal, Extra, Ultra> where Normal : ModItem where Extra : ModItem where Ultra : ModItem
//    {
//        public Player Player { get; }

//    }
//    public interface IUniqueWeaponProj<Normal, Extra> where Normal : ModItem where Extra : ModItem
//    {
//        public Player Player { get; }
//    }
//    public static class UpgradeWeaponExtension 
//    {
//        public static T UpgradeValue<T, Normal, Extra, Ultra>(IBossDropWeaponProj<Normal, Extra, Ultra> proj, T normal, T extra, T ultra, T defaultValue = default) where Normal : ModItem where Extra : ModItem where Ultra : ModItem
//        {
//            var type = proj.Player.HeldItem.type;
//            if (type == ModContent.ItemType<Normal>()) return normal;
//            if (type == ModContent.ItemType<Extra>()) return extra;
//            if (type == ModContent.ItemType<Ultra>()) return ultra;
//            return defaultValue;
//        }
//        public static T UpgradeValue<T, Normal, Extra>(IUniqueWeaponProj<Normal, Extra> proj, T normal, T extra, T defaultValue = default) where Normal : ModItem where Extra : ModItem
//        {
//            var type = proj.Player.HeldItem.type;
//            if (type == ModContent.ItemType<Normal>()) return normal;
//            if (type == ModContent.ItemType<Extra>()) return extra;
//            return defaultValue;
//        }
//    }
//    //public abstract class BossDropWeaponProj<Normal, Extra, Ultra> : Utils.BaseClasses.RangedHeldProjectile where Normal : ModItem where Extra : ModItem where Ultra : ModItem
//    //{
//    //    public T UpgradeValue<T>(T normal, T extra, T ultra, T defaultValue = default)
//    //    {
//    //        if (Player.HeldItem.type == ModContent.ItemType<Normal>()) return normal;
//    //        if (Player.HeldItem.type == ModContent.ItemType<Extra>()) return extra;
//    //        if (Player.HeldItem.type == ModContent.ItemType<Ultra>()) return ultra;
//    //        return defaultValue;
//    //    }
//    //}
//    //public abstract class UniqueWeaponProj<Normal, Extra> : Utils.BaseClasses.RangedHeldProjectile where Normal : ModItem where Extra : ModItem
//    //{
//    //    public T UpgradeValue<T>(T normal, T extra, T defaultValue = default)
//    //    {
//    //        if (Player.HeldItem.type == ModContent.ItemType<Normal>()) return normal;
//    //        if (Player.HeldItem.type == ModContent.ItemType<Extra>()) return extra;
//    //        return defaultValue;
//    //    }
//    //}
//}
