using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
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

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.GetContainingTransitionModifierPlaced))]
    public static void GetContainingTransitionModifierPlacedPostfix(ref float __result, IWorldAccessor world,
        BlockPos pos, EnumTransitionType transType)
    {
        if (world.BlockAccessor.GetBlockEntity(pos) is not BlockEntityCrock blockEntity ||
            transType != EnumTransitionType.Perish ||
            world.BlockAccessor.GetBlock(pos) is not BlockCrock block ||
            !blockEntity.Sealed ||
            !block.Attributes["sealedByDirtyApron"].AsBool())
            return;
        if (blockEntity.RecipeCode == null) return;
        __result *= 0.667f;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(BlockCrock), nameof(BlockCrock.GetContainingTransitionModifierContained))]
    public static void GetContainingTransitionModifierPlacedPostfix(ref float __result, IWorldAccessor world,
        ItemSlot inSlot, EnumTransitionType transType)
    {
        if (transType != EnumTransitionType.Perish) return;
        var itemStack = inSlot.Itemstack;
        var attributes = itemStack?.Attributes;
        if (attributes == null || !attributes.GetAsBool("sealed")) return;

        if (attributes.GetString("recipeCode") == null) return;
        if (attributes.GetBool("cookedByDirtyApron")) __result *= 0.9f;
        if (attributes.GetBool("sealedByDirtyApron")) __result *= 0.667f;
    }
}