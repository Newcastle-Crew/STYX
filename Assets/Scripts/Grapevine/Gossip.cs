#region 'Using' information
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#endregion

public class Gossip : MonoBehaviour
{
    [SerializeField] private TMP_Text currentGossip;

    private int gossipNum; // keeps track of the current gossip being displayed - no duplicates!

    // how this works:
    // gossipNum starts at 0, this is the default dialogue
    // clicking the gossip button runs the RandomGossip method
    // if it sees a 0, it changes the text
    // the update method looks for exact text before changing the number
    // this is effectively a sequence of text. Could randomise this later, but w/e, works for now.

    public void RandomGossip()
    {
        if(gossipNum == 0) // shows the cerberus gossip if default text is on
        { currentGossip.text = "That three-headed mutt LOVES honey cakes.       He almost bit my hand off when I tried to feed him one."; }

        if (gossipNum == 1)
        { currentGossip.text = "Aren't those freaky bird ladies your sisters? Tough luck, man."; }

        if(gossipNum == 2)
        { currentGossip.text = "Ole' Hephy is never far from that anvil. Maybe she'd be willing to look at your boat?"; }

        if (gossipNum == 3)
        { currentGossip.text = "That's all I've got for now. Buy something, will you?"; }

    }

    private void Update()
    {
        if (currentGossip.text == "WHAT'RE YA SELLIN? WHAT'RE YA BUYIN?") // default dialogue is gossip "0", so it knows what to do
        { gossipNum = 0; }

        if (currentGossip.text == "That three-headed mutt LOVES honey cakes.       He almost bit my hand off when I tried to feed him one.")
        { gossipNum = 1; } // cerb is "1"

        if (currentGossip.text == "Aren't those freaky bird ladies your sisters? Tough luck, man.")
        { gossipNum = 2; } // keres is "2"

        if(currentGossip.text == "Ole' Hephy is never far from that anvil. Maybe she'd be willing to look at your boat?")
        { gossipNum = 3; } // hephaestus is "3"
    }
}
