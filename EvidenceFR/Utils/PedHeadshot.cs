using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rage;
using Rage.Native;
using RAGENativeUI.Elements;

namespace EvidenceFR.Utils
{
    public class PedHeadshot
    {
        public uint Handle { get; private set; }

        public Ped Ped { get; }

        public bool IsValid => NativeFunction.Natives.IS_PEDHEADSHOT_VALID<bool>(Handle);

        public bool IsReady => NativeFunction.Natives.IS_PEDHEADSHOT_READY<bool>(Handle);

        public string Txd => NativeFunction.Natives.GET_PEDHEADSHOT_TXD_STRING<string>(Handle);

        public PedHeadshotType Type = PedHeadshotType.Normal;

        public PedHeadshot(Ped ped)
        {
            Ped = ped;
            Handle = 9999;
        }

        public PedHeadshot(Ped ped, PedHeadshotType type)
        {
            Ped = ped;
            Handle = 9999;
            Type = type;
        }

        public PedHeadshot(int handle)
        {
            Handle = (uint)handle;
        }

        public static void UnregisterAllHeadshots()
        {
            for (int i = 0; i <= 32; i++) new PedHeadshot(i).Unregister();
        }

        public string GetResult()
        {
            Register();
            GameFiber.SleepUntil(() => IsReady, 10000);
            return Txd;
        }

        public Sprite GetSprite()
        {
            string txd = GetResult();
            if (txd != null)
            {
                return new Sprite(txd, txd, Point.Empty, Size.Empty);
            }
            else
            {
                Logging.Log(Logging.LogLevel.Warning, "Couldn't register ped headshot");
                return null;
            }
        }

        public void Register()
        {
            Handle = Type switch
            {
                PedHeadshotType.Normal => NativeFunction.Natives.REGISTER_PEDHEADSHOT<uint>(Ped),
                PedHeadshotType.Transparent => NativeFunction.Natives.REGISTER_PEDHEADSHOT_TRANSPARENT<uint>(Ped),
                PedHeadshotType.HiRes => NativeFunction.Natives.REGISTER_PEDHEADSHOT_HIRES<uint>(Ped),
                _ => throw new NotImplementedException(),
            };
        }

        public void Unregister()
        {
            if (Handle <= 32) NativeFunction.Natives.UNREGISTER_PEDHEADSHOT<uint>(Handle);
        }

        public override bool Equals(object obj)
        {
            return obj is PedHeadshot headshot && Handle == headshot.Handle;
        }

        public override int GetHashCode()
        {
            return (int)Handle;
        }

        public static implicit operator bool(PedHeadshot h) => h != null && h.IsValid;
    }

    public enum PedHeadshotType
    {
        Normal,
        Transparent,
        HiRes
    }
}
