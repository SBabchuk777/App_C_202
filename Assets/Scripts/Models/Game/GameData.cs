using System.Collections.Generic;
using UnityEngine;
using Enums.Game;

namespace Models.Game
{
    public static class GameData
    {
        public static readonly float _cos = Mathf.Cos(Mathf.PI / 2f);
        public static readonly float _sin = Mathf.Sin(Mathf.PI / 2f);
        public static readonly float[] _rotationMatrix = new float[] { _cos, _sin, -_sin, -_cos };

        public static readonly Dictionary<Tetromino, Vector2[]> Cells = new Dictionary<Tetromino, Vector2[]>()
        {
            {
                Tetromino.I,
                new Vector2[] { new Vector2(-1, 1), new Vector2(0, 1), new Vector2(1, 1), new Vector2(2, 1) }
            },
            {
                Tetromino.J,
                new Vector2[] { new Vector2(-1, 1), new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0) }
            },
            {
                Tetromino.L,
                new Vector2[] { new Vector2(1, 1), new Vector2(-1, 0), new Vector2(0, 0), new Vector2(1, 0) }
            },
            {
                Tetromino.O,
                new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(0, 0), new Vector2(1, 0) }
            },
            {
                Tetromino.S,
                new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), new Vector2(-1, 0), new Vector2(0, 0) }
            },
            {
                Tetromino.T,
                new Vector2[] { new Vector2(0, 1), new Vector2(-1, 0), new Vector2(0, 0), new Vector2(-1, 0) }
            },
            {
                Tetromino.I,
                new Vector2[] { new Vector2(-1, 1), new Vector2(0, 1), new Vector2(0, 0), new Vector2(1, 0) }
            },
        };

        private static readonly Vector2[,] WallKickI = new Vector2[,]
        {
            {new Vector2(0,0), new Vector2(-2,0), new Vector2(1,0), new Vector2(-2,-1), new Vector2(1,2)},
            {new Vector2(0,0), new Vector2(2,0), new Vector2(-1,0), new Vector2(2,1), new Vector2(-1,-2)},
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(2,0), new Vector2(-1,2), new Vector2(2,-1)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(-2,0), new Vector2(1,-2), new Vector2(-2,1)},
            {new Vector2(0,0), new Vector2(2,0), new Vector2(-1,0), new Vector2(2,1), new Vector2(-1,-2)},
            {new Vector2(0,0), new Vector2(-2,0), new Vector2(1,0), new Vector2(-2,-1), new Vector2(1,2)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(-2,0), new Vector2(1,-2), new Vector2(-2,1)},
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(2,0), new Vector2(-1,-2), new Vector2(2,-1)},
        };
        
        private static readonly Vector2[,] WallKickJLostZ = new Vector2[,]
        {
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(-1,1), new Vector2(0,-2), new Vector2(-1,-2)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(1,-1), new Vector2(0,2), new Vector2(1,2)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(1,-1), new Vector2(0,2), new Vector2(1,2)},
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(-1,1), new Vector2(0,-2), new Vector2(1,-2)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,-2), new Vector2(1,-2)},
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(-1,-1), new Vector2(0,2), new Vector2(-1,2)},
            {new Vector2(0,0), new Vector2(-1,0), new Vector2(-1,-1), new Vector2(0,2), new Vector2(-1,2)},
            {new Vector2(0,0), new Vector2(1,0), new Vector2(1,1), new Vector2(0,-2), new Vector2(1,-2)},
        };

        public static readonly Dictionary<Tetromino, Vector2[,]> WallKicks = new Dictionary<Tetromino, Vector2[,]>()
        {
            { Tetromino.I, WallKickI },
            { Tetromino.J, WallKickJLostZ },
            { Tetromino.L, WallKickJLostZ },
            { Tetromino.O, WallKickJLostZ },
            { Tetromino.S, WallKickJLostZ },
            { Tetromino.T, WallKickJLostZ },
            { Tetromino.Z, WallKickJLostZ },
        };
    }
}