using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("BlockCookedContainerBase")]
public static class BlockCookedContainerBasePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockCookedContainerBase), nameof(BlockCookedContainerBase.ServeIntoStack))]
    public static void OnBlockInteractStartPrefix(ItemSlot potslot)
    {
        potslot.Itemstack?.Attributes.RemoveAttribute(Attributes.SealedByDirtyApron);
    }
}