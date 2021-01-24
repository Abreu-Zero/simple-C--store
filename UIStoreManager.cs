using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStoreManager : MonoBehaviour
{
    
    private Text diamondsText, rubysText, greenText, redText;
    private Image armorImage, skinImage;
    public UIStoreManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        diamondsText = GameObject.Find("ImageDiamond").GetComponentInChildren<Text>();
        rubysText = GameObject.Find("ImageRuby").GetComponentInChildren<Text>();
        greenText = GameObject.Find("ImagePotionGreen").GetComponentInChildren<Text>();
        redText = GameObject.Find("ImagePotionRed").GetComponentInChildren<Text>();
        skinImage = GameObject.Find("InvSkinImg").GetComponent<Image>();
        armorImage = GameObject.Find("InvEquipImg").GetComponent<Image>();
    }

    public void UpdateGems(int diamonds, int rubys){
        diamondsText.text = diamonds.ToString();
        rubysText.text = rubys.ToString();
    }

    public void UpdatePotions(int green, int red){
        greenText.text = green.ToString();    
        redText.text = red.ToString();
    }

    public void UpdateTextButtons(string toUpdate, GameObject baseItem)
    {
        baseItem.GetComponentInChildren<Text>().text = toUpdate;
    }

    public void UpdateInventory(string pathSkin, string pathArmor)
    {
        if (pathArmor.Length > 0)
        {
            pathArmor = "Sprites/Steve/Equips/" + pathArmor;
            armorImage.sprite = Resources.Load<Sprite>(pathArmor);
            armorImage.enabled = true;
        }else
        {
            armorImage.enabled = false;
        }
        if (pathSkin.Length > 0)
        {
            pathSkin = "Sprites/Steve/Skins/" + pathSkin;
            skinImage.sprite = Resources.Load<Sprite>(pathSkin);
        }     
    }
}
