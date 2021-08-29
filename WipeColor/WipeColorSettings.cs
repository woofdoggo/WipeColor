using Microsoft.Xna.Framework;
using Monocle;
using System;
using YamlDotNet.Serialization;

namespace Celeste.Mod.WipeColor {
    [SettingName("modoptions_wipecolormodule_title")]
    public class WipeColorSettings : EverestModuleSettings {
        [SettingInGame(false)]
        [SettingName("modoptions_wipecolormodule_enabled")]
        [SettingSubText("Whether or not the mod should change the screen wipe color.")]
        public bool ModEnabled { get; set; } = true;

        [SettingInGame(false)]
        [SettingMaxLength(6)]
        [SettingName("modoptions_wipecolormodule_wipecolor")]
        [SettingSubText("The hex color to set the screen wipe to. Make sure this is valid!")]
        public string WipeColorString { get; set; } = "000000";

        [SettingInGame(false)]
        [SettingName("modoptions_wipecolormodule_backgroundcolor")]
        [SettingSubText("Whether or not the wipe color should also apply to the background (during chapter entry, etc)")]
        [SettingNeedsRelaunch]
        public bool BackgroundEnabled { get; set; } = true;
    }
}