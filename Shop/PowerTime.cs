using System;
using UnityEngine;

namespace Shop
{
    [Serializable]
    public class PowerTime : Item
    {
        [SerializeField] float time;
        [SerializeField] float deltaTime;
        [SerializeField] int iteration = 1;
        [SerializeField] int priceDelta;

        public float Time => time;

        public override void Buy()
        {
            time += deltaTime;
            iteration++;
            if(time > PlayerProgress.Instance.SummaryStats.MaxPowerTime)
            {
                time = PlayerProgress.Instance.SummaryStats.MaxPowerTime;
            }

            if (time == PlayerProgress.Instance.SummaryStats.MaxPowerTime)
            {
                isByuing = true;
            }
            else
            {
                price += iteration * priceDelta;
            }
        }
    }
}