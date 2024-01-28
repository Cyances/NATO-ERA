using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GHPC;
using MelonLoader;
using MelonLoader.Utils;
using Thermals;
using UnityEngine;
using GHPC.UI.Tips;
using GHPC.Equipment;
using HarmonyLib;
using System.Reflection;
using System.Reflection.Emit;

namespace NatoEra
{

    public static class PRAT
    {
        public static GameObject PRAT_m60_hull_array;
        public static GameObject PRAT_m60_turret_array;
        private static Texture concrete_tex;
        private static Texture concrete_tex_normal;

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> betterPRAT_CEP;
        static MelonPreferences_Entry<bool> betterPRAT_KEP;
        static MelonPreferences_Entry<float> prat_R;
        static MelonPreferences_Entry<float> prat_G;
        static MelonPreferences_Entry<float> prat_B;
        public static void Config(MelonPreferences_Category cfg)
        {
            betterPRAT_CEP = cfg.CreateEntry<bool>("Improve PRAT CE Protection (500mm)", false);
            betterPRAT_CEP.Description = "Improve PRAT Protection";
            betterPRAT_KEP = cfg.CreateEntry<bool>("Improve PRAT KE Protection (100mm)", false);
            prat_R = cfg.CreateEntry<float>("PRAT R", 71); //Army green default (thanks to Doc for finding the color codes)
            prat_R.Description = "Adjust PRAT colors with RGB values (float)";
            prat_G = cfg.CreateEntry<float>("PRAT G", 80);
            prat_B = cfg.CreateEntry<float>("PRAT B", 65);
        }

        private static void ERA_Setup(Transform[] era_transforms)
        {
            float prat_Red = prat_R.Value / 255f;
            float prat_Green = prat_G.Value / 255f;
            float prat_Blue = prat_B.Value / 255f;
            UnityEngine.Color colour_primary = new UnityEngine.Color(prat_Red, prat_Green, prat_Blue);
            MelonLogger.Msg("PRAT Red value: " + prat_Red);
            MelonLogger.Msg("PRAT Green value: " + prat_Green);
            MelonLogger.Msg("PRAT Blue value: " + prat_Blue);
            UnityEngine.Color[] colours = new UnityEngine.Color[] { colour_primary };

            foreach (Transform transform in era_transforms)
            {
                if (!transform.gameObject.name.Contains("PRAT")) continue;

                transform.gameObject.AddComponent<UniformArmor>();
                UniformArmor armor = transform.gameObject.GetComponent<UniformArmor>();
                armor.SetName("PRAT");
                armor.PrimaryHeatRha = betterPRAT_CEP.Value ? 500f : 370f;
                armor.PrimarySabotRha = betterPRAT_KEP.Value ? 100f : 15f;
                armor.SecondaryHeatRha = 0f;
                armor.SecondarySabotRha = 0f;
                armor._canShatterLongRods = true;
                armor._crushThicknessModifier = 1f;
                armor._isEra = true;

                foreach (GameObject s in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (s.name == "Autocannon HE Armor Impact") { armor.DetonateEffect = s; break; }
                }

                armor.UndetonatedObjects = new GameObject[] { armor.gameObject };

                MeshRenderer mesh_renderer = transform.gameObject.GetComponent<MeshRenderer>();
                mesh_renderer.material = new Material(Shader.Find("Standard (FLIR)"));
                mesh_renderer.material.mainTexture = concrete_tex;
                mesh_renderer.material.mainTextureScale = new Vector2(0.07f, 0.07f);
                mesh_renderer.material.mainTextureOffset = new Vector2(0f, 0f);
                mesh_renderer.material.EnableKeyword("_NORMALMAP");
                mesh_renderer.material.SetTexture("_BumpMap", concrete_tex_normal);

                mesh_renderer.material.color = colours[UnityEngine.Random.Range(0, colours.Length)];

                transform.gameObject.AddComponent<HeatSource>();
            }
        }

