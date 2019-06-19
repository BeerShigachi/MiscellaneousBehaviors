using System.Collections;
using UnityEngine;

public class ProjectileMotion : MonoBehaviour
{
    public Transform projectile;
    public Transform Target;
    public float firingAngle = 45.0f;
    public float gravity = 9.8f;

    public bool isActive;

    void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isActive)
        {
            isActive = true;
            StartCoroutine(SimulateProjectile());
        }
    }


    IEnumerator SimulateProjectile()
    {

        projectile.position = transform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance between projectile and target
        float targetDistance = Vector3.Distance(projectile.position, Target.position);

        // Calculate the velocity to throw object to target at specified angle.
        float projectileDistance = targetDistance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Velocity of X Y
        float velocityX = Mathf.Sqrt(projectileDistance) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float velocityY = Mathf.Sqrt(projectileDistance) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = targetDistance / velocityX;

        // Projectile face at target.
        projectile.rotation = Quaternion.LookRotation(Target.position - projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            projectile.Translate(0, (velocityY - (gravity * elapse_time)) * Time.deltaTime, velocityX * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }

        isActive = false;
    }
}
