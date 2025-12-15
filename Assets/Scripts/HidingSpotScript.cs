using UnityEngine;
using System.Collections;

public class HidingSpotScript : MonoBehaviour
{
    public Transform player;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;
    private bool playerInTrigger = false;
    private bool isHidden = false;

    void Start()
    {
        if (player != null)
        {
            playerSprite = player.GetComponent<SpriteRenderer>();
            playerRb = player.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHidden)
                StartCoroutine(HidePlayer());
            else
                StartCoroutine(UnHidePlayer());
        }
    }

    private IEnumerator HidePlayer()
    {
        isHidden = true;

        // Disable physics so player won't interact
        if (playerRb != null) playerRb.simulated = false;

        float duration = 0.5f;
        float elapsed = 0f;
        Color originalColor = playerSprite.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private IEnumerator UnHidePlayer()
    {
        isHidden = false;

        if (playerRb != null) playerRb.simulated = true;

        float duration = 0.5f;
        float elapsed = 0f;
        Color originalColor = playerSprite.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInTrigger = false;
    }
}
