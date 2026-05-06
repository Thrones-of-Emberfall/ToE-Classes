using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("BlockCookedContainerBase")]
public static class BlockCookedContainerBasePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockCookedContainerBase), nameof(BlockCookedContainerBase.ServeIntoStack))]
    public static void OnBlockInteractStartPrefix(BlockCookedContainerBase __instance, ItemSlot bowlSlot,
        ItemSlot potslot, IWorldAccessor world)
    {
        potslot.Itemstack?.Attributes.RemoveAttribute("sealedByDirtyApron");
    }
}