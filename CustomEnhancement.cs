using CG.Game.Player;
using Gameplay.Enhancements;
using HarmonyLib;
using System.Collections.Generic;

namespace VoidRumble
{
    [HarmonyPatch(typeof(Enhancement), "RequestStateChange")]
    internal class CustomEnhancement
    {
        static void Postfix(EnhancementState newState, float grade, Enhancement __instance)
        {
            VoidRumbleBepinPlugin.Log.LogDebug($"CustomEnhancement::RequestStateChange got called");

            grade = Enhancement.NormalizedValueToLocalPlayerProficiency(grade);
            float durationMultiplier = LocalPlayer.I.EnhancementDurationMultiplier.Value;

            List<StatMod> statModList = __instance.AppliedEnhancementEffect.DefaultModifiers();
            if (__instance.AppliedEnhancementEffect.InvertedEffectApplication && statModList.Count > 0)
            {
                VoidRumbleBepinPlugin.Log.LogDebug(
                    $"CustomEnhancement::RequestStateChange found stat type: {StatType.GetNameById(statModList[0].Type)}");
                if (statModList[0].Type == StatType.EnginePowerPip.Id)
                {
                    if (VoidRumbleBepinPlugin.DeviceManager.IsConnected())
                    {
                        VoidRumbleBepinPlugin.DeviceManager.VibrateConnectedDevicesWithDuration(1.0, 2);
                    }
                }
            }
        }
    }
}
