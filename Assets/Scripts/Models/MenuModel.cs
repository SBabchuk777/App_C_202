using System;
using UnityEngine;

namespace Models
{
    public class MenuModel
    {
        private const string LastDayOpenBonusKey = "MenuModel.LastDayOpenBonus";

        public bool CanShowBonusBtn => !PlayerPrefs.HasKey(LastDayOpenBonusKey)
                                       || PlayerPrefs.GetInt(LastDayOpenBonusKey) != DateTime.UtcNow.Day;

        public void SetLastDayOpenBonus()
        {
            PlayerPrefs.SetInt(LastDayOpenBonusKey,DateTime.UtcNow.Day);
        }

    }
}