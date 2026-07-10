using HarmonyLib;
using Vintagestory.API.Common;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("BlockHerbalistPot")]
public static class BlockHerbalistPotPatch
{
    public static bool OnBlockInteractStartPrefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Apothecary)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}