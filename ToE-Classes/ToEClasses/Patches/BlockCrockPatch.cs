using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("BlockCrock")]
public static class BlockCrockPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.OnContainedInteractStart))]
    public static bool OnContainedInteractStartPrefix(BlockCrock __instance, ItemSlot slot, IPlayer byPlayer)
    {
        var activeHotbarSlot = byPlayer.InventoryManager.ActiveHotbarSlot;
        var itemstack = activeHotbarSlot.Itemstack;
        if (itemstack == null) return true;

        var canSealCrock = itemstack.Collectible.Attributes["canSealCrock"].AsBool();
        if (!canSealCrock) return true;
        if (slot.Itemstack == null) return true;
        if (!__instance.IsFullAndUnsealed(slot.Itemstack)) return true;
        if (!ToEClassesUtils.HasTrait(byPlayer.Entity.Api, byPlayer, Traits.DirtyApron)) return true;
        slot.Itemstack.Attributes.SetBool(Attributes.SealedByDirtyApron, true);
        return true;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.GetContainingTransitionModifierPlaced))]
    public static void GetContainingTransitionModifierPlacedPostfix(ref float __result, IWorldAccessor world,
        BlockPos pos, EnumTransitionType transType)
    {
        if (world.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCrock blockEntity ||
            transType != EnumTransitionType.Perish ||
            world.BlockAccessor.GetBlock(pos) is not BlockCrock block ||
            !blockEntity.Sealed ||
            !block.Attributes[Attributes.SealedByDirtyApron].AsBool())
            return;
        if (blockEntity.RecipeCode == null) return;
        __result *= 0.667f;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.GetContainingTransitionModifierContained))]
    public static void GetContainingTransitionModifierPlacedPostfix(ref float __result, ItemSlot inSlot,
        EnumTransitionType transType)
    {
        if (transType != EnumTransitionType.Perish) return;
        var itemStack = inSlot.Itemstack;
        var attributes = itemStack?.Attributes;
        if (attributes == null || !attributes.GetAsBool("sealed")) return;

        if (attributes.GetString("recipeCode") == null) return;
        if (attributes.GetBool(Attributes.SealedByDirtyApron)) __result *= 0.667f;
    }
}