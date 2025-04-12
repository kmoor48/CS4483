using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BangSoundTrigger : MonoBehaviour
{
    public AudioSource bangSound;
    private Transform playerHand;
    public TextMeshProUGUI noteText;
    public GameObject pannel;
    public TextMeshProUGUI GameOverText;
    public GameObject blackscreenpannel;

    private bool hasPlayed = false;

    private void Start()
    {
        playerHand = GameObject.FindWithTag("PlayerRightHandTarget").transform;
        
        pannel.SetActive(false);
        noteText.gameObject.SetActive(false);
        GameOverText.gameObject.SetActive(false);
        blackscreenpannel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player") && IsHoldingNote())
        {
            bangSound.Play();
            hasPlayed = true;
            GameOverText.gameObject.SetActive(true);
            blackscreenpannel.SetActive(true);
        }
    }

    private bool IsHoldingNote()
    {
        if (playerHand.childCount > 0)
        {
            GameObject heldItem = playerHand.GetChild(0).gameObject;
            return heldItem.CompareTag("jim_note");
        }
        return false;
    }
}
