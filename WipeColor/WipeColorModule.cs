using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections;

namespace Celeste.Mod.WipeColor {
    public class WipeColorModule : EverestModule {
        public static WipeColorModule Instance;

        public override Type SettingsType => typeof(WipeColorSettings);
        public static WipeColorSettings Settings => (WipeColorSettings) Instance._Settings;

        public WipeColorModule() {
            Instance = this;
        }

        public override void Load() {
            On.Celeste.AscendManager.Removed += AscendManagerRemovedHook;
            On.Celeste.CS10_Ending.OnEnd += CS10_EndingHook;
            On.Celeste.Level.Begin += LevelBeginHook;
            On.Celeste.Level.End += LevelEndHook;
            On.Celeste.LevelEnter.Go += LevelEnterHook;

            if (Settings.BackgroundEnabled) {
                try {
                    Monocle.Engine.ClearColor = Calc.HexToColor(Settings.WipeColorString);
                } catch (Exception e) {
                    Monocle.Engine.ClearColor = Color.Black;
                    Logger.Log(LogLevel.Warn, "WipeColor", "Failed to set ClearColor");
                    Logger.LogDetailed(e, "WipeColor");
                }
            }
        }

        public override void Unload()
        {
            On.Celeste.AscendManager.Removed -= AscendManagerRemovedHook;
            On.Celeste.CS10_Ending.OnEnd -= CS10_EndingHook;
            On.Celeste.Level.Begin -= LevelBeginHook;
            On.Celeste.Level.End -= LevelEndHook;
            On.Celeste.LevelEnter.Go -= LevelEnterHook;
        }

        private static void AscendManagerRemovedHook(On.Celeste.AscendManager.orig_Removed orig, AscendManager self, Scene scene) {
            orig(self, scene);
            if (Settings.ModEnabled) ScreenWipe.WipeColor = Calc.HexToColor(Settings.WipeColorString);
        }

        private static void CS10_EndingHook(On.Celeste.CS10_Ending.orig_OnEnd orig, CS10_Ending self, Level level) {
            orig(self, level);
            if (Settings.ModEnabled) ScreenWipe.WipeColor = Calc.HexToColor(Settings.WipeColorString);
        }

        private static void LevelBeginHook(On.Celeste.Level.orig_Begin orig, Level self) {
            orig(self);
            if (Settings.ModEnabled) ScreenWipe.WipeColor = Calc.HexToColor(Settings.WipeColorString);
        }

        private static void LevelEndHook(On.Celeste.Level.orig_End orig, Level self) {
            orig(self);
            ScreenWipe.WipeColor = Color.Black;
        }

        private static void LevelEnterHook(On.Celeste.LevelEnter.orig_Go orig, Session session, bool fromSaveData) {
            orig(session, fromSaveData);
            if (Settings.ModEnabled) ScreenWipe.WipeColor = Calc.HexToColor(Settings.WipeColorString);
        }
    }
}