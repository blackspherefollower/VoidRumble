using System;
using CG.Client.Ship.Hull;
using HarmonyLib;
using Gameplay.Atmosphere;

namespace VoidRumble
{
    [HarmonyPatch(typeof(LightSourceController), "AddFlashReason")]
    internal class CustomLightSourceController
    {
        private static DateTime _lastAction = new DateTime();

        static void Postfix(Room room, FlashReason reason, float? flashLength = null)
        {
            VoidRumbleBepinPlugin.Log.LogDebug($"CustomLightSourceController::AddFlashReason got called");

            if ((DateTime.Now - _lastAction).TotalMilliseconds > 500)
            {
                _lastAction = DateTime.Now;
            }
            else
            {
                VoidRumbleBepinPlugin.Log.LogDebug($"CustomLightSourceController::AddFlashReason debounced");
                return;
            }

            if (VoidRumbleBepinPlugin.DeviceManager.IsConnected())
            {
                if ((flashLength ?? 0.0) > 0.0)
                {
                    VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevicesWithDuration(1.0, flashLength ?? 0);
                }
                else
                {
                    VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevices(0.0);
                }
            }
        }
    }
}
