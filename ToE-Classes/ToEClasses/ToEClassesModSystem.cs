using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace ToEClasses;

public class ToEClassesModSystem : ModSystem
{
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        api.RegisterBlockClass("AnvilLocked", typeof(AnvilLocked));
        api.RegisterBlockClass("BellowsLocked", typeof(BellowLocked));
        api.RegisterBlockClass("HelveLocked", typeof(HelveLocked));
        api.RegisterBlockClass("PulverizerLocked", typeof(PulverizerLocked));
        api.RegisterItemClass("RollerLocked", typeof(RollerLocked));
        api.RegisterBlockClass("BoilerLocked", typeof(BoilerLocked));
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