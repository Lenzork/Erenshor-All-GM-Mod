using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MelonLoader;
using Steamworks;


namespace Erenshor_All_GM_Mod
{
    public class AllGM : MelonMod
    {
        private string steamPlayerName;

        public override void OnInitializeMelon()
        {
            LoggerInstance.Msg("##########################################");
            LoggerInstance.Msg("#       ERENSHOR DEMO ALL GM MOD         #");
            LoggerInstance.Msg("##########################################");
            LoggerInstance.Msg("Version 0.1.0 loaded!");
        }

        public override void OnLateInitializeMelon()
        {
            // Fetch Players Steam Name
            steamPlayerName = SteamFriends.GetPersonaName().ToLower();

            LoggerInstance.Msg($"Players Steam Name has been fetched {steamPlayerName}");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            if (sceneName != "LoadScene")
                return;

            LoggerInstance.Msg($"{sceneName}.{buildIndex} has been loaded!");

            if(GameData.GM.DemoBuild)
            {
                // Trick Game into thinking this is not the demo so we can use all commands
                GameData.GM.DemoBuild = false;
                LoggerInstance.Msg($"DemoBuild is now {GameData.GM.DemoBuild}");
            }

            try
            {
                List<List<string>> groups = new List<List<string>> { GameData.GM.DevTeam, GameData.GM.WikiTeam, GameData.GM.Patron };

                if (steamPlayerName != null)
                {
                    // Add To Groups
                    foreach (var group in groups)
                    {
                        if (!group.Contains(steamPlayerName))
                        {
                            group.Add(steamPlayerName);
                        }
                    }

                    LoggerInstance.Msg($"{steamPlayerName} has been added to groups.");
                }
                else
                {
                    LoggerInstance.Msg("Player Name was not found! Maybe no connection to Steam?");
                }
            }
            catch (Exception ex)
            {
                LoggerInstance.Msg(ex.ToString());
            }
            
        }
    }
}
