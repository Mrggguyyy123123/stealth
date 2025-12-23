using System;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem
{   
    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;
    private int health;
    private int healthMax;

    
    

    public HealthSystem(int health)
    {
        this.health = health;
        this.healthMax = health;
    }

    
    public int GetHealth()
    {
        return health;
        
    }

    public float GetHealthPercentage()
    {
        return (float)health/healthMax;
    }
    public void Damage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0) {
            health = 0;
        }
        if(OnHealthChanged != null) OnHealthChanged(this, EventArgs.Empty);
        if (health == 0)
            OnDead?.Invoke(this, EventArgs.Empty);  
    
    }
}
