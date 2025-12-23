using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.SceneManagement;

using System.Collections;

public class dead_zone : MonoBehaviour
{
    public Transform pfHealthBar;
    public SpriteRenderer spriteRenderer_player;
    public SpriteRenderer spriteRenderer_gun;
    private HealthSystem healthSystem;
    public void Die()
    {
        if (spriteRenderer_player != null)
            spriteRenderer_player.enabled = false;
            if (spriteRenderer_gun != null)
            spriteRenderer_gun.enabled = false;
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(2f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Die();
        }
    }
    public void Damage(int amount)
    {
        healthSystem.Damage(100);
    }
}