using UnityEngine;

public class SquishByHands : MonoBehaviour
{
    private Collider2D collider;
    void Start()
    {
        collider = GetComponent<Collider2D>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] currentCollisions = new Collider2D[2];
        ContactFilter2D filter2D = new ContactFilter2D();
        LayerMask handsMask = LayerMask.GetMask("Hands");
        filter2D.SetLayerMask(handsMask);
        
        collider.OverlapCollider(filter2D, currentCollisions);
        if (currentCollisions[0] != null && currentCollisions[1] != null)
        {
            if (currentCollisions[0].bounds.Intersects(currentCollisions[1].bounds))
            {
                // is colliding with both hands and hands are touching eachother
                FlyController flyController = GetComponent<FlyController>();
                if (flyController != null)
                {
                    flyController.DamageFly();
                    return;
                }
                KidFlyController kidFly = GetComponent<KidFlyController>();
                if (kidFly != null)
                {
                    kidFly.DamageFly();
                    return;
                }
            }
        }
    }
}
