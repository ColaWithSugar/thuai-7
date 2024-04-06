using static GameServer.GameLogic.IItem;

namespace GameServer.GameLogic;

public partial class Player : IPlayer
{
    public enum PlayerEventType
    {
        PlayerAbandon,
        PlayerAttack,
        PlayerPickUp,
        PlayerSwitchArm,
        PlayerUseGrenade,
        PlayerUseMedicine
    };

    public class PlayerAbandonEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerAbandon;
        public Player Player { get; }
        public int Number { get; }
        public List<(IItem.ItemKind itemKind, string itemSpecificName)> AbandonedSupplies { get; }

        public PlayerAbandonEventArgs(Player player, int number, List<(IItem.ItemKind itemKind, string itemSpecificName)> abandonedSupplies)
        {
            Player = player;
            Number = number;
            AbandonedSupplies = abandonedSupplies;
        }
    }

    public class PlayerAttackEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerAttack;
        public Player Player { get; }
        public Position TargetPosition { get; }

        public PlayerAttackEventArgs(Player player, Position targetPosition)
        {
            Player = player;
            TargetPosition = targetPosition;
        }
    }

    public class PlayerPickUpEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerPickUp;
        public Player Player { get; }
        public string TargetSupply { get; }
        public Position TargetPosition { get; }
        public int Numb { get; }

        public PlayerPickUpEventArgs(Player player, string targetSupply, Position targetPosition, int numb)
        {
            Player = player;
            TargetSupply = targetSupply;
            TargetPosition = targetPosition;
            Numb = numb;
        }
    }

    public class PlayerSwitchArmEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerSwitchArm;
        public Player Player { get; }
        public string TargetFirearm { get; }

        public PlayerSwitchArmEventArgs(Player player, string targetFirearm)
        {
            Player = player;
            TargetFirearm = targetFirearm;
        }
    }


    public class PlayerUseGrenadeEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerUseGrenade;
        public Player Player { get; }
        public Position TargetPosition { get; }

        public PlayerUseGrenadeEventArgs(Player player, Position targetPosition)
        {
            Player = player;
            TargetPosition = targetPosition;
        }
    }

    public class PlayerUseMedicineEventArgs : EventArgs
    {
        public const PlayerEventType EventName = PlayerEventType.PlayerUseMedicine;
        public Player Player { get; }
        public Medicine TargetMedicine { get; }

        public PlayerUseMedicineEventArgs(Player player, Medicine targetMedicine)
        {
            Player = player;
            TargetMedicine = targetMedicine;
        }
    }


    public event EventHandler<PlayerAbandonEventArgs>? PlayerAbandonEvent;
    public event EventHandler<PlayerAttackEventArgs>? PlayerAttackEvent;
    public event EventHandler<PlayerPickUpEventArgs>? PlayerPickUpEvent;
    public event EventHandler<PlayerSwitchArmEventArgs>? PlayerSwitchArmEvent;
    public event EventHandler<PlayerUseGrenadeEventArgs>? PlayerUseGrenadeEvent;
    public event EventHandler<PlayerUseMedicineEventArgs>? PlayerUseMedicineEvent;
}
