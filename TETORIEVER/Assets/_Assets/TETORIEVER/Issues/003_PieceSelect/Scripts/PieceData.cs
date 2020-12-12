using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    public class PieceData
    {
        public Cell[,] BlockData { get; private set; }

        public PieceData(Cell[,] blockData)
        {
            BlockData = blockData;
        }
    }
}