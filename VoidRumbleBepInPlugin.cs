using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Reflection;

namespace VoidRumble
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.USERS_PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Void Crew.exe")]
    [BepInDependency(VoidManager.MyPluginInfo.PLUGIN_GUID)]
    public class VoidRumbleBepinPlugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        internal static DeviceManager DeviceManager { get; private set; }

        private void Awake()
        {
            Log = Logger;
            DeviceManager = new DeviceManager(MyPluginInfo.USERS_PLUGIN_NAME);
            DeviceManager.ConnectDevices();
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), MyPluginInfo.PLUGIN_GUID);
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
