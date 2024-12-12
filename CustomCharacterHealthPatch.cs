using BufferedEvents.Impacts;
using CG.Space;
using Gameplay.Damage;
using HarmonyLib;
using UnityEngine;

namespace VoidRumble
{
    [HarmonyPatch(typeof(CustomCharacterHealth), "InformOfHit")]
    internal class CustomCharacterHealthPatch
    {
        static void Postfix(IDamageReceiver target, float damage, OrbitObject source, ImpactSize impactSize, DamageType damageType, Vector3 point, Quaternion rotation, bool IsMine)
        {
            VoidRumbleBepinPlugin.Log.LogDebug($"CustomCharacterHealth::InformOfHit got called");

            if (VoidRumbleBepinPlugin.DeviceManager.IsConnected())
            {
                VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevicesWithDuration(1.0, 2);
            }
        }
    }
}
