using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour 
{

    [Serializable]
    private class ConversationState
    {
        public Conversation conversation;
        public bool conversationHasEnded;

        public ConversationState (Conversation conversation)
        {
            this.conversation = conversation;
            conversationHasEnded = false;
        }

        public void EndConversation() => conversationHasEnded = true;
    }


    [SerializeField] private SpackersUI speackersUILeft;
    [SerializeField] private SpackersUI speackersUIRight;

    [SerializeField] private List<ConversationState> conversationsList;

    private int activeLineIndex = 0;
    public int activeConvesationIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        speackersUILeft.Speacker = conversationsList[activeConvesationIndex].conversation.speackerLeft;
        speackersUIRight.Speacker = conversationsList[activeConvesationIndex].conversation.speackerRight;

        AdvanceConversation();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AdvanceConversation();
        }
    }

    private void AdvanceConversation()
    {
        if(conversationsList[activeConvesationIndex].conversationHasEnded == false)
        {
            if (activeLineIndex< conversationsList[activeConvesationIndex].conversation.lines.Count )
            {
                DisplayLine();
                activeLineIndex++;
            }
            else
            {
                speackersUILeft.Hide();
                speackersUIRight.Hide();
                activeLineIndex = 0;
                conversationsList[activeConvesationIndex].EndConversation();
                if(activeConvesationIndex < conversationsList.Count -1) 
                    activeConvesationIndex++;
            }
        }
    }

    private void DisplayLine()
    {
        Line line = conversationsList[activeConvesationIndex].conversation.lines[activeLineIndex];
        CharacterDS character = line.character;

        if (speackersUILeft.SpeackerIs(character))
        {
            SetDialogue(speackersUILeft, speackersUIRight, line.text);
        }
        else
        {
            SetDialogue(speackersUIRight, speackersUILeft, line.text);
        }
    }

    private void SetDialogue(SpackersUI activeSpeackerUI, SpackersUI inactiveSpeackerUI, string text)
    {
        // add the animation Text here !
        activeSpeackerUI.Dialog= text;
        activeSpeackerUI.Show();
        inactiveSpeackerUI.Hide();
    }
}
