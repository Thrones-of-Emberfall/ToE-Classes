using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.GameContent;

namespace ToEClasses
{
    public static class ToELockUtils
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
