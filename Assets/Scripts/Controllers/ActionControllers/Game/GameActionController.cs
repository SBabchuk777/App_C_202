using System;

namespace Controllers.ActionControllers.Game
{
    public class GameActionController
    {
        public Action<bool> BlockHasBeenDestroy { get; set; }
        public Action<int, int> ChoseItem { get; set; }

        public Action LineHasBeenDestroy { get; set; }

        public Action GameOver { get; set; }
    }
}