using Microsoft.Xna.Framework;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace Celeste.Mod.WipeColor {
    public partial class WipeColorModule : EverestModule {
        public static WipeColorModule Instance;
    
        public override Type SettingsType => typeof(WipeColorSettings);
        public static WipeColorSettings Settings => (WipeColorSettings) Instance._Settings;

        public static Color WipeColor = Color.Black;

        public WipeColorModule() {
            Instance = this;
        }

        public override void Load() {
            ApplyClearColor();

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

            // Extra hooks
            IL.Celeste.FadeWipe.Render += FadeRenderHook;
            IL.Celeste.HeartWipe.Render += HeartRenderHook;
            IL.Celeste.SpotlightWipe.Render += SpotlightRenderHook;

            On.Celeste.Editor.MapEditor.ctor += MapEditorCtorHook;
            On.Celeste.Editor.MapEditor.MakeMapEditorBetter += MapEditorBetterHook;
            On.Celeste.Editor.MapEditor.Update += MapEditorUpdateHook;
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

            // Extra hooks
            IL.Celeste.FadeWipe.Render -= FadeRenderHook;
            IL.Celeste.HeartWipe.Render -= HeartRenderHook;
            IL.Celeste.SpotlightWipe.Render -= SpotlightRenderHook;

            On.Celeste.Editor.MapEditor.ctor -= MapEditorCtorHook;
            On.Celeste.Editor.MapEditor.MakeMapEditorBetter -= MapEditorBetterHook;
            On.Celeste.Editor.MapEditor.Update -= MapEditorUpdateHook;
        }

        public static void ApplyClearColor() {
            if (Settings.BackgroundEnabled && Settings.ModEnabled) {
                Monocle.Engine.ClearColor = WipeColor;
            } else {
                Monocle.Engine.ClearColor = Color.Black;
            }
        }
    }
}