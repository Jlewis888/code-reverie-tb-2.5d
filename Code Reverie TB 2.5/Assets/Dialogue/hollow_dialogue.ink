VAR test = false
VAR FindCecil_Available = false

-> SpeakToCecil

=== FindCecil ===
#CheckQuestDialogue:FindCecil;Available;FindCecil_Available
Finally made it.

We should find Cecil to get an update on the situation. 
 * [Let's go] #AcceptQuest:FindCecil #ExitDialogue:close
    -> END
 * { FindCecil_Available } [Continue]
    -> END

=== SpeakToCecil ===
Hey there Fullbody!

I see you finally made it and not a moment too soon.
 + [What's the situation]
    Probably better if I show you. Follow me. #CompleteQuestObjective:test #ExitDialogue:close
    -> CecilExplanation 
    
    
=== CecilExplanation ===
//#CompleteQuestObjective:test
//This is where Cecil Explanins stuff #ExitDialogue:close
-> END
