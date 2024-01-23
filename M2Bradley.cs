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
    public static class M2Bradley
    {

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> showERAm2;

        public static void Config(MelonPreferences_Category cfg)
        {
            showERAm2 = cfg.CreateEntry<bool>("M2 ERA", true);
            showERAm2.Description = "Enable ERA for M2";
        }

        public static IEnumerator Convert(GameState _)
        {
            ////Apply BRAT
            ////ERA to M2
            ////Turret/Turret Front Alu 7039 1"
            ////HULL/Hull Front Alu 5083
            //0.549 1.26 1.376
            if (showERAm2.Value)
            {

                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (BRAT.BRAT_m2_hull_array == null) continue;
                    if (BRAT.BRAT_m2_turret_array == null) continue;
                    if (!armor_go.GetComponent<LateFollow>()) continue;

                    string name = armor_go.GetComponent<LateFollow>().ParentUnit.FriendlyName;

                    if (name == "M2 Bradley") continue;

                    if (armor_go.name == "HULL")
                    {
                        if (armor_go.transform.Find("Hull Front Alu 5083/M2 Hull Alignment(Clone)")) continue;
                        GameObject hull_array = GameObject.Instantiate(BRAT.BRAT_m2_hull_array, armor_go.transform.Find("Hull Front Alu 5083"));
                        hull_array.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        hull_array.transform.localPosition = new Vector3(0f, 0f, 0f);

                    }

                    if (name == "M2 Bradley") continue;

                    if (armor_go.name == "Turret")
                    {
                        if (armor_go.transform.Find("Turret Front Alu 7039 1\"/M2 Turret Alignment(Clone)")) continue;
                        GameObject turret_array = GameObject.Instantiate(BRAT.BRAT_m2_turret_array, armor_go.transform.Find("Turret Front Alu 7039 1\""));
                        turret_array.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        turret_array.transform.localPosition = new Vector3(0f, 0f, -1.45f);
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
