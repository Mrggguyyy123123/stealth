using UnityEngine;
using CodeMonkey.Utils;
public class GameHandle : MonoBehaviour
{
    public Transform pfHealthBar;
    public int maxHealth = 100;

    private HealthSystem healthSystem;

    private void Awake()
    {
        // Create health system
        healthSystem = new HealthSystem(maxHealth);

        // Spawn health bar
        Transform healthBarTransform = Instantiate(pfHealthBar, transform);
        healthBarTransform.localPosition = new Vector3(-1, 1, 0);
        HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>();
        healthBar.Setup(healthSystem);

        // Handle death
        healthSystem.OnDead += delegate(object sender, System.EventArgs e)
        {
            Destroy(gameObject); // destroy THIS GameObject
        };
    }

    // Public method to take damage
    public void Damage(int amount)
    {
        healthSystem.Damage(amount);
    }
}
