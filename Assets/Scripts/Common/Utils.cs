using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public static class Utils
    {
        private const string PlayerNamePrefsKey = "PlayerName";

        public static string PlayerName
        {
            get => PlayerPrefs.GetString(PlayerNamePrefsKey);
            set => PlayerPrefs.SetString(PlayerNamePrefsKey, value);
        }

        public static IEnumerator MakeActionDelay(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action.Invoke();
        }
    }
}