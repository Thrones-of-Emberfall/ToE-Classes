using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Config;
using Vintagestory.API.Server;

namespace ToEClasses;

public class ToEClassesModSystem : ModSystem
{
    private Harmony _harmony;
    private ICoreAPI _api;

    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        _api = api;
        RegisterBlockClasses();
        RegisterItemClasses();

        if (Harmony.HasAnyPatches(Mod.Info.ModID)) return;
        _harmony = new Harmony(Mod.Info.ModID);
        _harmony.PatchAll();
        api.World.Logger.Notification($"{Mod.Info.Name}: Harmony patches enabled.");
    }

    private void RegisterItemClasses()
    {
        _api.RegisterItemClass("RollerLocked", typeof(RollerLocked));
    }

    private void RegisterBlockClasses()
    {
        _api.RegisterBlockClass("AnvilLocked", typeof(AnvilLocked));
        _api.RegisterBlockClass("BellowsLocked", typeof(BellowLocked));
        _api.RegisterBlockClass("HelveLocked", typeof(HelveLocked));
        _api.RegisterBlockClass("PulverizerLocked", typeof(PulverizerLocked));
        _api.RegisterBlockClass("BoilerLocked", typeof(BoilerLocked));
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("toe-classes:hello"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("toe-classes:hello"));
    }
}