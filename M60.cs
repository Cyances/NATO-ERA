using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GHPC.Camera;
using GHPC.Player;
using MelonLoader;
using UnityEngine;
using GHPC.State;
using System.Collections;
using GHPC.Weapons;
using GHPC.Equipment.Optics;
using GHPC.Vehicle;
using System.Reflection;
using Reticle;
using GHPC.Equipment;
using GHPC.Utility;
using GHPC;
using HarmonyLib;

namespace NatoEra
{
    public static class M60
    {

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> showERAm60;

        public static void Config(MelonPreferences_Category cfg)
        {
            showERAm60 = cfg.CreateEntry<bool>("M60A3 ERA", true);
            showERAm60.Description = "Enable ERA for M60A3";
        }

        public static IEnumerator Convert(GameState _)
        {
            ////Apply PRAT
            ////ERA to M60A3
            ////TURRET ARMOR/ARMOR/TURRET EXTRA
            ////HULL ARMOR/ARMOR/HULL EXTRA

            if (showERAm60.Value)
            {
                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (PRAT.PRAT_m60_turret_array == null) continue;

                    if (armor_go.name != "TURRET ARMOR" && armor_go.name != "HULL ARMOR") continue;
                    if (!armor_go.transform.parent.GetComponent<LateFollow>()) continue;

                    string name = armor_go.transform.parent.GetComponent<LateFollow>().ParentUnit.FriendlyName;

                    if (name != "M60A3 TTS") continue;
                    {
                        if (armor_go.name == "TURRET ARMOR")
                        {
                            if (armor_go.transform.Find("M60 Turret Alignment(Clone)")) continue;
                            GameObject m60turret_array = GameObject.Instantiate(PRAT.PRAT_m60_turret_array, armor_go.transform);
                            m60turret_array.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                            m60turret_array.transform.localPosition = new Vector3(0f, 0f, 0f);
                        }
                    }

                    if (armor_go.name == "HULL ARMOR")
                    {
                        if (armor_go.transform.Find("M60 Hull Alignment(Clone)")) continue;
                        GameObject m60hull_array = GameObject.Instantiate(PRAT.PRAT_m60_hull_array, armor_go.transform);
                        m60hull_array.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                        m60hull_array.transform.localPosition = new Vector3(0f, 0f, 0f);
                    }
                }
            }




            yield break;
        }
        public static void Init()
        {
            StateController.RunOrDefer(GameState.GameReady, new GameStateEventHandler(Convert), GameStatePriority.Lowest);
        }
    }
}
