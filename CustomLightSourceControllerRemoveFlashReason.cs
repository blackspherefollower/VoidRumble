using System;
using CG.Client.Ship.Hull;
using HarmonyLib;
using Gameplay.Atmosphere;

namespace VoidRumble
{
        [HarmonyPatch(typeof(LightSourceController), "RemoveFlashReason")]
        internal class CustomLightSourceControllerAddFlashReason
        {
            private static DateTime _lastAction = new DateTime();

            static void Postfix(Room room, FlashReason reason)
            {
                VoidRumbleBepinPlugin.Log.LogDebug($"CustomLightSourceControllerAddFlashReason::RemoveFlashReason got called");

                if ((DateTime.Now - _lastAction).TotalMilliseconds > 500)
                {
                    _lastAction = DateTime.Now;
                }
                else
                {
                    VoidRumbleBepinPlugin.Log.LogDebug($"CustomLightSourceControllerAddFlashReason::RemoveFlashReason debounced");
                    return;
                }

                if (VoidRumbleBepinPlugin.DeviceManager.IsConnected())
                {
                    VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevices(0.0);
                }
            }
        }
}
