﻿using Frixu.BouncyHero.Extensions;
using Frixu.BouncyHero.Managers;
using Unity.Entities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Frixu.BouncyHero.Scripts
{
    public static class BouncyHeroBootstrap
    {
        private static bool RunPreviously;

        /// <summary> Loads needed scenes and game managers. </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static async void Bootstrap()
        {
            if (RunPreviously) return;
            RunPreviously = true;
            World.Active.GetOrCreateSystem<GameManager>();
            World.Active.GetOrCreateSystem<LifeManager>();
            World.Active.GetOrCreateSystem<TimeManager>();
            World.Active.GetOrCreateSystem<ThemeManager>();
            await World.Active.GetOrCreateSystem<PlayerDataManager>().Load();

            #if !UNITY_EDITOR
            await SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
            Debug.Log("The gameplay scene has been loaded.");
            await SceneManager.LoadSceneAsync("UserInterface", LoadSceneMode.Additive);
            Debug.Log("The UI scene has been loaded.");
            #endif

            Debug.Log("The game has been successfully bootstrapped.");
        }
    }
}
