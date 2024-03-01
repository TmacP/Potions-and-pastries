VAR friendship = 0  // (int) _inkStory.variablesState["NPCState.Friendship"]
VAR class = "Warrior" //(string) _inkStory.variablesState["NPCState.Class"]

-> first_day

=== first_day
= tutorial
After a long trip from the city you finally reach your new inn. Standing outside you see a {class} hard at work
He says, "Hello, you must be the new owner of the inn. Would you like an explanation about running thing?"

 * "Yes, I'm new here[".] can you explain.
 * No, I already know what to do. -> end_tutorial

- "of course, I was good friends with the previous owner and can tell you everything you need to know"
    -> end_tutorial

= end_tutorial
"Great, since you know what you're doing I'll leave you to it."
-> end_first_day


= end_first_day
The sun is starting to set. We better go inside and get ready for our first customers.
-> second_day

=== second_day
After your first night you ...
 -> END
