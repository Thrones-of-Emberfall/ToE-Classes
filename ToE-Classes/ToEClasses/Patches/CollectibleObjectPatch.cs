using HarmonyLib;
using Vintagestory.API.Common;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("CollectibleBehaviour")]
public static class CollectibleObjectPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CollectibleObject), nameof(CollectibleObject.GetMiningSpeed))]
    private static void GetMiningSpeedPostfix(ref float __result, Block block, BlockSelection blockSel,
        IPlayer forPlayer)
    {
        var material = block.GetBlockMaterial(forPlayer.Entity.Api.World.BlockAccessor, blockSel.Position);

        var multiplier = material switch
        {
            EnumBlockMaterial.Gravel or EnumBlockMaterial.Sand or EnumBlockMaterial.Soil => forPlayer.Entity.Stats
                .GetBlended(Attributes.DiggingSpeed),
            _ => 1f
        };

        __result *= multiplier;
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(CollectibleObject), nameof(CollectibleObject.GetTransitionRateMul))]
    private static void GetTransitionRateMulPostfix(ref float __result, ItemSlot inSlot, EnumTransitionType transType)
    {
        if (transType != EnumTransitionType.Perish) return;
        if (inSlot.Itemstack == null) return;
        if (inSlot.Itemstack.Attributes.GetAsBool(Attributes.CookedByDirtyApron)) __result *= 0.9f;
    }
}