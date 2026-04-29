using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("BlockCrock")]
public class BlockCrockPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.OnContainedInteractStart))]
    public static bool OnContainedInteractStartPrefix(BlockCrock __instance, BlockEntityContainer be, ItemSlot slot,
        IPlayer byPlayer, BlockSelection blockSel)
    {
        var activeHotbarSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
        var itemstack = activeHotbarSlot.Itemstack;
        if (itemstack == null) return true;

        var canSealCrock = itemstack.Collectible.Attributes["canSealCrock"].AsBool();
        if (!canSealCrock) return true;
        if (slot.Itemstack == null) return true;
        if (!__instance.IsFullAndUnsealed(slot.Itemstack)) return true;
        if (!ToEClassesUtils.HasTrait(byPlayer.Entity.Api, byPlayer, Traits.DirtyApron)) return true;
        slot.Itemstack.Attributes.SetBool("sealedByDirtyApron", true);
        return true;
    }
}