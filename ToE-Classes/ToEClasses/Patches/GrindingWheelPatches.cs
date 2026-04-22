using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("GrindingWheelLock")]
public class GrindingWheelPatches
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockEntityGrindingWheel), nameof(BlockEntityGrindingWheel.OnInteractStart))]
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
}