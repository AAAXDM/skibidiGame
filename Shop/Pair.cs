using System;
using UnityEngine;


    [Serializable]
    public struct Pair <T1,T2>
    {
        [SerializeField] T1 key;
        [SerializeField] T2 value;

        public T1 Key => key;
        public T2 Value => value;

        public Pair(T1 key, T2 value)
        {
            this.key = key;
            this.value = value;
        }
    }
