VAR class = ""
VAR friendship = 0
VAR name = ""
VAR day = 1
VAR nextPathString = ""

"Welcome. If this is your first time playing I'll show you how to craft a delicious puff pastry." 

 * "That sounds wonderful."
 -> explain
 * "No thanks I already know how to play."
 -> already_know
 

=== explain
~ nextPathString = "gather_ingredients"
- "First grab the ingredients from that box. Gather them by pressing `E` beside it. When you've got them come talk to me by standing beside me and pressing `E` again."
+ [Continue]
-> DONE

=== gather_ingredients
~ nextPathString = "open_inventory"
"I got the stuff from the box"
- "Fantastic now lets look at what you picked up. Open your inventory by pressing `TAB`. After close it by pressing `TAB` again. Do this now then come back and I'll explain how to add the ingredients into your service deck"
+ [Continue]
-> DONE


=== open_inventory
~ nextPathString = "start_service"
"I found my inventory"
- "Now let me explain how crafting works around here. Your inventory holds ingredients but to use them with the crafting station you will need to convert them into cards to add to your deck."
+ "How do ingredients transform into playing cards?"
- "Don't worry about that! All you have to do is drag the ingredients from your inventory into the deck. Press `TAB` again and do so now. Then after I'll explain about starting service."
+ [Continue]
-> DONE

=== start_service
~ nextPathString = "recipie_book"
"I added the ingredients into my deck!"
- "Congradulations! Now you probably noticed when you pressed `TAB` to open your inventory there is a button to start service under your deck."
+ "What does the 'start service' button do?"
- "Great question! That button will open our inn to our customers for the evening. It is IMPORTANT that you know once you 'start service' you won't be able to open your inventory anymore!
+ "Got it[."] so I gather ingredients, open my inventory then add them to the deck to get those cards for serving customers of my inn?" 
- "Yes, you've got it figured out. Why don't you click that button and we can open up the inn for service."
+ [Continue]
-> DONE

=== recipie_book
~ nextPathString = "mixer"
"I clicked the button and now someone is in here. They want something."
- "That's right, our first customer. Now I'll show you how to make that puff pastry like I promised."
+ "Okay, tell me how."
-"First you need to check the recipie, press `G` to open up your recipie book. then under the oven column you will see a 'Puff' with the ingredients you need to put into the oven to bake it."
+ "what are the Mixer, Cauldron, and Oven?"
- "Those are the three crafting station behind me that you will use. Please open the recipie book now then take a look. After talk to me and I can explain further."
+ [Continue]
-> DONE


=== mixer
~ nextPathString = "mixer_butter"
"I looked at the recipie book. It looks like I need sugar, butter, and dough to make my Puff."
-"That's right. So first let's make sugar. Go beside the mixer and select the sugar cane card in you hand. Press `Q` to play the card into the mixer. Then after press `E` to take the sugar. Try making sugar then I'll explain making the butter and dough."
+ [Continue]
-> DONE

=== mixer_butter
~ nextPathString = "mixer_dough"
"I made sugar."
-"Okay now you want to do the same with milk to make butter. Try making butter then I'll tell you how to make the dough."
+ [Continue]
-> DONE


=== mixer_dough
~ nextPathString = "oven"
"I made butter."
-"Perfect, now you will want to mix the wheat to make flour. Then, once you have flour, add the flour and an egg into the mixer to make dough. After you're done I'll explain making dough."
+ [Continue]
-> DONE

=== oven
~ nextPathString = "deliver_puff"
"I made the dough"
-"Now that we have mixed our ingredients we can add them into the oven to bake our puff. Go to the oven and add the sugar, butter, and dough into the oven. Once you have the puff I'll explain how to deliver it to our customer"
+ [Continue]
-> DONE


=== deliver_puff
~ nextPathString = "already_know"
"I've got the puff."
-"Great now select the puff in your hand and stand next to our customer. Press `Q` to deliver it. Once you have done that come talk to me and I'll let you know what you might want to do next."
+ [Continue]
-> DONE


=== already_know
"I delivered the puff."
-"Nicely done. You now know the basics of gathering, crafting, and service. It looks like you got some money too. If you want some additional ingredients for more recipies you could use the money to unlock the gates outside."
+ "Thanks for helping me figure things out."
-"No problem now why don't you try gathering some ingredients for yourself outside."
+ [Continue]
-> END


