using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashForce = 10f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public KeyCode dashKey = KeyCode.K;

    private bool isDashing = false;
    private float dashTimeLeft = 0f;
    private float dashCooldownTimer = 0f;
    private Vector3 moveDirection;

    private void Update()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        if (Input.GetKeyDown(dashKey) && dashCooldownTimer <= 0)
        {
            isDashing = true;
            dashTimeLeft = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if (isDashing)
        {
            dashTimeLeft -= Time.deltaTime;
            if (dashTimeLeft <= 0)
            {
                isDashing = false;
            }
        }
        else
        {
            dashCooldownTimer -= Time.deltaTime;
            dashCooldownTimer = Mathf.Max(0, dashCooldownTimer); // Evita valores negativos
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            GetComponent<Rigidbody>().AddForce(moveDirection * dashForce, ForceMode.Impulse);
        }
    }
}