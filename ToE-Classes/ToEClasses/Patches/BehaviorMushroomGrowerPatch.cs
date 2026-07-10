using Vintagestory.API.Common;

namespace ToEClasses.Patches;

public class BehaviorMushroomGrowerPatch
{
    public static bool OnBlockInteractStartPrefix(IPlayer byPlayer, ref EnumHandling handling, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        var hasTraitFarmer = ToEClassesUtils.HasTrait(api, byPlayer, Traits.Farmer);
        var hasTraitApothecary = ToEClassesUtils.HasTrait(api, byPlayer, Traits.Apothecary);
        if (hasTraitFarmer || hasTraitApothecary) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        handling = EnumHandling.PreventSubsequent;
        return false;
    }
}