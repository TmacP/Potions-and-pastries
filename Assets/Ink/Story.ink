-> first_day
//-> second_day // debug

// FIRST DAY
=== first_day
= fresh_day
After a long trip from the city you finally reach your new inn. Standing outside you see someone hard at work
VAR nextPathString = "first_day.tutorial" // where we want to continue when we talk to npc
-> DONE

= tutorial
They say, "Hello, you must be the new owner. Would you like an explanation about running thing?"

 * "Yes, I'm new here[".] can you explain.", you say.
 * "No, I already know what to do[".]", you say. -> end_tutorial

- "Of course, I was good friends with the previous owner and can tell you everything you need to know" he says. "As the new owner you are responsible for gathering ingredients for the inn's apothecary and bakery. You will find everything you need around outside. You'll have the whole day to gather since the inn doesn't start service till the evening."

- (questions)
    * "What sort of ingredients[".] do I need to gather?", you ask. -> ingredients
    * "What will I be crafting[".] with the ingredients I gather?", you ask. -> crafts
    * "Okay I think I understand[".] -> end_tutorial
- (ingredients) They respond, "The ingredients you will need are: Apples, Cherries, Eggs, Cocoa beans, Milk, Salt, Sugar Canes, Tea Leaves, Vanilla Beans, and Wheat.". -> questions
- (crafts) He responds, "with these ingredients you will be crafting potions and pastries to sell to the patrons of the inn." -> questions
-> end_tutorial

= end_tutorial
"Great, since you know what you're doing I'll leave you to it.", they reply.
~ nextPathString = "second_day"
-> DONE

// SECOND DAY
=== second_day
// get the class of the npc [Rogue Magician Ranger Fighter] and our friendship lvl with them
//VAR class = "Rogue"
//VAR class = "Magician"
VAR class = "Ranger"
//VAR class = "Fighter"
VAR friendship = 1
VAR name = "Jane Doe"
{
- friendship >= 1:
    {
        - class == "Rogue":
            -> Rogue
        - class == "Magician":
            -> Magician
        - class == "Ranger":
            -> Ranger
        - class == "Fighter":
            -> Fighter
    }
    - else:
        "I don't trust strangers" 
}

= Rogue
They say, "Hello, I'm Rogue {name}. It is nice to have a chance to talk. You were so busy last night. How are you enjoying your inn so far?"
    * "A lot[."], it has so much potential. With some work things will really go great."
    * "Not much[."], I didn't realize how much work it took to run things."
- "Yea, it is a lot of work, but the potential is there. I can't wait to see what you do with the place."
    * "So what are you doing[."] out here anyways?", you ask.
    * "Thanks"[."] and good luck with whatever you're up to. -> finish
    - "Well aren't you the curious little inn keeper. I don't know if we're good enough friends for me to let you know that. Let's just say I'm looking for something".
    - "Okay best of luck finding it.", you say.
    -> finish
    
= Magician
They say, "Hey, I'm Magician {name}. So you're the new owner of the inn."
"That's right."
"Well I hope you stock up on tea leaves. I'm know for being thirsty, amongst other things, especially after spending all day out in the sunshine."
* "What are you doing[."] out here anyways?", you ask.
* "Sure...[."] I'll get right on that.", you say. -> finish
- "Shh, that's my business. Plus if I told you I'd have to worry about you poisoning me and trying to steal my boots"
"That wouldn't happen"
"Exactally what a theif would say! Maybe once we are better friends I'll let you know. As long as I don't see you eyeing my boots."
"What ever you say. I'll leave you alone to keep being weird then"
- -> finish

= Ranger
They say, "I'm Ranger {name}. I guess you're probably wondering what all these people are doing out here anyways?"
* "Funny you should mention that[."], I was just about to ask.", you say. 
- "They will play coy like they aren't but they are looking for the entrance to the cave which is rumored to be around here."
- (cave)
    * "Why would they care about a cave" 
    -> why
    * "What like for spelunking?" 
    -> spelunking
    * "Well I'm not going in any freaky cave[."], even if there is treasure, and glory". 
    -> end_cave_intro
    
    - (why) "Supposedly inside there are all sorts of monsters inside which one could fight to prove their bravery."
    -> cave
"
    - (spelunking) "That and the rumors of a wealth of gold and precious gems hidden inside its depths."
    -> cave
    
    - (end_cave_intro) "And I don't blame you. Who'd run the inn? Besides you don't look like the type to follow others."
        "Thanks"
        "I respect that, even if you do seem a little timid"
        "Hey"

- -> finish

= Fighter
"I'm Fighter {name} bish"
- -> finish

= finish
~ nextPathString = "third_day"
-> DONE

// THIRD DAY
=== third_day
~ nextPathString = "fourth_day"
-> DONE

// FOURTH DAY
=== fourth_day
~ nextPathString = "fourth_day"
-> DONE

 -> END
