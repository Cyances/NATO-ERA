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

    public static class ARAT
    {
        public static GameObject ARAT1_m1_turret_array;
        public static GameObject ARAT1_m11ip_hull_array;
        public static GameObject ARAT1_m1ip_turret_array;
        private static Texture concrete_tex;
        private static Texture concrete_tex_normal;

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> betterARAT_CEP;
        static MelonPreferences_Entry<bool> betterARAT_KEP;
        static MelonPreferences_Entry<float> arat_R;
        static MelonPreferences_Entry<float> arat_G;
        static MelonPreferences_Entry<float> arat_B;

        public static MelonPreferences_Entry<bool> ipModel;

        public static ArmorCodexScriptable armor_codex_arat1 = null;
        public static ArmorType armor_arat1 = new ArmorType();
        public static void Config(MelonPreferences_Category cfg)
        {
            ipModel = cfg.CreateEntry<bool>("IP model to M1", false);
            ipModel.Description = "Set to true if base M1 is using IP model (AMP mod)";

            betterARAT_CEP = cfg.CreateEntry<bool>("Improve CE Protection (600mm)", false);
            betterARAT_CEP.Description = "Improve ARAT Protection";
            betterARAT_KEP = cfg.CreateEntry<bool>("Improve KE Protection (150mm)", false);
            arat_R = cfg.CreateEntry<float>("ARAT R", 71); //Army green default (thanks to Doc for finding the color codes)
            arat_R.Description = "Adjust ARAT colors with RGB values (float)";
            arat_G = cfg.CreateEntry<float>("ARAT G", 80);
            arat_B = cfg.CreateEntry<float>("ARAT B", 65);
        }

        private static void ERA_Setup(Transform[] era_transforms)
        {
            float arat_Red = arat_R.Value / 255f;
            float arat_Green = arat_G.Value / 255f;
            float arat_Blue = arat_B.Value / 255f;
            UnityEngine.Color colour_primary = new UnityEngine.Color(arat_Red, arat_Green, arat_Blue);
            MelonLogger.Msg("ARAT Red value: " + arat_Red);
            MelonLogger.Msg("ARAT Green value: " + arat_Green);
            MelonLogger.Msg("ARAT Blue value: " + arat_Blue);
            UnityEngine.Color[] colours = new UnityEngine.Color[] { colour_primary };

            foreach (Transform transform in era_transforms)
            {
                if (!transform.gameObject.name.Contains("ARAT-1")) continue;

                transform.gameObject.AddComponent<BoxCollider>();

                transform.gameObject.AddComponent<UniformArmor>();
                UniformArmor armor = transform.gameObject.GetComponent<UniformArmor>();
                armor.SetName("ARAT-1");
                armor.PrimaryHeatRha = betterARAT_CEP.Value ? 600f : 450f;
                armor.PrimarySabotRha = betterARAT_KEP.Value ? 150f : 30f;
                armor.SecondaryHeatRha = 0f;
                armor.SecondarySabotRha = 0f;
                armor._canShatterLongRods = true;
                armor._crushThicknessModifier = 1f;
                armor._isEra = true;

                if (armor_codex_arat1 == null)
                {
                    armor_codex_arat1 = ScriptableObject.CreateInstance<ArmorCodexScriptable>();
                    armor_codex_arat1.name = "ARAT-1 Armor Codex";
                    armor_arat1.RhaeMultiplierKe = 1f;
                    armor_arat1.RhaeMultiplierCe = 1f;
                    armor_arat1.CanRicochet = false;
                    armor_arat1.CrushThicknessModifier = 1f;
                    armor_arat1.NormalizesHits = true;
                    armor_arat1.CanShatterLongRods = false;
                    armor_arat1.ThicknessSource = ArmorType.RhaSource.Multipliers;
                    armor_arat1.Name = "ARAT-1 Armor";

                    armor_codex_arat1.ArmorType = armor_arat1;
                }

                armor._armorType = armor_codex_arat1;

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

            if (ARAT1_m1_turret_array == null)
            {
                var ARAT1_bundle_m11ip_hull = AssetBundle.LoadFromFile(Path.Combine(MelonEnvironment.ModsDirectory + "/NatoEraAssets", "abrams_hull_arat"));
                var ARAT1_bundle_m1_turret = AssetBundle.LoadFromFile(Path.Combine(MelonEnvironment.ModsDirectory + "/NatoEraAssets", "abrams_m1_turret_arat"));
                var ARAT1_bundle_m1ip_turret = AssetBundle.LoadFromFile(Path.Combine(MelonEnvironment.ModsDirectory + "/NatoEraAssets", "abrams_m1ip_turret_arat"));

                foreach (Texture t in Resources.FindObjectsOfTypeAll<Texture>())
                {
                    if (t.name == "GHPC_ConcretePanels_Diffuse") { concrete_tex = t; break; }
                }   

                foreach (Texture t in Resources.FindObjectsOfTypeAll<Texture>())
                {
                    if (t.name == "GHPC_ConcretePanels_Normal") { concrete_tex_normal = t; break; }
                }

                ARAT1_m11ip_hull_array = ARAT1_bundle_m11ip_hull.LoadAsset<GameObject>("Hull ERA Array.prefab");
                ARAT1_m11ip_hull_array.transform.localScale = new Vector3(1f, 1f, 1f);

                ARAT1_m1_turret_array = ipModel.Value ? ARAT1_bundle_m1ip_turret.LoadAsset<GameObject>("Turret ERA Array M1IP.prefab") : ARAT1_bundle_m1_turret.LoadAsset<GameObject>("Turret ERA Array.prefab");
                ARAT1_m1_turret_array.transform.localScale = new Vector3(1f, 1f, 1f);

                ARAT1_m1ip_turret_array = ARAT1_bundle_m1ip_turret.LoadAsset<GameObject>("Turret ERA Array M1IP.prefab");
                ARAT1_m1ip_turret_array.transform.localScale = new Vector3(1f, 1f, 1f);

                ARAT1_m11ip_hull_array.hideFlags = HideFlags.DontUnloadUnusedAsset;
                ARAT1_m1_turret_array.hideFlags = HideFlags.DontUnloadUnusedAsset;
                ARAT1_m1ip_turret_array.hideFlags = HideFlags.DontUnloadUnusedAsset;

                ERA_Setup(ARAT1_m1_turret_array.GetComponentsInChildren<Transform>());
                ERA_Setup(ARAT1_m11ip_hull_array.GetComponentsInChildren<Transform>());
                ERA_Setup(ARAT1_m1ip_turret_array.GetComponentsInChildren<Transform>());
            }
        }
    }

    [HarmonyPatch(typeof(GHPC.Weapons.LiveRound), "penCheck")]
    public class InsensitiveARAT
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
