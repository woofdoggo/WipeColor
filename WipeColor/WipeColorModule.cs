using Microsoft.Xna.Framework;
using Monocle;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Reflection;

namespace Celeste.Mod.WipeColor {
    public partial class WipeColorModule : EverestModule {
        public static WipeColorModule Instance;
    
        public override Type SettingsType => typeof(WipeColorSettings);
        public static WipeColorSettings Settings => (WipeColorSettings) Instance._Settings;

        public static Color WipeColor = Color.Black;
        private static FieldInfo WipeColorField = typeof(WipeColorModule).GetField("WipeColor", BindingFlags.Public | BindingFlags.Static);

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

            // Extra hooks
            IL.Celeste.FadeWipe.Render += FadeRenderHook;
            IL.Celeste.HeartWipe.Render += HeartRenderHook;
            IL.Celeste.SpotlightWipe.Render += SpotlightRenderHook;
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
        }

        private static void FadeRenderHook(ILContext context) {
            ILCursor cursor = new ILCursor(context);

            while (cursor.TryGotoNext(MoveType.After, instr => instr.MatchLdsfld("Celeste.ScreenWipe", "WipeColor"))) {
                Logger.Log("WipeColor/FadeRender", $"Applying wipe color hook to {cursor.Index} in {cursor.Method.FullName}");

                cursor.Index--;
                cursor.Remove();
                cursor.Emit(OpCodes.Ldsfld, WipeColorField);
            }
        }

        private static void HeartRenderHook(ILContext context) {
            ILCursor cursor = new ILCursor(context);

            while (cursor.TryGotoNext(MoveType.After, instr => instr.MatchLdsfld("Celeste.ScreenWipe", "WipeColor"))) {
                Logger.Log("WipeColor/HeartRender", $"Applying wipe color hook to {cursor.Index} in {cursor.Method.FullName}");

                cursor.Index--;
                cursor.Remove();
                cursor.Emit(OpCodes.Ldsfld, WipeColorField);
            }
        }

        private static void SpotlightRenderHook(ILContext context) {
            ILCursor cursor = new ILCursor(context);
            
            while (cursor.TryGotoNext(MoveType.After, instr => instr.MatchLdsfld("Celeste.ScreenWipe", "WipeColor"))) {
                Logger.Log("WipeColor/SpotlightRender", $"Applying wipe color hook to {cursor.Index} in {cursor.Method.FullName}");

                cursor.Index--;
                cursor.Remove();
                cursor.Emit(OpCodes.Ldsfld, WipeColorField);
            }
        }

        // TODO: Make apply background color better
        // TODO: Core, summit vignettes
    }
}