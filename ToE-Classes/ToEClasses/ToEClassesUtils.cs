using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace ToEClasses
{
    internal static class Traits
    {
        public const string Smith = "smith";
        public const string Machinist = "machinist";
        public const string Shipwright = "shipwright";
        public const string Distiller = "distiller";
        public const string Stitcher = "stitcher";
        public const string DirtyApron = "dirtyapron";
        public const string Mixer = "mixer";
        public const string Apothecary = "apothecary";
        public const string Clothier = "clothier";
        public const string Farmer = "farmer";
    }

    internal static class Attributes
    {
        public const string SealedByDirtyApron = "sealedByDirtyApron";
        public const string DiggingSpeed = "soilDiggingSpeedMul";
    }
    public static class ToEClassesUtils
    {
        public static bool HasTrait(ICoreAPI api, IPlayer player, string traitCode)
        {
            if (api == null || player == null || string.IsNullOrWhiteSpace(traitCode))
            {
                return false;
            }

            CharacterSystem charSys = api.ModLoader.GetModSystem<CharacterSystem>();
            return charSys != null && charSys.HasTrait(player, traitCode);
        }

        public static bool DenyWithPopup(ICoreAPI api, string errorMessage = "toeclasses:missing-trait")
        {
            if (api.Side == EnumAppSide.Client)
            {
                (api as ICoreClientAPI)?.TriggerIngameError(
                    null,
                    "classes-denied",
                    Lang.Get(errorMessage)
                );
            }

            return true;
        }
    }
}
