VAR friendship = 0  // (int) _inkStory.variablesState["NPCState.Friendship"]
VAR class = "Warrior" //(string) _inkStory.variablesState["NPCState.Class"]

-> first_day

=== first_day
= tutorial
After a long trip from the city you finally reach your new inn. Standing outside you see a {class} hard at work
He says, "Hello, you must be the new owner. Would you like an explanation about running thing?"

 * "Yes, I'm new here[".] can you explain.", you say.
 * "No, I already know what to do[".]", you say. -> end_tutorial

- "Of course, I was good friends with the previous owner and can tell you everything you need to know" he says. "As the new owner you are responsible for gathering ingredients for the inn's apothecary and bakery. You will find everything you need around outside. You'll have the whole day to gather since the inn dosen't start service till the evening."

- (questions)
* "What sort of ingredients[".] do I need to gather?", you ask. -> ingredients
* "What will I be crafting[".] with the ingredients I gather?", you ask. -> crafts
* "Okay I think I understand[".] -> end_tutorial

- (ingredients) He responds, "The ingredients you will need are: Apples, Cherrys, Eggs, Cocoa beans, Milk, Salt, Sugar Canes, Tea Leaves, Vanilla Beans, and Wheat.". -> questions
- (crafts) He responds, "with these ingredients you will be crafting potions and pasteries to sell to the patrons of the inn." -> questions


    -> end_tutorial

= end_tutorial
"Great, since you know what you're doing I'll leave you to it.", he replies.
-> end_first_day


= end_first_day
The sun is starting to set. We better go inside and get ready for our first customers.
-> second_day

=== second_day
After your first night you slept like a baby. It feels amazing to finally have your own place. It was rather exciting meeting all those adventurers. Too bad you were so busy serving you didn't get much time to talk to them. I'd bet they've got some wild storys about the sort of quests they're on. Maybe we will get a chance to ask them while we are out gathering more ingredients for tonight.




 -> END
