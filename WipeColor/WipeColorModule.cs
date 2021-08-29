using Microsoft.Xna.Framework;
using Monocle;
using System;

namespace Celeste.Mod.WipeColor {
    public partial class WipeColorModule : EverestModule {
        public static WipeColorModule Instance;

        public override Type SettingsType => typeof(WipeColorSettings);
        public static WipeColorSettings Settings => (WipeColorSettings) Instance._Settings;

        public WipeColorModule() {
            Instance = this;
            
        }

        public override void Load() {
            if (Settings.BackgroundEnabled) {
                try {
                    Monocle.Engine.ClearColor = Calc.HexToColor(Settings.WipeColorString);
                } catch (Exception e) {
                    Monocle.Engine.ClearColor = Color.Black;
                    Logger.Log(LogLevel.Warn, "WipeColor", "Failed to set ClearColor");
                    Logger.LogDetailed(e, "WipeColor");
                }
            }

            // Wipe constructors
            On.Celeste.AngledWipe.ctor += AngledWipeHook;
            On.Celeste.CurtainWipe.ctor += CurtainWipeHook;
            On.Celeste.DreamWipe.ctor += DreamWipeHook;
            On.Celeste.DropWipe.ctor += DropWipeHook;
            On.Celeste.FallWipe.ctor += FallWipeHook;
            On.Celeste.HeartWipe.ctor += HeartWipeHook;
            On.Celeste.KeyDoorWipe.ctor += KeyDoorWipeHook;
            On.Celeste.MountainWipe.ctor += MountainWipeHook;
            On.Celeste.WindWipe.ctor += WindWipeHook;


        }

        public override void Unload() {
            // Wipe constructors
            On.Celeste.AngledWipe.ctor -= AngledWipeHook;
            On.Celeste.CurtainWipe.ctor -= CurtainWipeHook;
            On.Celeste.DreamWipe.ctor -= DreamWipeHook;
            On.Celeste.DropWipe.ctor -= DropWipeHook;
            On.Celeste.FallWipe.ctor -= FallWipeHook;
            On.Celeste.HeartWipe.ctor -= HeartWipeHook;
            On.Celeste.KeyDoorWipe.ctor -= KeyDoorWipeHook;
            On.Celeste.MountainWipe.ctor -= MountainWipeHook;
            On.Celeste.WindWipe.ctor -= WindWipeHook;


        }

        // TODO: Fade wipe
        // TODO: HeartWipe.Render IL Hook
        // TODO: Spotlight wipe
        // TODO: Make apply background color better
    }
}