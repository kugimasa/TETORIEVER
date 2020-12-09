using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;

namespace TETORIEVER.PieceSelect
{
    public class SimpleDisplaySelected : MonoBehaviour,IDisplaySelected
    {
        [SerializeField] GameObject target;


        public void Select()
        {
            target.SetActive(true);
        }

        public void DisSelect()
        {
            target.SetActive(false);
        }
    }
}