using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

namespace CodeReverie
{
    public class CharacterVictoryPanel : SerializedMonoBehaviour
    {
        public Character character;
        public Image characterPortrait;
        public TMP_Text characterName;
        public Slider expSlider;


        public void Set(Character _character, float exp)
        {

            character = _character;
            character.Experience = exp;
            
            if (character != null)
            {
                characterPortrait.sprite = character.GetCharacterPortrait();
                characterName.text = character.info.characterName;
                expSlider.value = character.currentExp;
            }
        }
        
    }
}