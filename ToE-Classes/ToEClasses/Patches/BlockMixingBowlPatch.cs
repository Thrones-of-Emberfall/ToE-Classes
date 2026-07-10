using Vintagestory.API.Common;

namespace ToEClasses.Patches;

public class BlockMixingBowlPatch
{
    public static bool OnBlockInteractStartPrefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Mixer)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}