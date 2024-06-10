using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class changeCharacterProfile : MonoBehaviour
{
    [SerializeField]
    private TMP_Text characterName;
    [SerializeField]
    private GameObject[] characters;
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private StatsPanel statsPanel;

    private GameObject character;
    int n = 0;

    private void Start()
    {
        character = Instantiate(characters[0], spawnPoint);
        characterName.text = "The Fox";
        characterName.color = new Color(255 / 255f, 236 / 255f, 0 / 255f, 1f);
        character.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnMouseEnter()
    {
        Color textColor = characterName.color;

        textColor.a = 0.8f;

        characterName.color = textColor;
    }

    private void OnMouseExit()
    {
        Color textColor = characterName.color;

        textColor.a = 1;

        characterName.color = textColor;
    }

    private void OnMouseDown()
    {
        n += 1;
        statsPanel.UpdateStatsPanel(n % 3);

        if (n % 3 == 0)
        {
            Destroy(character);
            character = Instantiate(characters[0], spawnPoint);
            characterName.text = "The Fox";
            characterName.color = new Color(255 / 255f, 236 / 255f, 0 / 255f, 1f);
            character.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (n % 3 == 1)
        {
            Destroy(character);
            character = Instantiate(characters[1], spawnPoint);
            characterName.text = "The Panda";
            characterName.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 1f);
        }
        else if (n % 3 == 2)
        {
            Destroy(character);
            character = Instantiate(characters[2], spawnPoint);
            characterName.text = "The Spider";
            characterName.color = new Color(195 / 255f, 255 / 255f, 0 / 255f, 1f);
        }

        Color textColor = characterName.color;

        textColor.a = 0.8f;

        characterName.color = textColor;
    }
}
