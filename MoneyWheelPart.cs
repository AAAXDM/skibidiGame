using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class MoneyWheelPart : MonoBehaviour
{
    [SerializeField] int coins;

    public int Coins => coins;
    public Action<MoneyWheelPart> Collision;

    void OnTriggerEnter2D(Collider2D collision) => Collision(this);
}