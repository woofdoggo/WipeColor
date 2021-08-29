using Celeste.Editor;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;

namespace Celeste.Mod.WipeColor {
    public partial class WipeColorModule : EverestModule {
        private static void MapEditorCtorHook(On.Celeste.Editor.MapEditor.orig_ctor orig, MapEditor self, AreaKey area, bool reloadMapData = true) {
            orig(self, area, reloadMapData);

            if (!Settings.ApplyToDebugMap) {
                Engine.ClearColor = Color.Black;
            }
        }

        private static void MapEditorBetterHook(On.Celeste.Editor.MapEditor.orig_MakeMapEditorBetter orig, MapEditor self) {
            orig(self);
            DynData<Engine> data = new DynData<Engine>(Engine.Instance);

            if (data.Get<Scene>("nextScene").GetType() == typeof(LevelLoader)) {
                ApplyClearColor();
            }
        }

        private static void MapEditorUpdateHook(On.Celeste.Editor.MapEditor.orig_Update orig, MapEditor self) {
            orig(self);
            DynData<Engine> data = new DynData<Engine>(Engine.Instance);

            if (data.Get<Scene>("nextScene").GetType() == typeof(LevelLoader)) {
                ApplyClearColor();
            }
        }
    }
}