using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace ToEClasses.Patches;

[HarmonyPatchCategory("GrindingWheelLock")]
public class BlockEntityGrindingWheelPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(BlockEntityGrindingWheel), nameof(BlockEntityGrindingWheel.OnInteractStart))]
    public static bool Prefix(BlockEntityGrindingWheel __instance, IPlayer byPlayer)
    {
        var api = __instance.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Smith)) return true;
        ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}

[HarmonyPatchCategory("AnvilLock")]
[HarmonyPatch]
public class BlockAnvilPatch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockAnvil), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(BlockAnvil __instance, IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (__instance.MetalTier <= 1 || ToEClassesUtils.HasTrait(api, byPlayer, Traits.Smith)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}

[HarmonyPatchCategory("HelveLock")]
[HarmonyPatch]
public class BlockHelveHammerPatch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockHelveHammer), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}

[HarmonyPatchCategory("PulverizerLock")]
[HarmonyPatch]
public class BlockPulverizerPatch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockPulverizer), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}

[HarmonyPatchCategory("BoilerLock")]
[HarmonyPatch]
public class BlockBoilerPatch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockBoiler), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Distiller)) return true;
        __result = ToEClassesUtils.DenyWithPopup(api);
        return false;
    }
}

[HarmonyPatchCategory("RollerLock")]
public class ItemRollerPatch
{
    [HarmonyPrefix]
    [HarmonyPatch(typeof(ItemRoller), methodName: "OnHeldInteractStart")]
    public static bool Prefix(EntityAgent byEntity, ref EnumHandHandling handling)
    {
        var api = byEntity.World.Api;
        IPlayer player = (byEntity as EntityPlayer)?.Player;

        if (!ToEClassesUtils.HasTrait(api, player, Traits.Shipwright))
        {
            ToEClassesUtils.DenyWithPopup(api);
            handling = EnumHandHandling.PreventDefault;
            return false;
        }

        return true;
    }
}

[HarmonyPatchCategory("TinctureLock")]
[HarmonyPatch]
public class BlockEntityBarrelPatch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockEntityBarrel), "GetCanSeal");

    [HarmonyPostfix]
    public static void Postfix(BlockEntityBarrel __instance, IPlayer byPlayer, ref bool __result)
    {
        if (!__result) return;

        var outputCode = __instance.CurrentRecipe?.Output?.ResolvedItemStack?.Collectible.Code?.Path;
        if (outputCode == null || !outputCode.StartsWith("tinctureportion")) return;

        var api = __instance.Api;
        if (ToEClassesUtils.HasTrait(api, byPlayer, Traits.Apothecary)) return;
        __result = false;
        ToEClassesUtils.DenyWithPopup(api);
    }
}