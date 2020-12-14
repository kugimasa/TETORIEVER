using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER
{
    public class TextReadPieceSource : MonoBehaviour, IPieceSource
    {
        [SerializeField] TextAsset m_textData;
        public Cell[][] PieceDatas => ReadText();
        Cell[][] ReadText()
        {
            var result =new List<Cell[]>();
            var text = m_textData.text;
            var mc = System.Text.RegularExpressions.Regex.Matches(
                text, @"{(.*?)}\((.*?)\)",System.Text.RegularExpressions.RegexOptions.Singleline
                );
            foreach(System.Text.RegularExpressions.Match m in mc)
            {
                //Debug.Log($"{m.Groups[1].Value},{m.Groups[2].Value}");
                result.Add( ConvertText2Cells(m.Groups[1].Value,m.Groups[2].Value).ToArray());
            }

            return result.ToArray();
        }

        List<Cell> ConvertText2Cells(string value, string index)
        {
            List<Cell> result = new List<Cell>();
            var indexdata = index.Trim().Split(',');
            int center_x = int.Parse(indexdata[0]);
            int center_y = int.Parse(indexdata[1]);
            var texts = value.Trim().Split('\n');
            for (int y = 0; y < texts.Length; y++)
            {
                for (int x = 0; x < texts[y].Length; x++)
                {
                    switch (texts[y][x])
                    {
                        case '_': break;
                        case 'x': result.Add(new Cell(new Vector2Int(x, y) - new Vector2Int(center_x, center_y), Cell.CellType.Normal)); break;
                        case 'o': result.Add(new Cell(new Vector2Int(x, y) - new Vector2Int(center_x, center_y), Cell.CellType.Vanish)); break;
                    }
                }
            }
            return result;
        }
    }
}