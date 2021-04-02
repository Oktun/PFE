using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpackersUI : MonoBehaviour
{
    public Image portrait;
    public Text fullName;
    public Text dialog;

    private CharacterDS speacker;
    public CharacterDS Speacker
    {
        get { return speacker; }
        set
        {
            speacker = value;
            portrait.sprite = speacker.portrait;
            fullName.text = speacker.fullName;
        }
    }

    public string Dialog
    {
        set { dialog.text = value; }
    }

    public bool SpeackerIs(CharacterDS character) => speacker == character;

    public bool HasSpeacker() => speacker != null;

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);
}
