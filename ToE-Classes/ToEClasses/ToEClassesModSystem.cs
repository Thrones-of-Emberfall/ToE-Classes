using HarmonyLib;
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
        api.World.Logger.Notification($"{Mod.Info.Name}: Harmony patches enabled.");
    }
}