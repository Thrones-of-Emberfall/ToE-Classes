using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace ToEClasses;


[HarmonyPatchCategory("AnvilLock")]
[HarmonyPatch]
public class BlockAnvil_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockAnvil), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(BlockAnvil __instance, IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (__instance.MetalTier > 1 && !ToEClassesUtils.HasTrait(api, byPlayer, Traits.Smith))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            return false;
        }
        return true;
    }
}


[HarmonyPatchCategory("HelveLock")]
[HarmonyPatch]
public class BlockHelveHammer_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockHelveHammer), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            return false;
        }
        return true;
    }
}


[HarmonyPatchCategory("PulverizerLock")]
[HarmonyPatch]
public class BlockPulverizer_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockPulverizer), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            return false;
        }
        return true;
    }
}


[HarmonyPatchCategory("BoilerLock")]
[HarmonyPatch]
public class BlockBoiler_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockBoiler), "OnBlockInteractStart");

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Distiller))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            return false;
        }
        return true;
    }
}

[HarmonyPatchCategory("RollerLock")]
public class ItemRoller_OnHeldInteractStart_Patch
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


[HarmonyPatchCategory("MixingBowlLock")]
[HarmonyPatch]
public class BlockMixingBowl_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod()
    {
        var type = AccessTools.TypeByName("ACulinaryArtillery.BlockMixingBowl");
        return type == null ? null : AccessTools.DeclaredMethod(type, "OnBlockInteractStart");
    }

    static bool Prepare() => TargetMethod() != null;

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Mixer))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            return false;
        }
        return true;
    }
}


[HarmonyPatchCategory("TinctureLock")]
[HarmonyPatch]
public class BlockEntityBarrel_GetCanSeal_Patch
{
    static MethodBase TargetMethod() => AccessTools.Method(typeof(BlockEntityBarrel), "GetCanSeal");

    [HarmonyPostfix]
    public static void Postfix(BlockEntityBarrel __instance, IPlayer byPlayer, ref bool __result)
    {
        if (!__result) return;

        var outputCode = __instance.CurrentRecipe?.Output?.ResolvedItemStack?.Collectible.Code?.Path;
        if (outputCode == null || !outputCode.StartsWith("tinctureportion")) return;

        var api = __instance.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Apothecary))
        {
            __result = false;
            ToEClassesUtils.DenyWithPopup(api);
        }
    }
}

[HarmonyPatchCategory("MushroomGrowerLock")]
[HarmonyPatch]
public class BehaviorMushroomGrower_OnBlockInteractStart_Patch
{
    static MethodBase TargetMethod()
    {
        var type = AccessTools.TypeByName("Substrate.Behaviors.BehaviorMushroomGrower");
        return type == null ? null : AccessTools.DeclaredMethod(type, "OnBlockInteractStart");
    }

    static bool Prepare() => TargetMethod() != null;

    [HarmonyPrefix]
    public static bool Prefix(IPlayer byPlayer, ref EnumHandling handling, ref bool __result)
    {
        var api = byPlayer.Entity.World.Api;
        if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Farmer))
        {
            __result = ToEClassesUtils.DenyWithPopup(api);
            handling = EnumHandling.PreventSubsequent;
            return false;
        }
        return true;
    }
}