using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    /// <summary>
    /// ブロックデータを登録する
    /// </summary>
    public interface ISetPieceData
    {
        void SetPieceData(PieceData data, int index);
    }
    /// <summary>
    /// ブロックデータを取得する
    /// </summary>
    public interface IGetPieceData
    {
        PieceData GetPieceData(int hand);
    }
    /// <summary>
    /// ブロックデータを画面に表示する
    /// </summary>
    public interface IPieceDisplayer
    {
        void CreatePiece(PieceData blockData);
        void SetPosition(Vector3 centerPosition);
    }

    /// <summary>
    /// 選択、非選択の画面表示を行うもの
    /// </summary>
    public interface IDisplaySelected
    {
        void Select();
        void DisSelect();
    }

}