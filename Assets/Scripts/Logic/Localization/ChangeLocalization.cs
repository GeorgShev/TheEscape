using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Logic.Localization
{
    public class ChangeLocalization : MonoBehaviour 
    {
        private bool _active = false;
 

        public void ChangeLocate(int localeID)
        {
            if (_active)
            {
                return;
            }
            StartCoroutine(SetLocale(localeID));
        }


        IEnumerator SetLocale(int localeID)
        {
            _active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            _active = false;
        }
    }
}
