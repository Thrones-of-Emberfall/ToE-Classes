using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("CollectibleBehaviorWearable")]
public static class CollectibleBehaviorWearablePatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(CollectibleBehaviorWearable), nameof(CollectibleBehaviorWearable.TryMergeStacks))]
    public static bool TryMergeStacksPrefix(ref CollectibleBehaviorWearable __instance, ItemStackMergeOperation op,
        ref EnumHandling handling)
    {
        var player = op.ActingPlayer;
        var api = op.World.Api;

        if (op.CurrentPriority != EnumMergePriority.DirectMerge) return true;

        if (ToEClassesUtils.HasTrait(api, player, Traits.Stitcher)) return true;

        ToEClassesUtils.DenyWithPopup(api, "toeclasses:missing-stitcher-trait");
        handling = EnumHandling.Handled;
        return false;
    }
}