using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeOutfit : MonoBehaviour
{
    [SerializeField]
    private GameObject outfitChanger;

    private bool canChangeOutfit;

    private void Update()
    {
        if (canChangeOutfit && Input.GetKeyDown(KeyCode.E))
        {
            OutfitChangerOpen();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canChangeOutfit = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canChangeOutfit = false;
        }
    }

    public void OutfitChangerOpen()
    {
        outfitChanger.SetActive(true);
    }
    public void OutfitChangerClose()
    {
        outfitChanger?.SetActive(false);
        FindObjectOfType<BodyPartsManager>().UpdateBodyParts();
    }
}
