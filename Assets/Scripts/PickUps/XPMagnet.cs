using UnityEngine;

public class XPMagnet : Pickup
{
    float trackTimer; // How long this pickup has been chasing the player.
    bool inEffect = false; // When the magnet touches the player, set this to true, so the Magnet performs its buff.
    float remainingEffectDuration; // Remaining duration of the magnet effect.

    [Header("Magnet")]
    public float effectDuration = 2f;
    public float magnetRange = 90f; // The range within which pickups are collected.

    protected override void Update()
    {
        if (target)
        {
            if (inEffect)
            {
                transform.position = target.transform.position; // Move the pickup to the player's position.
                remainingEffectDuration -= Time.deltaTime;

            }
            else
            {
                // Move it towards the player and check the distance between.
                Vector2 distance = target.transform.position - transform.position;
                if (distance.sqrMagnitude > speed * speed * Time.deltaTime)
                    transform.position += speed * Time.deltaTime * (Vector3)distance.normalized;
                else
                {
                    // When it becomes active, disable the pickup GameObject.
                    inEffect = true;
                    remainingEffectDuration = effectDuration;
                    gameObject.SetActive(false);
                }

                // Tracks how long we have been tracking the target.
                trackTimer += Time.deltaTime;
                
            }

            // Check for nearby pickups and collect them
            Collider2D[] nearbyPickups = Physics2D.OverlapCircleAll(transform.position, magnetRange);
            foreach (Collider2D pickupCollider in nearbyPickups)
            {
                Pickup pickup = pickupCollider.GetComponent<Pickup>();
                if (pickup)
                {
                    pickup.Collect(target, speed, lifespan);
                }
            }
            
        }
        else
        {
            // Handle the animation of the object.
            transform.position = initialPosition + bobbingAnimation.direction * Mathf.Sin((Time.time + initialOffset) * bobbingAnimation.frequency);
        }
    }

    public override bool Collect(PlayerStats target, float speed, float lifespan = 0f)
    {
        if (!this.target)
        {
            this.target = target;
            this.speed = speed;
            if (lifespan > 0) this.lifespan = lifespan;
            return true;
        }
        return false;
    }
}