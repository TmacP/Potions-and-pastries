VAR class = ""
VAR friendship = 0
VAR name = ""
VAR day = 1
VAR nextPathString = ""

"Welcome. If this is your first time I'll show you how to craft a delicious puff pastry." 

 * "That sounds wonderful."
 -> dont_know
 * "No thanks I already know how to craft."
 -> already_know
 

=== dont_know
~ nextPathString = "then_what"
- "First grab the ingredients from that box. Gather by pressing the `E` key. When you've got them come talk to me by standing beside me and pressing `E`"
+ [Continue]
-> DONE
=== then_what 
- "Fir"
+ [Continue]
-> DONE
=== already_know
~ nextPathString = "first_day.tutorial"
-> END


//dialogue with info on what is going to happen, i.e. craft recipes with cards woo
//pick up ingredients (box or something with everything needed with a puff)
//make the deck
//top right pop up tells you how to make deck (pop up always there until completed)
//start service
//dialogue of “make something for NPC” and prompt to open recipe book
//recipe book only has required ingredients for puff listed
//NPC places order and walks you through how to make it
//give puff to NPC, get money (enough for a gate?), get kicked out the exterior, and game starts