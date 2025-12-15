using UnityEngine;
using System.Collections;

public class HidingSpotScript : MonoBehaviour
{
    public Transform player;
    public Transform Sprite_renderer_Player;
    public Transform Sprite_renderer_Gun;
    private SpriteRenderer playerSprite;
    private SpriteRenderer gunSprite;
    private Rigidbody2D playerRb;
    private bool playerInTrigger = false;
    private Color originalColor;
    public Collider player_collider;
    private bool isHidden = false;

    void Start()
    {


        playerSprite = Sprite_renderer_Player.GetComponent<SpriteRenderer>();
        gunSprite = Sprite_renderer_Player.GetComponent<SpriteRenderer>();
        playerRb = player.GetComponent<Rigidbody2D>();
    
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHidden){
                HidePlayer();
                Debug.Log("hidden");
            }
        }else if (isHidden && Input.GetKeyDown(KeyCode.E))
        {
            UnHidePlayer();
            Debug.Log("Unhidden");
        }
    }

    private void HidePlayer()
    {
        isHidden = true;


        playerRb.simulated = false;


        Color originalColor = playerSprite.color;

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private void UnHidePlayer()
    {
        isHidden = false;
        playerRb.simulated = true;
        playerSprite.color = originalColor;
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
