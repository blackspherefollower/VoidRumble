[![.github/workflows/build.yml](https://github.com/blackspherefollower/VoidRumble/actions/workflows/build.yml/badge.svg)](https://github.com/blackspherefollower/VoidRumble/actions/workflows/build.yml) [![Patreon donate button](https://img.shields.io/badge/patreon-donate-yellow.svg)](https://www.patreon.com/blackspherefollower)

# VoidRumble - 🔞 Adding Vibration to Void Crew

This mod adds vibration triggers to various in-game triggers, using https://intiface.com to handle the hardware compatibility: see [IoSTIndex](https://iostindex.com/?filter0Availability=Available,DIY&filter1Connection=Digital&filter2ButtplugSupport=4&filter3Features=OutputsVibrators) for supported output devices.

## Install Instructions

Ensure that you have [BepInEx 5](https://thunderstore.io/c/void-crew/p/BepInEx/BepInExPack/) (stable version 5 **MONO**) and [VoidManager](https://thunderstore.io/c/void-crew/p/VoidCrewModdingTeam/VoidManager/) installed.

Download the VoidRumble zip file from the most recent build (click the build badge above, then the top workflow run, then VoidRumble from the artifacts list at the bottom of the page)
Drag and drop `VoidRumble.dll`, `Buttplug.dll` and `Newtonsoft.Json.dll` into `Void Crew\BepInEx\plugins`

## Usage Instructions

Make sure Intiface is running on the default port `ws://localhost:12345` before you start the game - you should see it connect as the game starts and then cause vibrations as events are triggered.

In a multi-player game, all players must have the mod installed and **must** be of adult age and consenting to having their actions potentially trigger vibrations for other players.