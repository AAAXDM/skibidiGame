using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class Item : IItem
    {
        [SerializeField] int id;
        [SerializeField] protected int price;
        [SerializeField] protected bool isByuing;

        public int Id => id;
        public int Price => price;
        public bool IsByuing => isByuing;

        public virtual void Buy() => isByuing = true;
    }
}