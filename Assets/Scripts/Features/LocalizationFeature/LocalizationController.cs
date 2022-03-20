using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Features.LocalizationFeature
{
    public class LocalizationController : MonoBehaviour
    {
        #region Fields

        private bool _isInitialized;

        #endregion

        #region Properties

        public List<Locale> ExistingLocales {get; private set;}

        #endregion

        #region UnityMethods

        private void Start()
        {
            StartCoroutine(WaitLocale());
            LocalizationSettings.SelectedLocaleChanged += ChangeLocale;
        }

        #endregion

        #region Methods

        private IEnumerator WaitLocale()
        {
            yield return LocalizationSettings.InitializationOperation;
            ExistingLocales = LocalizationSettings.AvailableLocales.Locales;
            _isInitialized = true;
        }

        private void ChangeLocale(Locale locale)
        {
            if (!_isInitialized)
                return;
            LocalizationSettings.SelectedLocale = locale;
        }

        #endregion
    }
}