        public static void Init()
        {

            if (PRAT.PRAT_m60_turret_array == null)
            {
                var PRAT_bundle_m60_hull = AssetBundle.LoadFromFile(Path.Combine(MelonEnvironment.ModsDirectory + "/NatoEraAssets", "m60_hull_alignment"));
                var PRAT_bundle_m60_turret = AssetBundle.LoadFromFile(Path.Combine(MelonEnvironment.ModsDirectory + "/NatoEraAssets", "m60_turret_alignment"));

                foreach (Texture t in Resources.FindObjectsOfTypeAll<Texture>())
                {
                    if (t.name == "GHPC_ConcretePanels_Diffuse") { concrete_tex = t; break; }
                }   

                foreach (Texture t in Resources.FindObjectsOfTypeAll<Texture>())
                {
                    if (t.name == "GHPC_ConcretePanels_Normal") { concrete_tex_normal = t; break; }
                }

                PRAT_m60_hull_array = PRAT_bundle_m60_hull.LoadAsset<GameObject>("M60 Hull Alignment.prefab");
                PRAT_m60_hull_array.transform.localScale = new Vector3(1f, 1f, 1f);

                PRAT_m60_turret_array = PRAT_bundle_m60_turret.LoadAsset<GameObject>("M60 Turret Alignment.prefab");
                PRAT_m60_turret_array.transform.localScale = new Vector3(1f, 1f, 1f);

                PRAT_m60_hull_array.hideFlags = HideFlags.DontUnloadUnusedAsset;
                PRAT_m60_turret_array.hideFlags = HideFlags.DontUnloadUnusedAsset;

                ERA_Setup(PRAT_m60_hull_array.GetComponentsInChildren<Transform>());
                ERA_Setup(PRAT_m60_turret_array.GetComponentsInChildren<Transform>());
            }
        }
    }

    [HarmonyPatch(typeof(GHPC.Weapons.LiveRound), "penCheck")]
    public class InsensitivePRAT
    {
        private static float pen_threshold = 10f;
        private static float caliber_threshold = 20f;

        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator il)
        {
            var detonate_era = AccessTools.Method(typeof(GHPC.IArmor), "Detonate");
            var is_era = AccessTools.PropertyGetter(typeof(GHPC.IArmor), nameof(GHPC.IArmor.IsEra));
            var pen_rating = AccessTools.PropertyGetter(typeof(GHPC.Weapons.LiveRound), nameof(GHPC.Weapons.LiveRound.CurrentPenRating));
            var debug = AccessTools.Field(typeof(GHPC.Weapons.LiveRound), nameof(GHPC.Weapons.LiveRound.Debug));
            var shot_info = AccessTools.Field(typeof(GHPC.Weapons.LiveRound), nameof(GHPC.Weapons.LiveRound.Info));
            var caliber = AccessTools.Field(typeof(AmmoType), nameof(AmmoType.Caliber));

            var instr = new List<CodeInstruction>(instructions);
            int idx = -1;
            int debug_count = 0;
            Label endof = il.DefineLabel();
            Label exec = il.DefineLabel();

            // find location of if-statement for ERA det code 
            for (int i = 0; i < instr.Count; i++)
            {
                if (instr[i].opcode == OpCodes.Callvirt && instr[i].operand == (object)is_era)
                {
                    idx = i + 5; break;
                }
            }

            // find start of the next if-statement
            for (int i = idx; i < instr.Count; i++)
            {
                if (instr[i].opcode == OpCodes.Ldsfld && instr[i].operand == (object)debug)
                {
                    debug_count++;

                    // IL_0C26
                    if (debug_count == 1) instr[i].labels.Add(exec);


                    // IL_0C6C
                    if (debug_count == 2) { instr[i].labels.Add(endof); break; }
                }
            }

            var custom_instr = new List<CodeInstruction>();
            custom_instr.Add(new CodeInstruction(OpCodes.Ldarg_0));
            custom_instr.Add(new CodeInstruction(OpCodes.Ldfld, shot_info));
            custom_instr.Add(new CodeInstruction(OpCodes.Ldfld, caliber));
            custom_instr.Add(new CodeInstruction(OpCodes.Ldc_R4, caliber_threshold));
            custom_instr.Add(new CodeInstruction(OpCodes.Bge_S, exec));

            custom_instr.Add(new CodeInstruction(OpCodes.Ldarg_0));
            custom_instr.Add(new CodeInstruction(OpCodes.Call, pen_rating));
            custom_instr.Add(new CodeInstruction(OpCodes.Ldc_R4, pen_threshold));
            custom_instr.Add(new CodeInstruction(OpCodes.Ble_Un_S, endof));
            instr.InsertRange(idx, custom_instr);

            return instr;
        }
    }
}
