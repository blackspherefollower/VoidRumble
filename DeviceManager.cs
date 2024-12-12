using Buttplug.Client;
using Buttplug.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Stolen from https://github.com/bananasov/LethalVibrations/blob/master/LethalVibrations/Buttplug/DeviceManager.cs
namespace VoidRumble
{
    public class DeviceManager
    {
        private List<ButtplugClientDevice> ConnectedDevices { get; set; }
        private ButtplugClient ButtplugClient { get; set; }

        public DeviceManager(string clientName)
        {
            ConnectedDevices = new List<ButtplugClientDevice>();
            ButtplugClient = new ButtplugClient(clientName);
            VoidRumbleBepinPlugin.Log.LogInfo("BP client created for " + clientName);
            ButtplugClient.DeviceAdded += HandleDeviceAdded;
            ButtplugClient.DeviceRemoved += HandleDeviceRemoved;
        }

        public bool IsConnected() => ButtplugClient.Connected;

        public async void ConnectDevices()
        {
            if (ButtplugClient.Connected) { return; }

            try
            {
                VoidRumbleBepinPlugin.Log.LogInfo($"Attempting to connect to Intiface server at ws://localhost:12345");
                await ButtplugClient.ConnectAsync(new ButtplugWebsocketConnector(new Uri("ws://localhost:12345")));
                VoidRumbleBepinPlugin.Log.LogInfo("Connection successful. Beginning scan for devices");
                await ButtplugClient.StartScanningAsync();
            }
            catch (ButtplugException exception)
            {
                VoidRumbleBepinPlugin.Log.LogError($"Attempt to connect to devices failed. Ensure Intiface is running and attempt to reconnect from the 'Devices' section in the mod's in-game settings.");
                VoidRumbleBepinPlugin.Log.LogDebug($"ButtplugIO error occured while connecting devices: {exception}");
            }
        }

        public void VibrateConnectedDevicesWithDuration(double intensity, float time)
        {
            intensity += 0;

            async void Action(ButtplugClientDevice device)
            {
                await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
                await Task.Delay((int)(time * 1000f));
                await device.VibrateAsync(0.0f);
            }

            ConnectedDevices.ForEach(Action);
        }

        /// <summary>
        ///  This has to be manually stopped
        /// </summary>
        public void VibrateConnectedDevices(double intensity)
        {
            intensity += 0;

            async void Action(ButtplugClientDevice device)
            {
                await device.VibrateAsync(Mathf.Clamp((float)intensity, 0f, 1.0f));
            }

            ConnectedDevices.ForEach(Action);
        }

        public void StopConnectedDevices()
        {
            ConnectedDevices.ForEach(async (ButtplugClientDevice device) => await device.Stop());
        }

        internal void CleanUp()
        {
            StopConnectedDevices();
        }

        private void HandleDeviceAdded(object sender, DeviceAddedEventArgs args)
        {
            if (!IsVibratableDevice(args.Device))
            {
                VoidRumbleBepinPlugin.Log.LogInfo($"{args.Device.Name} was detected but ignored due to it not being vibratable.");
                return;
            }

            VoidRumbleBepinPlugin.Log.LogInfo($"{args.Device.Name} connected to client {ButtplugClient.Name}");
            ConnectedDevices.Add(args.Device);
        }

        private void HandleDeviceRemoved(object sender, DeviceRemovedEventArgs args)
        {
            if (!IsVibratableDevice(args.Device)) { return; }

            VoidRumbleBepinPlugin.Log.LogInfo($"{args.Device.Name} disconnected from client {ButtplugClient.Name}");
            ConnectedDevices.Remove(args.Device);
        }


        private bool IsVibratableDevice(ButtplugClientDevice device)
        {
            return device.VibrateAttributes.Count > 0;
        }
    }
}
