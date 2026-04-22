using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("GrindingWheelLock")]
public class GrindingWheelPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockEntityGrindingWheel), "OnInteractStart")]
    public static bool BlockEntityGrindingWheelOnInteractStartPrefix(BlockEntityGrindingWheel __instance, IPlayer byPlayer)
    {
      var api = __instance.Api;

      if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Smith))
      {
        ToELockUtils.DenyWithPopup(api);
        return false;
      }

      return true;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Block), nameof(BlockGrindingWheel.TryPlaceBlock))]
    public static bool BlockGrindingWheelTryPlaceBlockPrefix(BlockGrindingWheel __instance, IWorldAccessor world, IPlayer byPlayer,
      ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
    {
      var api = world.Api;

      if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Smith))
      {
        failureCode = "toeclasses:missing-trait";
        return false;
      }

      return true;
    }
}