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
        static MelonPreferences_Entry<bool> showERAm2_HullSideExtended;
        static MelonPreferences_Entry<bool> showERAm2_HullSideLower;
        static MelonPreferences_Entry<bool> showERAm2_HullSideLowerExtended;
        static MelonPreferences_Entry<bool> showERAm2_HullLowerFront;
        static MelonPreferences_Entry<bool> showERAm2_HullUpper;

        public static void Config(MelonPreferences_Category cfg)
        {
            showERAm2 = cfg.CreateEntry<bool>("M2 ERA", true);
            showERAm2.Description = "Enable ERA for M2";

            showERAm2_HullSideExtended = cfg.CreateEntry<bool>("M2 Extended Hull Side ERA", false);
            showERAm2_HullSideExtended.Description = "M2 Hull ERA Arrangement";
            showERAm2_HullSideLower = cfg.CreateEntry<bool>("M2 Hull Side Lower ERA", true);
            showERAm2_HullSideLowerExtended = cfg.CreateEntry<bool>("M2 Extended Hull Side Lower ERA", false);
            showERAm2_HullLowerFront = cfg.CreateEntry<bool>("M2 Hull Lower Front ERA", false);
            showERAm2_HullUpper = cfg.CreateEntry<bool>("M2 Hull Upper ERA", false);
        }

        public static IEnumerator Convert(GameState _)
        {
            ////Apply BRAT
            if (showERAm2.Value)
            {
                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (BRAT.BRAT_m2_hull_array == null) continue;
                    //if (BRAT.BRAT_m2_turret_array == null) continue;
                    if (!armor_go.GetComponent<LateFollow>()) continue;

                    //string name = armor_go.GetComponent<LateFollow>().ParentUnit.FriendlyName;
                    string name = armor_go.GetComponent<LateFollow>().ParentUnit.UniqueName;

                    if (name != "M2BRADLEY") continue;

                    if (armor_go.name == "HULL")
                    {
                        if (armor_go.transform.Find("Hull Front Alu 5083/M2 Hull ERA Array(Clone)")) continue;
                        GameObject m2_hull_array = GameObject.Instantiate(BRAT.BRAT_m2_hull_array, armor_go.transform.Find("Hull Front Alu 5083"));
                        m2_hull_array.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        m2_hull_array.transform.localPosition = new Vector3(0f, 0f, 0f);

                        if (!showERAm2_HullSideLower.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Lower Array").gameObject);
                        }

                        if (!showERAm2_HullSideLowerExtended.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Lower Extended Array").gameObject);
                        }

                        if (!showERAm2_HullSideExtended.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Side Extended Array").gameObject);
                        }

                        if (!showERAm2_HullLowerFront.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Front Lower Array").gameObject);
                        }

                        if (!showERAm2_HullUpper.Value)
                        {
                            GameObject.Destroy(m2_hull_array.transform.Find("Hull Upper Array").gameObject);
                        }

                        //GameObject.Destroy(m2_hull_array.transform.Find("Front Upper Array").gameObject);
                        //GameObject.Destroy(m2_hull_array.transform.Find("Front Headlight Array").gameObject);

                    }

                    /*if (name == "M2 Bradley") continue;

                    if (armor_go.name == "Turret")
                    {
                        if (armor_go.transform.Find("Turret Front Alu 7039 1\"/M2 Turret Alignment(Clone)")) continue;
                        GameObject turret_array = GameObject.Instantiate(BRAT.BRAT_m2_turret_array, armor_go.transform.Find("Turret Front Alu 7039 1\""));
                        turret_array.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
                        turret_array.transform.localPosition = new Vector3(0f, 0f, -1.45f);
                    }*/
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
