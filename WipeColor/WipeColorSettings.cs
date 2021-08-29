using Microsoft.Xna.Framework;
using Monocle;
using System;
using YamlDotNet.Serialization;

namespace Celeste.Mod.WipeColor {
    [SettingName("modoptions_wipecolormodule_title")]
    public class WipeColorSettings : EverestModuleSettings {
        [SettingName("modoptions_wipecolormodule_enabled")]
        [SettingSubText("Whether or not the mod should change the screen wipe color")]
        public bool ModEnabled {
            get => _Enabled;
            set {
                _Enabled = value;
                WipeColorModule.ApplyClearColor();
            }
        }

        [SettingName("modoptions_wipecolormodule_backgroundcolor")]
        [SettingSubText("Whether or not the wipe color should also apply to the background (during chapter entry, etc)")]
        public bool BackgroundEnabled { 
            get => _BackgroundEnabled;
            set {
                _BackgroundEnabled = value;
                WipeColorModule.ApplyClearColor();
            }
        }

        [SettingName("modoptions_wipecolormodule_applymountainwipe")]
        [SettingSubText("Whether or not the mountain wipe color should be changed, even when it is white")]
        public bool AlwaysReplaceMountainWipe { get; set; } = false;

        [SettingName("modoptions_wipecolormodule_applyindebug")]
        [SettingSubText("Whether or not the map editor should have its background color changed")]
        public bool ApplyToDebugMap { get; set; } = false;

        [SettingMaxLength(6)]
        [SettingName("modoptions_wipecolormodule_wipecolor")]
        [SettingSubText("The hex color to set the screen wipe to. Make sure this is valid!")]
        public string WipeColorString {
            get => WipeColorModule.WipeColor.R.ToString("X2") + WipeColorModule.WipeColor.G.ToString("X2") + WipeColorModule.WipeColor.B.ToString("X2");
            set => WipeColorModule.WipeColor = Calc.HexToColor(value);
        }



        [YamlIgnore]
        [SettingIgnore]
        private bool _BackgroundEnabled { get; set; } = true;

        [YamlIgnore]
        [SettingIgnore]
        private bool _Enabled { get; set; } = true;
    }
}