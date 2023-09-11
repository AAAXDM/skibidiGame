using UnityEngine;

public class MapPart : Obstacle
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.transform.tag == "EndPoint") CollisionAction.Invoke(this);
    }

    public override void SetSpeed(int speed)
    {
        this.speed = speed;
        Stop();
        AddForce();
    }
}
