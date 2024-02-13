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
    public static class M1Abrams
    {

        ////MelonPreferences.cfg variables
        static MelonPreferences_Entry<bool> showERAm1;
        static MelonPreferences_Entry<bool> showERAm1_Turret;
        static MelonPreferences_Entry<bool> showERAm1_TurretExtended;
        static MelonPreferences_Entry<bool> showERAm1_TurretRoof;
        static MelonPreferences_Entry<bool> showERAm1_TurretCheek;
        static MelonPreferences_Entry<bool> showERAm1_HullLowerFront;
        static MelonPreferences_Entry<bool> showERAm1_HullUpperFront;
        static MelonPreferences_Entry<bool> showERAm1_HullExtended;

        static MelonPreferences_Entry<bool> showERAm1ip;
        static MelonPreferences_Entry<bool> showERAm1ip_Turret;
        static MelonPreferences_Entry<bool> showERAm1ip_TurretExtended;
        static MelonPreferences_Entry<bool> showERAm1ip_TurretRoof;
        static MelonPreferences_Entry<bool> showERAm1ip_TurretCheek;
        static MelonPreferences_Entry<bool> showERAm1ip_HullLowerFront;
        static MelonPreferences_Entry<bool> showERAm1ip_HullUpperFront;
        static MelonPreferences_Entry<bool> showERAm1ip_HullExtended;

        public static void Config(MelonPreferences_Category cfg)
        {
            showERAm1 = cfg.CreateEntry<bool>("M1 ERA", true);
            showERAm1.Description = "Enable ERA for M1";

            showERAm1_HullLowerFront = cfg.CreateEntry<bool>("M1 Hull Lower Front ERA", false);
            showERAm1_HullLowerFront.Description = "M1 Hull ERA Arrangement";
            showERAm1_HullUpperFront = cfg.CreateEntry<bool>("M1 Hull Upper Front ERA", false);
            showERAm1_HullExtended = cfg.CreateEntry<bool>("M1 Extended Hull ERA", false);

            showERAm1_Turret = cfg.CreateEntry<bool>("M1 Turret ERA", false);
            showERAm1_Turret.Description = "M1 Turret ERA Arrangement";
            showERAm1_TurretRoof = cfg.CreateEntry<bool>("M1 Turret Roof ERA", false);
            showERAm1_TurretCheek = cfg.CreateEntry<bool>("M1 Turret Cheek ERA", false);
            showERAm1_TurretExtended = cfg.CreateEntry<bool>("M1 Extended Turret ERA", false);

            showERAm1ip = cfg.CreateEntry<bool>("M1IP ERA", true);
            showERAm1ip.Description = "Enable ERA for M1IP";

            showERAm1ip_HullLowerFront = cfg.CreateEntry<bool>("M1IP Hull Lower Front ERA", false);
            showERAm1ip_HullLowerFront.Description = "M1IP Hull ERA Arrangement";
            showERAm1ip_HullUpperFront = cfg.CreateEntry<bool>("M1IP Hull Upper Front ERA", false);
            showERAm1ip_HullExtended = cfg.CreateEntry<bool>("M1IP Extended Hull ERA", false);

            showERAm1ip_Turret = cfg.CreateEntry<bool>("M1IP Turret ERA", false);
            showERAm1ip_Turret.Description = "M1IP Turret ERA Arrangement";
            showERAm1ip_TurretRoof = cfg.CreateEntry<bool>("M1IP Turret Roof ERA", false);
            showERAm1ip_TurretCheek = cfg.CreateEntry<bool>("M1IP Turret Cheek ERA", false);
            showERAm1ip_TurretExtended = cfg.CreateEntry<bool>("M1IP Extended Turret ERA", false);
        }

        public static IEnumerator Convert(GameState _)
        {
            ////Apply ARAT
            ////ERA to M1IP
            if (showERAm1ip.Value)
            {
                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (ARAT.ARAT1_m1ip_turret_array == null) continue;

                    if (armor_go.name != "Turret_Armor" && armor_go.name != "HULLARMOR") continue;
                    if (!armor_go.transform.parent.GetComponent<LateFollow>()) continue;

                    string name = armor_go.transform.parent.GetComponent<LateFollow>().ParentUnit.FriendlyName;

                    if (name != "M1IP") continue;

                    if (showERAm1ip_Turret.Value)
                    {
                        if (armor_go.name == "Turret_Armor")
                        {
                            if (armor_go.transform.Find("Turret ERA Array(Clone)")) continue;
                            GameObject m1ipturret_array = GameObject.Instantiate(ARAT.ARAT1_m1ip_turret_array, armor_go.transform);
                            m1ipturret_array.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                            m1ipturret_array.transform.localPosition = new Vector3(0f, 0.9817f, -0.69f);

                            if (!showERAm1ip_TurretExtended.Value)
                            {
                                GameObject.Destroy(m1ipturret_array.transform.Find("Side Plus Array").gameObject);
                            }

                            if (!showERAm1ip_TurretRoof.Value)
                            {
                                GameObject.Destroy(m1ipturret_array.transform.Find("Roof Array").gameObject);
                            }

                            if (!showERAm1ip_TurretCheek.Value)
                            {
                                GameObject.Destroy(m1ipturret_array.transform.Find("Cheek Array").gameObject);
                            }
                        }
                    }

                    if (armor_go.name == "HULLARMOR")
                    {
                        if (armor_go.transform.Find("Hull ERA Array_Doc(Clone)")) continue;
                        GameObject m1iphull_array = GameObject.Instantiate(ARAT.ARAT1_m11ip_hull_array, armor_go.transform);
                        m1iphull_array.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        m1iphull_array.transform.localPosition = new Vector3(0f, 0.9817f, -0.69f);

                        /*if (!showERAm1ip_HullExtended.Value)
                        {
                            GameObject.Destroy(m1iphull_array.transform.Find("Plus Array").gameObject);
                        }

                        if (!showERAm1ip_HullLowerFront.Value)
                        {
                            GameObject.Destroy(m1iphull_array.transform.Find("Lower Front Array").gameObject);
                        }

                        if (!showERAm1ip_HullUpperFront.Value)
                        {
                            GameObject.Destroy(m1iphull_array.transform.Find("Upper Front Array").gameObject);
                        }*/
                    }
                }
            }

            ////ERA to M1
            if (showERAm1.Value)
            {
                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (ARAT.ARAT1_m1_turret_array == null) continue;

                    if (armor_go.name != "M1A0_turret_armour" && armor_go.name != "HULLARMOR") continue;
                    if (!armor_go.transform.parent.GetComponent<LateFollow>()) continue;

                    string name = armor_go.transform.parent.GetComponent<LateFollow>().ParentUnit.FriendlyName;

                    if (name != "M1") continue;


                    if (showERAm1_Turret.Value)
                    {
                        if (armor_go.name == "M1A0_turret_armour")
                        {
                            //if (armor_go.transform.Find("Turret ERA Array(Clone)")) continue;
                            if (armor_go.transform.Find("Turret ERA Array_Doc(Clone)")) continue;
                            GameObject m1turret_array = GameObject.Instantiate(ARAT.ARAT1_m1_turret_array, armor_go.transform);
                            m1turret_array.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                            m1turret_array.transform.localPosition = new Vector3(0f, 0.9817f, -0.69f);

                            if (!showERAm1_TurretExtended.Value)
                            {
                                GameObject.Destroy(m1turret_array.transform.Find("Side Plus Array").gameObject);
                            }

                            if (!showERAm1_TurretRoof.Value)
                            {
                                GameObject.Destroy(m1turret_array.transform.Find("Roof Array").gameObject);
                            }

                            if (!showERAm1_TurretCheek.Value)
                            {
                                GameObject.Destroy(m1turret_array.transform.Find("Cheek Array").gameObject);
                            }
                        }
                    }

                    if (armor_go.name == "HULLARMOR")
                    {
                        if (armor_go.transform.Find("Hull ERA Array_Doc(Clone)")) continue;
                        GameObject m1hull_array = GameObject.Instantiate(ARAT.ARAT1_m11ip_hull_array, armor_go.transform);
                        m1hull_array.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        m1hull_array.transform.localPosition = new Vector3(0f, 0.9817f, -0.69f);

                        if (!showERAm1_HullExtended.Value)
                        {
                            GameObject.Destroy(m1hull_array.transform.Find("Plus Array").gameObject);
                        }

                        if (!showERAm1_HullLowerFront.Value)
                        {
                            GameObject.Destroy(m1hull_array.transform.Find("Lower Front Array").gameObject);
                        }

                        if (!showERAm1_HullUpperFront.Value)
                        {
                            GameObject.Destroy(m1hull_array.transform.Find("Upper Front Array").gameObject);
                        }
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
