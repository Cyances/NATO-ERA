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

        static MelonPreferences_Entry<bool> featherARAT;

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

            featherARAT = cfg.CreateEntry<bool>("Feather ARAT", true);
            featherARAT.Description = "Removes ARAT weight penalty";
        }

        public static IEnumerator Convert(GameState _)
        {
            //Mass values here
            int mass_m1ip_Hull = featherARAT.Value ? 0 : 1887;
            int mass_m1ip_HullExtended = featherARAT.Value ? 0 : 769;
            int mass_m1ip_HullLowerFront = featherARAT.Value ? 0 : 295;
            int mass_m1ip_HullUpperFront = featherARAT.Value ? 0 : 1150;
            int mass_m1ip_Turret = featherARAT.Value ? 0 : 737;
            int mass_m1ip_TurretExtended = featherARAT.Value ? 0 : 413;
            int mass_m1ip_TurretRoof = featherARAT.Value ? 0 : 501;
            int mass_m1ip_TurretCheek = featherARAT.Value ? 0 : 383;

            int mass_m1_Hull = featherARAT.Value ? 0 : 1887;
            int mass_m1_HullExtended = featherARAT.Value ? 0 : 769;
            int mass_m1_HullLowerFront = featherARAT.Value ? 0 : 295;
            int mass_m1_HullUpperFront = featherARAT.Value ? 0 : 1150;
            int mass_m1_Turret = featherARAT.Value ? 0 : 678;
            int mass_m1_TurretExtended = featherARAT.Value ? 0 : 413;
            int mass_m1_TurretRoof = featherARAT.Value ? 0 : 501;
            int mass_m1_TurretCheek = featherARAT.Value ? 0 : 383;

            ////Apply ARAT
            ////ERA to M1IP
            if (showERAm1ip.Value)
            {
                foreach (GameObject armor_go in GameObject.FindGameObjectsWithTag("Penetrable"))
                {
                    if (ARAT.ARAT1_m1ip_turret_array == null) continue;

                    if (armor_go.name != "Turret_Armor" && armor_go.name != "HULLARMOR") continue;
                    if (!armor_go.transform.parent.GetComponent<LateFollow>()) continue;

                    string name = armor_go.transform.parent.GetComponent<LateFollow>().ParentUnit.UniqueName;

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
                        if (armor_go.transform.Find("Hull ERA Array(Clone)")) continue;
                        GameObject m1iphull_array = GameObject.Instantiate(ARAT.ARAT1_m11ip_hull_array, armor_go.transform);
                        m1iphull_array.transform.localEulerAngles = new Vector3(0f, 270f, 0f);
                        m1iphull_array.transform.localPosition = new Vector3(0f, 0.9817f, -0.69f);

                        if (!showERAm1ip_HullExtended.Value)
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
                        }
                    }
                }

                foreach (GameObject vic_go in NatoERA.vic_gos)
                {
                    Vehicle vic = vic_go.GetComponent<Vehicle>();

                    if (vic == null) continue;

                    if (vic_go.GetComponent<Util.AlreadyConverted>() != null) continue;
                    if (vic.FriendlyName == "M1IP")
                    {
                        vic_go.AddComponent<Util.AlreadyConverted>();

                        Rigidbody m1ipRb = vic_go.GetComponent<Rigidbody>();

                        m1ipRb.mass += mass_m1ip_Hull;

                        if (showERAm1ip_HullExtended.Value)
                        {
                            m1ipRb.mass += mass_m1ip_HullExtended;
                        }

                        if (showERAm1ip_HullLowerFront.Value)
                        {
                            m1ipRb.mass += mass_m1ip_HullLowerFront;
                        }

                        if (showERAm1ip_HullUpperFront.Value)
                        {
                            m1ipRb.mass += mass_m1ip_HullUpperFront;
                        }

                        if (showERAm1ip_Turret.Value)
                        {
                            m1ipRb.mass += mass_m1ip_Turret;
                        }

                        if (showERAm1ip_TurretExtended.Value)
                        {
                            m1ipRb.mass += mass_m1ip_TurretExtended;
                        }

                        if (showERAm1ip_TurretRoof.Value)
                        {
                            m1ipRb.mass += mass_m1ip_TurretRoof;
                        }

                        if (showERAm1ip_TurretCheek.Value)
                        {
                            m1ipRb.mass += mass_m1ip_TurretCheek;
                        }
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

                    string name = armor_go.transform.parent.GetComponent<LateFollow>().ParentUnit.UniqueName;

                    if (name != "M1") continue;


                    if (showERAm1_Turret.Value)
                    {
                        if (armor_go.name == "M1A0_turret_armour")
                        {
                            if (armor_go.transform.Find("Turret ERA Array(Clone)")) continue;
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
                        if (armor_go.transform.Find("Hull ERA Array(Clone)")) continue;
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

                foreach (GameObject vic_go in NatoERA.vic_gos)
                {
                    Vehicle vic = vic_go.GetComponent<Vehicle>();

                    if (vic == null) continue;

                    if (vic_go.GetComponent<Util.AlreadyConverted>() != null) continue;
                    if (vic.FriendlyName == "M1")
                    {
                        vic_go.AddComponent<Util.AlreadyConverted>();

                        Rigidbody m1Rb = vic_go.GetComponent<Rigidbody>();

                        m1Rb.mass += mass_m1_Hull;
                        

                        if (showERAm1_HullExtended.Value)
                        {
                            m1Rb.mass += mass_m1_HullExtended;
                        }

                        if (showERAm1_HullLowerFront.Value)
                        {
                            m1Rb.mass += mass_m1_HullLowerFront;
                        }

                        if (showERAm1_HullUpperFront.Value)
                        {
                            m1Rb.mass += mass_m1_HullUpperFront;
                        }

                        if (showERAm1_Turret.Value)
                        {
                            m1Rb.mass += ARAT.ipModel.Value ? mass_m1ip_Turret  : mass_m1_Turret;
                        }

                        if (showERAm1_TurretExtended.Value)
                        {
                            m1Rb.mass += mass_m1_TurretExtended;
                        }

                        if (showERAm1_TurretRoof.Value)
                        {
                            m1Rb.mass += mass_m1_TurretRoof;
                        }

                        if (showERAm1_TurretCheek.Value)
                        {
                            m1Rb.mass += mass_m1_TurretCheek;
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
