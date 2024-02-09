using UnityEngine;

namespace Models
{
    public class MainModel
    {
        private const string ClearBoardBoosterKey = "MainModel.ClearBoardBooster";
        private const string DestroyBlockBoosterKey = "MainModel.DestroyBlockBooster";

        public int ClearBoardBoosterCount
        {
            get => PlayerPrefs.HasKey(ClearBoardBoosterKey) ? PlayerPrefs.GetInt(ClearBoardBoosterKey) : 1;
            set => PlayerPrefs.SetInt(ClearBoardBoosterKey, value);
        }

        public int DestroyBlockBoosterCount
        {
            get => PlayerPrefs.HasKey(DestroyBlockBoosterKey) ? PlayerPrefs.GetInt(DestroyBlockBoosterKey) : 1;
            set => PlayerPrefs.SetInt(DestroyBlockBoosterKey, value);
        }
    }
}