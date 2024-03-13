VAR class = ""
VAR friendship = 0
VAR name = ""
VAR day = 1
VAR nextPathString = ""

// Are our 4 gates open?
VAR appleGate = false
VAR cherryGate = false
VAR vanillaGate = false
VAR chocolateGate = false


-> first_day
//-> second_day // debug
//-> third_day // debug

// FIRST DAY *****************************************
// tutorial dialogue
=== first_day
It is the start of something wonderful
+ [Continue]
~ nextPathString = "first_day.tutorial"
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
+ [Continue]
~ nextPathString = "second_day"
-> DONE

// SECOND DAY *****************************************
// Introduce caracters and cave
=== second_day
{ day < 2 : -> DONE}
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
        -> finish
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
    * "Well I'm not going in any freaky cave[."], even if there is treasure, and glory." 
    -> end_cave_intro
    
    - (why) "Supposedly inside there are all sorts of monsters inside which one could fight to prove their bravery."
    -> cave
"
    - (spelunking) "That and the rumors of a wealth of gold and precious gems hidden inside its depths."
    -> cave
    
    - (end_cave_intro) "And I don't blame you. Who'd run the inn? Besides you don't look like the type to follow others."
        "Thanks"
        "I respect that, even if you do seem a little timid."
        "Hey."

- -> finish

= Fighter
They say, "I'm Fighter {name}. So you decided to try running an inn, very bold of you, you know most buisinesses fail. But I admire your courage."
"Thanks... I think."
- (cave)
    * "What are you up to[?"], out here anyways?"
    -> why
    * "You aren't another one[."] of those cave explorors are you?"
    -> what
    * "Well I hope you figure out all that[."], even if you seem a little confused I'm sure it will all work out." 
    -> end_cave_intro
    
- (why)
"You know I've been so busy chasing wealth I never even stopped to ask myself that. What even am I doing out here?"
"?"-> cave

- (what) 
"Amazing, how could you tell? And who told you about the cave it is supposed to be a secret." -> cave

- (end_cave_intro) "You know, even when things look like they are going badly things have a way of working themselves out."
 
- -> finish

= finish
~ nextPathString = "third_day"
-> DONE

// THIRD DAY *****************************************
// adventurers find the cave
=== third_day
{ day < 3 : -> DONE}
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
        -> finish
}

= Rogue
They say, "H"
- (find_cave)
    * "Did you find it yet?" 
    -> one
    * "What is it like inside" 
    -> two
    * "Sounds interesting" 
    -> end_cave_entrance
    
- (one) 
"Yes found the entrance"
    -> find_cave
"
- (two) 
"Inside is like"
    -> find_cave
    
- (end_cave_entrance) 
"See you later"
    -> finish
    
= Magician
They say, "H"
- (find_cave)
    * "Did you find it yet?" 
    -> one
    * "What is it like inside" 
    -> two
    * "Sounds interesting" 
    -> end_cave_entrance
    
- (one) 
"Yes found the entrance"
    -> find_cave
"
- (two) 
"Inside is like"
    -> find_cave
    
- (end_cave_entrance) 
"See you later"
- -> finish

= Ranger
They say, "H"
- (find_cave)
    * "Did you find it yet?" 
    -> one
    * "What is it like inside" 
    -> two
    * "Sounds interesting" 
    -> end_cave_entrance
    
- (one) 
"Yes found the entrance"
    -> find_cave
"
- (two) 
"Inside is like"
    -> find_cave
    
- (end_cave_entrance) 
"See you later"

- -> finish

= Fighter
They say, "H"
- (find_cave)
    * "Did you find it yet?" 
    -> one
    * "What is it like inside" 
    -> two
    * "Sounds interesting" 
    -> end_cave_entrance
    
- (one) 
"Yes found the entrance"
    -> find_cave
"
- (two) 
"Inside is like"
    -> find_cave
    
- (end_cave_entrance) 
"See you later"
- -> finish

= finish
~ nextPathString = "fourth_day"
-> DONE

// FOURTH DAY *****************************************
// adventurers find a monster gaurding treasure
=== fourth_day
{ day < 4 : -> DONE}
~ nextPathString = "fifth_day"
-> DONE

// FIFTH DAY *****************************************
// adventurers find a monster gaurding treasure
=== fifth_day
{ day < 5 : -> DONE}
~ nextPathString = "sixth_day"
-> DONE

// SIXTH DAY *****************************************
// adventurers defeat monster and get treasure END OF STORY
=== sixth_day
{ day < 6 : -> DONE}

 -> END
