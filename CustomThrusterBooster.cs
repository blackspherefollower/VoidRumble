using HarmonyLib;
using CG.Ship.Modules;

namespace VoidRumble
{
    [HarmonyPatch(typeof(ThrusterBooster), "ChangeState")]
    internal class CustomThrusterBooster
    {
        static void Postfix(ThrusterBoosterState state)
        {
            VoidRumbleBepinPlugin.Log.LogDebug($"CustomThrusterBooster::ChangeState got called");

            if (VoidRumbleBepinPlugin.DeviceManager.IsConnected())
            {
                VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevices(state == ThrusterBoosterState.Charging ? .5 : 0);
            }
        }
    }
}
