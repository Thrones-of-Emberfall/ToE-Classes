using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.GameContent.Mechanics;

namespace ToEClasses
{
    internal static class LockRequirements
    {
        public const string Smith = "smith";
        public const string Machinist = "machinist";
        public const string Shipwright = "shipwright";
    }

    public class AnvilLocked : BlockAnvil
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (MetalTier > 2 && !ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Smith))
            {
                return ToELockUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class BellowLocked : BlockBellows
    {
        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Smith))
            {
                return ToELockUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class HelveLocked : BlockHelveHammer
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Machinist))
            {
                ToELockUtils.DenyWithPopup(api);
                failureCode = "__ignore__";
                return false;
            }

            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Machinist))
            {
                return ToELockUtils.DenyWithPopup(api);
            }

            return base.OnBlockInteractStart(world, byPlayer, blockSel);
        }
    }

    public class PulverizerLocked : BlockPulverizer
    {
        public override bool TryPlaceBlock(IWorldAccessor world, IPlayer byPlayer, ItemStack itemstack, BlockSelection blockSel, ref string failureCode)
        {
            if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Machinist))
            {
                ToELockUtils.DenyWithPopup(api);
                failureCode = "__ignore__";
                return false;
            }

            return base.TryPlaceBlock(world, byPlayer, itemstack, blockSel, ref failureCode);
        }

        public override bool OnBlockInteractStart(IWorldAccessor world, IPlayer byPlayer, BlockSelection blockSel)
        {
            if (!ToELockUtils.HasTrait(api, byPlayer, LockRequirements.Machinist))
            {
                return ToELockUtils.DenyWithPopup(api);
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

            if (!ToELockUtils.HasTrait(api, player, LockRequirements.Shipwright))
            {
                ToELockUtils.DenyWithPopup(api);
                handling = EnumHandHandling.PreventDefault;
                return;
            }

            base.OnHeldInteractStart(slot, byEntity, blockSel, entitySel, firstEvent, ref handling);
        }
    }
}
