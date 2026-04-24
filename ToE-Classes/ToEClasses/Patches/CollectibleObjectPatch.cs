using HarmonyLib;
using Vintagestory.API.Common;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("CollectibleBehaviour")]
public class CollectibleObjectPatch
{
    [HarmonyPostfix]
    [HarmonyPatch(typeof(CollectibleObject), nameof(CollectibleObject.GetMiningSpeed))]
    private static void GetMiningSpeedPostfix(ref float __result, Block block, BlockSelection blockSel,
        IPlayer forPlayer)
    {
        var material = block.GetBlockMaterial(forPlayer.Entity.Api.World.BlockAccessor, blockSel.Position);

        var multiplier = material switch
        {
            EnumBlockMaterial.Soil => forPlayer.Entity.Stats.GetBlended("soilDiggingSpeedMul"),
            _ => 1f
        };

        __result *= multiplier;
        forPlayer.Entity.Api.Logger.Debug("[{0}] Mining Speed for {1}: {2}", forPlayer.Entity.Api.Side, block.Code,
            __result);
    }
}