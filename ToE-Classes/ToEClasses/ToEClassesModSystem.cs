using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using ToEClasses.Patches;
using Vintagestory.API.Common;

namespace ToEClasses;

public class ToEClassesModSystem : ModSystem
{
    private Harmony _harmony;

    public override void Start(ICoreAPI api)
    {
        if (Harmony.HasAnyPatches(Mod.Info.ModID)) return;
        _harmony = new Harmony(Mod.Info.ModID);
        _harmony.PatchAll();
        
        ApplyPatchesToMods(api);

        api.World.Logger.Notification($"{Mod.Info.Name}: Harmony patches enabled.");
    }

    private void ApplyPatchesToMods(ICoreAPI api)
    {
        if (api.ModLoader.IsModEnabled("herbalistpotsfork"))
        {
            DynamicPatchMod(api,
                "HerbPots",
                "BlockHerbalistPot",
                "OnBlockInteractStart",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                typeof(BlockHerbalistPotPatch).GetMethod(nameof(BlockHerbalistPotPatch.OnBlockInteractStartPrefix)));
        }

        if (api.ModLoader.IsModEnabled("substrate"))
        {
            DynamicPatchMod(api,
                "Substrate",
                "BehaviorMushroomGrower",
                "OnBlockInteractStart",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                typeof(BehaviorMushroomGrowerPatch).GetMethod(nameof(BehaviorMushroomGrowerPatch.OnBlockInteractStartPrefix)));
        }

        if (api.ModLoader.IsModEnabled("aculinaryartillery"))
        {
            DynamicPatchMod(api,
                "ACulinaryArtillery",
                "BlockMixingBowl",
                "OnBlockInteractStart",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic,
                typeof(BlockMixingBowlPatch).GetMethod(nameof(BlockMixingBowlPatch.OnBlockInteractStartPrefix)));
        }
    }

    private void DynamicPatchMod(ICoreAPI api,
        string assemblyName,
        string targetTypeName,
        string originalMethodName,
        BindingFlags originalMethodFlags,
        MethodInfo? prefixMethod = null,
        MethodInfo? postfixMethod = null)
    {
        var herbAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == assemblyName);

        if (herbAsm == null)
        {
            api.Logger.Warning($"[{_harmony.Id}] {assemblyName} not loaded. Skipping patch.");
            return;
        }

        var targetType = herbAsm.GetTypes().FirstOrDefault(t => t.Name == targetTypeName);

        if (targetType == null)
        {
            api.Logger.Warning($"[{_harmony.Id}] {assemblyName} is enabled, but type '{targetTypeName}' could not be located.");
            return;
        }

        var originalMethod = targetType.GetMethod(originalMethodName, originalMethodFlags);

        if (originalMethod == null)
        {
            api.Logger.Error($"[{_harmony.Id}] Could not find method '{originalMethodName}' on type '{targetTypeName}'.");
            return;
        }

        _harmony.Patch(
            originalMethod,
            prefix: prefixMethod != null ? new HarmonyMethod(prefixMethod) : null,
            postfix: postfixMethod != null ? new HarmonyMethod(postfixMethod) : null);
        api.Logger.Notification($"[{_harmony.Id}] Successfully injected Harmony patch into {assemblyName}!");
    }
}