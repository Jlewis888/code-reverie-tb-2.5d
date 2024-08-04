-> main

=== main ====
Hello! #Speaker:NPC

How may I help you? fhasdklfh dhjjadklfjh Once upon a time... 
 + [Weapon Stash]
    Let's upgrade your weapon stash #Speaker:Player
    -> chosen
 + [Bow Stash]
    Let's upgrade your bow stash #Speaker:Player #AcceptQuest:questID
    -> chosen
 + [Shield Stash]
    Let's upgrade your shield stash #Speaker:Player
    -> chosen
    
 + [trash]
    This is trash #Speaker:Player
    -> chosen
 + [Nothing Right Now] #ExitDialogue:close
    -> chosen

=== chosen ===
- They lived happily ever after. #Speaker:Test
-> END