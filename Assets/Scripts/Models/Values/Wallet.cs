using UnityEngine;

namespace Models.Values
{
    public static class Wallet
    {
        private const string MoneyCountKey = "Wallet.MoneyCount";

        public static int MoneyCount => PlayerPrefs.HasKey(MoneyCountKey) ? PlayerPrefs.GetInt(MoneyCountKey) : 200;

        public static void AddMoney(int value)
        {
            int newMoneyCount = MoneyCount + value;
            
            PlayerPrefs.SetInt(MoneyCountKey, newMoneyCount);
        }

        public static bool TryPurchase(int value)
        {
            bool canPurchase = MoneyCount >= value;

            if (!canPurchase)
            {
                return false;
            }

            int newMoneyCount = MoneyCount - value;
            
            PlayerPrefs.SetInt(MoneyCountKey, newMoneyCount);

            return true;
        }
    }
}