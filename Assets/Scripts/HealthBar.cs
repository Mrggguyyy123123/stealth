using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HealthBar : MonoBehaviour
{
    private HealthSystem healthSystem;
    public void Setup(HealthSystem healthSystem)
    {
        this.healthSystem=healthSystem;
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        transform.Find("Bar").localScale = new UnityEngine.Vector3(healthSystem.GetHealthPercentage(),1);
    }
    
}
