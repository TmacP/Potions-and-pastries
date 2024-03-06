-> first_day

=== first_day
= fresh_day
After a long trip from the city you finally reach your new inn. Standing outside you see someone hard at work
// pause story till talk to npc
// set 
VAR nextPathString = "first_day.tutorial"
-> DONE
= tutorial
// talk to npc .. unity input
They say, "Hello, you must be the new owner. Would you like an explanation about running thing?"

 * "Yes, I'm new here[".] can you explain.", you say.
 * "No, I already know what to do[".]", you say. -> end_tutorial

- "Of course, I was good friends with the previous owner and can tell you everything you need to know" he says. "As the new owner you are responsible for gathering ingredients for the inn's apothecary and bakery. You will find everything you need around outside. You'll have the whole day to gather since the inn doesn't start service till the evening."

- (questions)
* "What sort of ingredients[".] do I need to gather?", you ask. -> ingredients
* "What will I be crafting[".] with the ingredients I gather?", you ask. -> crafts
* "Okay I think I understand[".] -> end_tutorial

- (ingredients) He responds, "The ingredients you will need are: Apples, Cherries, Eggs, Cocoa beans, Milk, Salt, Sugar Canes, Tea Leaves, Vanilla Beans, and Wheat.". -> questions
- (crafts) He responds, "with these ingredients you will be crafting potions and pastries to sell to the patrons of the inn." -> questions


    -> end_tutorial

= end_tutorial
"Great, since you know what you're doing I'll leave you to it.", they reply.
~ nextPathString = "first_day.end_first_day"
-> DONE




= end_first_day
The sun is starting to set. We better go inside and get ready for our first customers.
// pause story till start of second day
-> second_day

=== second_day

After your first night you slept like a baby. It feels amazing to finally have your own place. It was rather exciting meeting all those adventurers. Too bad you were so busy serving you didn't get much time to chat. I'd bet they've got some wild stories about the sort of quests they're on. Maybe we will get a chance to ask them while we are out gathering more ingredients for tonight. -> talk

= talk
// pause story till talk to npc
// get the class of the npc and our friendship lvl with them
//Rogue
//Magician
//Ranger
//Fighter
VAR class = "Rogue"
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
    -> finish
= Magician
"I'm Magician {name} bish"
- -> finish
= Ranger
"I'm Ranger {name} bish"
- -> finish
= Fighter
"I'm Fighter {name} bish"
- -> finish


= finish










 -> END
