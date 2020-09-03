using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum barType{
health, mana
}
public class Barra : MonoBehaviour
{
    public barType type;
    private Slider slider;
    


    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        switch(type){
            case barType.health:
            slider.maxValue = PlayerController.MAX_HEALTH;
            slider.value = PlayerController.INITIAL_HEALTH;
            break;
            case barType.mana:
            slider.maxValue = PlayerController.MAX_MANA;
            slider.value = PlayerController.INITIAL_MANA;
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
         switch(type){
            case barType.health:
            slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetHealth();
            break;
            case barType.mana:
            slider.value = GameObject.Find("Player").GetComponent<PlayerController>().GetMana();

            break;
        }
    }
}
