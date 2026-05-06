using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace ToEClasses
{
    internal static class Traits
    {
        public const string Smith = "smith";
        public const string Machinist = "machinist";
        public const string Shipwright = "shipwright";
        public const string Distiller = "distiller";
        public const string Stitcher = "stitcher";
        public const string DirtyApron = "dirtyapron";
    }

    internal static class Attributes
    {
        public const string CookedByDirtyApron = "cookedByDirtyApron";
        public const string SealedByDirtyApron = "sealedByDirtyApron";
        public const string DiggingSpeed = "soilDiggingSpeedMul";
    }

    public class AnvilLocked : BlockAnvil
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (MetalTier > 1 && !ToEClassesUtils.HasTrait(api, byPlayer, Traits.Smith))
            {
                return ToEClassesUtils.DenyWithPopup(api, "toeclasses:anvil-tier-too-high");
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class BellowLocked : BlockBellows
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Smith))
            {
                return ToEClassesUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class HelveLocked : BlockHelveHammer
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
            {
                ToEClassesUtils.DenyWithPopup(api);
                failureCode = "__ignore__";
                return false;
            }

            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
            {
                return ToEClassesUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class PulverizerLocked : BlockPulverizer
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
            {
                ToEClassesUtils.DenyWithPopup(api);
                failureCode = "__ignore__";
                return false;
            }

            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Machinist))
            {
                return ToEClassesUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class RollerLocked : ItemRoller
    {
        public override void OnHeldInteractStart(
            ItemSlot slot,
            EntityAgent byEntity,
            BlockSelection blockSel,
            EntitySelection entitySel,
            bool firstEvent,
            ref EnumHandHandling handling)
        {
            IPlayer player = (byEntity as EntityPlayer)?.Player;

            if (!ToEClassesUtils.HasTrait(api, player, Traits.Shipwright))
            {
                ToEClassesUtils.DenyWithPopup(api);
                handling = EnumHandHandling.PreventDefault;
                return;
            }

            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
        }
    }

    public class BoilerLocked : BlockBoiler
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Distiller))
            {
                ToEClassesUtils.DenyWithPopup(api);
                failureCode = "__ignore__";
                return false;
            }

            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToEClassesUtils.HasTrait(api, byPlayer, Traits.Distiller))
            {
                return ToEClassesUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }
}
