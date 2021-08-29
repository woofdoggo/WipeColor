using Microsoft.Xna.Framework;
using Monocle;
using System;
using YamlDotNet.Serialization;

namespace Celeste.Mod.WipeColor {
    [SettingName("modoptions_wipecolormodule_title")]
    public class WipeColorSettings : EverestModuleSettings {
        [SettingInGame(true)]
        [SettingName("modoptions_wipecolormodule_enabled")]
        [SettingSubText("Whether or not the mod should change the screen wipe color.")]
        public bool ModEnabled { get; set; } = true;

        [SettingInGame(false)]
        [SettingMaxLength(6)]
        [SettingName("modoptions_wipecolormodule_wipecolor")]
        [SettingSubText("The hex color to set the screen wipe to. Make sure this is valid!")]
        public string WipeColorString {
            get => WipeColorModule.WipeColor.R.ToString("X2") + WipeColorModule.WipeColor.G.ToString("X2") + WipeColorModule.WipeColor.B.ToString("X2");
            set => WipeColorModule.WipeColor = Calc.HexToColor(value);
        }

        [SettingInGame(true)]
        [SettingName("modoptions_wipecolormodule_backgroundcolor")]
        [SettingSubText("Whether or not the wipe color should also apply to the background (during chapter entry, etc)")]
        public bool BackgroundEnabled { 
            get => WipeColorModule.ClearColor;
            set {
                WipeColorModule.ClearColor = value;
                WipeColorModule.ApplyClearColor();
            }
        }

        [SettingInGame(true)]
        [SettingName("modoptions_wipecolormodule_applymountainwipe")]
        [SettingSubText("Whether or not the mountain wipe color should be changed, even when it is white")]
        public bool AlwaysReplaceMountainWipe { get; set; } = false;
    }
}